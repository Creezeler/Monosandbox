// Decompiled with JetBrains decompiler
// Type: GrenadeManager
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using GorillaLocomotion;
using MonoSandbox;
using MonoSandbox.Behaviours;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class GrenadeManager : MonoBehaviour
{
  public bool editMode;
  public GameObject Grenade;
  public GameObject Holdable;
  public GameObject Explode;
  public GameObject Ring;
  public GameObject Folder;
  private GrenadeManager.GrenadeState state;
  private IEnumerator routine;
  private GorillaVelocityEstimator _velEstimator;
  private float grip;

  public void Start() => this.Folder = ((Component) this).gameObject;

  public void Update()
  {
    if (this.editMode && Object.op_Equality((Object) this.Holdable, (Object) null))
    {
      this.Holdable = Object.Instantiate<GameObject>(this.Grenade);
      this.Holdable.transform.localPosition = new Vector3(-0.028f, 0.0138f, 0.0027f);
      this.Holdable.transform.localScale = Vector3.op_Multiply(new Vector3(-1f, 1f, 1f), 1.1f);
      this.Holdable.transform.eulerAngles = new Vector3(152.336f, 84.623f, -101.461f);
      this.Holdable.transform.SetParent(RefCache.RHand.transform, false);
      this.Ring = ((Component) this.Holdable.transform.GetChild(0)).gameObject;
      this._velEstimator = this.Holdable.AddComponent<GorillaVelocityEstimator>();
    }
    else if (!this.editMode && Object.op_Inequality((Object) this.Holdable, (Object) null))
    {
      Object.Destroy((Object) this.Holdable);
      this.state = GrenadeManager.GrenadeState.Idle;
      if (this.routine != null)
      {
        this.StopCoroutine(this.routine);
        ((IDisposable) this.routine).Dispose();
        this.routine = (IEnumerator) null;
      }
    }
    if (!this.editMode)
      return;
    this.grip = InputHandling.RightGrip;
    if (this.state == GrenadeManager.GrenadeState.Idle && (double) this.grip > 0.6600000262260437)
    {
      this.state = GrenadeManager.GrenadeState.Activated;
      this.Ring.transform.SetParent((Transform) null, true);
      this.Ring.GetComponent<AudioSource>().Play();
      Rigidbody rigidbody = this.Ring.AddComponent<Rigidbody>();
      rigidbody.collisionDetectionMode = (CollisionDetectionMode) 2;
      rigidbody.interpolation = (RigidbodyInterpolation) 1;
      rigidbody.velocity = Vector3.op_Addition(this._velEstimator.linearVelocity, ((Collider) GTPlayer.Instance.bodyCollider).attachedRigidbody.velocity);
      this.routine = this.Explosion();
      this.StartCoroutine(this.routine);
    }
    else if (this.state == GrenadeManager.GrenadeState.Activated && (double) this.grip < 0.30000001192092896)
    {
      this.Holdable.transform.SetParent((Transform) null, true);
      Rigidbody rigidbody = this.Holdable.AddComponent<Rigidbody>();
      rigidbody.collisionDetectionMode = (CollisionDetectionMode) 2;
      rigidbody.interpolation = (RigidbodyInterpolation) 1;
      rigidbody.velocity = Vector3.op_Multiply(this._velEstimator.linearVelocity, 1.6f);
      rigidbody.angularVelocity = this._velEstimator.angularVelocity;
    }
  }

  public void OnDisable()
  {
    if (this.routine == null)
      return;
    this.StopCoroutine(this.routine);
  }

  public IEnumerator Explosion()
  {
    yield return (object) new WaitForSeconds(5f);
    this.state = GrenadeManager.GrenadeState.Cooldown;
    this.Holdable.GetComponent<AudioSource>().Play();
    this.Holdable.GetComponent<Renderer>().enabled = false;
    GameObject ExplodeOBJ = Object.Instantiate<GameObject>(this.Explode);
    ExplodeOBJ.transform.SetParent(((Component) this).transform);
    ExplodeOBJ.transform.localPosition = this.Holdable.transform.position;
    ExplodeOBJ.transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
    Collider[] colliderArray = Physics.OverlapSphere(this.Holdable.transform.position, 10f);
    for (int index = 0; index < colliderArray.Length; ++index)
    {
      Collider nearyby = colliderArray[index];
      Rigidbody rig = ((Component) nearyby).GetComponent<Rigidbody>();
      rig?.AddExplosionForce(7500f, this.Holdable.transform.position, 8f);
      ((Component) nearyby).GetComponent<BombDetonate>()?.Explode();
      ((Component) nearyby).GetComponent<MineDetonate>()?.Explode();
      ((Component) nearyby).GetComponent<global::Explode>()?.ExplodeObject();
      rig = (Rigidbody) null;
      nearyby = (Collider) null;
    }
    colliderArray = (Collider[]) null;
    Rigidbody PlayerRigidbody = ((Component) GTPlayer.Instance).GetComponent<Rigidbody>();
    PlayerRigidbody.AddExplosionForce(12500f * Mathf.Sqrt(PlayerRigidbody.mass), this.Holdable.transform.position, 8.75f);
    yield return (object) new WaitForSeconds(3f);
    this.state = GrenadeManager.GrenadeState.Idle;
    Object.Destroy((Object) this.Holdable);
    this.Holdable = (GameObject) null;
  }

  public enum GrenadeState
  {
    Idle,
    Activated,
    Cooldown,
  }
}
