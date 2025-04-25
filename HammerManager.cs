// Decompiled with JetBrains decompiler
// Type: HammerManager
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using MonoSandbox;
using MonoSandbox.Behaviours;
using UnityEngine;

#nullable disable
public class HammerManager : MonoBehaviour
{
  public bool editMode;
  private float lastTime;
  public GameObject asset;
  public GameObject holdable;
  public GameObject point;
  private AudioSource _hammerSource;
  private GorillaVelocityEstimator _velEstimator;

  public void Update()
  {
    if (this.editMode && Object.op_Equality((Object) this.holdable, (Object) null))
    {
      this.holdable = Object.Instantiate<GameObject>(this.asset);
      this.holdable.transform.eulerAngles = new Vector3(0.0f, 90f, -90f);
      this.holdable.transform.localScale = new Vector3(-1f, 1f, 1f);
      this.holdable.transform.localPosition = new Vector3(-0.03f, 0.02f, 0.035f);
      this.holdable.transform.SetParent(RefCache.RHand.transform, false);
      this.point = ((Component) this.holdable.transform.GetChild(0)).gameObject;
      this._hammerSource = this.holdable.GetComponent<AudioSource>();
      this._velEstimator = this.point.AddComponent<GorillaVelocityEstimator>();
    }
    else if (!this.editMode && Object.op_Inequality((Object) this.holdable, (Object) null))
    {
      Object.Destroy((Object) this.holdable);
    }
    else
    {
      Vector3 angularVelocity;
      int num1;
      if (this.editMode && Object.op_Inequality((Object) this.holdable, (Object) null) && (double) Time.time > (double) this.lastTime + 0.25)
      {
        angularVelocity = this._velEstimator.angularVelocity;
        num1 = (double) ((Vector3) ref angularVelocity).magnitude > 4.0 ? 1 : 0;
      }
      else
        num1 = 0;
      if (num1 == 0)
        return;
      foreach (Collider collider in Physics.OverlapSphere(this.point.transform.position, 0.07f))
      {
        Rigidbody componentInParent = ((Component) collider).GetComponentInParent<Rigidbody>();
        if (Object.op_Implicit((Object) componentInParent) && ((Object) collider).name.Contains("MonoObject"))
        {
          this.lastTime = Time.time;
          Rigidbody rigidbody = componentInParent;
          angularVelocity = this._velEstimator.angularVelocity;
          double num2 = 1800.0 * (double) Mathf.Clamp(((Vector3) ref angularVelocity).magnitude * 1.8f, 1.25f, 3f);
          Vector3 vector3 = Vector3.Lerp(((Component) collider).transform.position, this.holdable.transform.position, 0.4f);
          rigidbody.AddExplosionForce((float) num2, vector3, 10f);
          HapticManager.Haptic(HapticManager.HapticType.Use);
          AudioSource hammerSource = this._hammerSource;
          angularVelocity = this._velEstimator.angularVelocity;
          double num3 = (double) Mathf.Clamp(((Vector3) ref angularVelocity).magnitude / 20f, 0.9f, 1f);
          hammerSource.pitch = (float) num3;
          this._hammerSource.Play();
          ((Component) componentInParent).GetComponent<BombDetonate>()?.Explode();
          ((Component) componentInParent).GetComponent<Explode>()?.ExplodeObject();
        }
      }
    }
  }
}
