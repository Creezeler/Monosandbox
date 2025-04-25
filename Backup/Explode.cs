// Decompiled with JetBrains decompiler
// Type: Explode
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using GorillaLocomotion;
using UnityEngine;

#nullable disable
public class Explode : MonoBehaviour
{
  public GameObject Explosion;
  public float Multiplier;
  private bool Exploding = true;

  public void ExplodeObject()
  {
    if (!this.Exploding)
      return;
    this.Exploding = false;
    ((Component) this).gameObject.GetComponent<AudioSource>().Play();
    ((Component) this).gameObject.GetComponent<Renderer>().enabled = false;
    ((Collider) ((Component) this).gameObject.GetComponent<BoxCollider>()).enabled = false;
    GameObject gameObject = Object.Instantiate<GameObject>(this.Explosion);
    gameObject.transform.SetParent(((Component) this).transform);
    gameObject.transform.localPosition = Vector3.zero;
    gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    foreach (Collider collider in Physics.OverlapSphere(((Component) this).transform.position, 6f))
    {
      ((Component) collider).GetComponent<Rigidbody>()?.AddExplosionForce(1500f * this.Multiplier, ((Component) this).transform.position, 8f);
      ((Component) collider).GetComponent<BombDetonate>()?.Explode();
      ((Component) collider).GetComponent<MineDetonate>()?.Explode();
      ((Component) collider).GetComponent<Explode>()?.ExplodeObject();
    }
    Rigidbody component = ((Component) GTPlayer.Instance).GetComponent<Rigidbody>();
    component.AddExplosionForce(2500f * this.Multiplier * Mathf.Sqrt(component.mass), ((Component) this).transform.position, (float) (5.0 + 0.75 * (double) this.Multiplier));
    this.Invoke("Destroy", 3f);
  }

  private void Delete() => Object.Destroy((Object) ((Component) this).gameObject);
}
