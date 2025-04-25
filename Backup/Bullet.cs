// Decompiled with JetBrains decompiler
// Type: Bullet
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using MonoSandbox.Behaviours;
using UnityEngine;

#nullable disable
public class Bullet : MonoBehaviour
{
  public int gunIndex;
  private bool hasCollided;
  public float bulletSpeed = 10f;
  public GameObject melonExplode;
  private GameObject exploded;

  private void Update()
  {
    if (this.gunIndex != 0)
      return;
    ((Component) this).gameObject.transform.Translate(Vector3.op_Multiply(Vector3.op_Multiply(Vector3.op_UnaryNegation(((Component) this).transform.right), Time.deltaTime), this.bulletSpeed));
  }

  private void OnCollisionEnter(Collision other)
  {
    if (this.hasCollided)
      return;
    this.hasCollided = true;
    if (this.gunIndex == 2)
    {
      ((Component) other.transform).GetComponent<Enemy>()?.Damage(2f, 5f, 3f);
      ((Component) this).gameObject.SetActive(false);
      this.exploded = Object.Instantiate<GameObject>(this.melonExplode);
      this.exploded.transform.position = ((Component) this).transform.position;
      foreach (Transform transform in this.exploded.transform)
      {
        ((Component) transform).GetComponent<Rigidbody>().velocity = Vector3.op_Multiply(Vector3.op_Subtraction(transform.position, ((Component) this).transform.position), 50f);
        ((Component) transform).gameObject.AddComponent<SineScaleOut>().Delay = 2f;
      }
      this.Invoke("DestroyMelon", 4.5f);
    }
    if (this.gunIndex == 0)
    {
      ((Component) other.transform).GetComponent<Enemy>()?.Damage(2f, 5f, 3f);
      foreach (Component component1 in Physics.OverlapSphere(((Component) this).transform.position, 0.5f))
      {
        Rigidbody component2 = component1.GetComponent<Rigidbody>();
        if (Object.op_Inequality((Object) component2, (Object) null) && component2.useGravity)
          component2.AddExplosionForce(150f, ((Component) this).transform.position, 1f);
      }
      Object.Destroy((Object) ((Component) this).gameObject);
    }
  }

  private void DestroyMelon()
  {
    Object.Destroy((Object) this.exploded);
    Object.Destroy((Object) ((Component) this).gameObject);
  }
}
