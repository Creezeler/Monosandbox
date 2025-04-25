// Decompiled with JetBrains decompiler
// Type: BombDetonate
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using GorillaLocomotion;
using MonoSandbox.Behaviours;
using UnityEngine;

#nullable disable
public class BombDetonate : MonoBehaviour
{
  private bool secButtonDown;
  private bool primaryDown;
  private bool canExplode = true;
  public bool useDefault = true;
  public float multiplier = 4f;
  public float Radius = 10f;
  public GameObject ExplosionOBJ;

  private void Update()
  {
    this.secButtonDown = InputHandling.RightSecondary;
    this.primaryDown = InputHandling.RightPrimary;
    if (!this.secButtonDown || !this.canExplode)
      return;
    this.Explode();
  }

  public void Explode()
  {
    if (!this.canExplode)
      return;
    this.canExplode = false;
    ((Component) this).gameObject.GetComponent<AudioSource>().Play();
    ((Component) this).gameObject.GetComponent<Renderer>().enabled = false;
    ((Collider) ((Component) this).gameObject.GetComponent<BoxCollider>()).enabled = false;
    GameObject gameObject = Object.Instantiate<GameObject>(this.ExplosionOBJ);
    gameObject.transform.SetParent(((Component) this).transform);
    gameObject.transform.localPosition = Vector3.zero;
    gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    foreach (Component component in Physics.OverlapSphere(((Component) this).transform.position, this.Radius))
      component.GetComponent<Rigidbody>()?.AddExplosionForce(1500f * this.multiplier, ((Component) this).transform.position, 8f);
    foreach (Collider collider in Physics.OverlapSphere(((Component) this).transform.position, this.Radius))
    {
      ((Component) collider).GetComponent<BombDetonate>()?.Explode();
      ((Component) collider).GetComponent<MineDetonate>()?.Explode();
      ((Component) collider).GetComponent<global::Explode>()?.ExplodeObject();
    }
    Rigidbody component1 = ((Component) GTPlayer.Instance).GetComponent<Rigidbody>();
    component1.AddExplosionForce(1500f * this.multiplier * Mathf.Sqrt(component1.mass), ((Component) this).transform.position, (float) (5.0 + 0.75 * (double) this.multiplier));
    this.Invoke("Delete", 3f);
  }

  private void Delete() => Object.Destroy((Object) ((Component) this).gameObject);
}
