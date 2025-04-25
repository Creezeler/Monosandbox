// Decompiled with JetBrains decompiler
// Type: Airstrike
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using GorillaLocomotion;
using UnityEngine;

#nullable disable
public class Airstrike : MonoBehaviour
{
  public Vector3 StrikeLocation;
  public GameObject ExplosionOBJ;
  private bool canExplode = true;
  public static float speed = 20f;

  public void Update()
  {
    ((Component) this).transform.position = ((Component) this).transform.position = Vector3.MoveTowards(((Component) this).transform.position, this.StrikeLocation, Airstrike.speed * Time.deltaTime);
    if ((double) Vector3.Distance(((Component) this).transform.position, this.StrikeLocation) >= 0.5 || !this.canExplode)
      return;
    this.Explode();
    this.canExplode = false;
  }

  public void Explode()
  {
    ((Component) this).gameObject.GetComponent<AudioSource>().minDistance = 15f;
    ((Component) this).gameObject.GetComponent<AudioSource>().Play();
    ((Component) this).gameObject.GetComponent<Renderer>().enabled = false;
    foreach (Component component in ((Component) this).transform)
      Object.Destroy((Object) component.gameObject);
    GameObject gameObject = Object.Instantiate<GameObject>(this.ExplosionOBJ);
    gameObject.transform.SetParent(((Component) this).transform);
    gameObject.transform.localPosition = Vector3.zero;
    gameObject.transform.localScale = Vector3.op_Multiply(Vector3.one, 0.3f);
    foreach (Collider collider in Physics.OverlapSphere(((Component) this).transform.position, 10f))
    {
      ((Component) collider).GetComponent<BombDetonate>()?.Explode();
      ((Component) collider).GetComponent<MineDetonate>()?.Explode();
      ((Component) collider).GetComponent<global::Explode>()?.ExplodeObject();
      Rigidbody component = ((Component) collider).GetComponent<Rigidbody>();
      if (Object.op_Inequality((Object) component, (Object) null) && component.useGravity)
        component.AddExplosionForce(14400f, ((Component) this).transform.position, 80f, 0.5f, (ForceMode) 0);
    }
    Rigidbody component1 = ((Component) GTPlayer.Instance).GetComponent<Rigidbody>();
    component1.AddExplosionForce(14400f * Mathf.Sqrt(component1.mass), ((Component) this).transform.position, 80f, 0.5f, (ForceMode) 0);
    this.Invoke("Finish", 2f);
  }

  private void Finish() => Object.Destroy((Object) ((Component) this).gameObject);
}
