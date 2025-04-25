// Decompiled with JetBrains decompiler
// Type: MineDetonate
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using GorillaLocomotion;
using System.Threading.Tasks;
using UnityEngine;

#nullable disable
public class MineDetonate : MonoBehaviour
{
  public float Multiplier = 4f;
  public float Radius = 10f;
  public GameObject Explosion;
  private bool _canExplode = true;

  public void OnCollisionEnter(Collision collision)
  {
    if (!Object.op_Inequality((Object) collision.collider, (Object) null) || !Object.op_Inequality((Object) collision.collider.attachedRigidbody, (Object) null))
      return;
    this.Explode();
  }

  public async void Explode()
  {
    GameObject ExplodeOBJ;
    Collider[] colliders;
    Collider[] bombs;
    Rigidbody PlayerRigidbody;
    if (!this._canExplode)
    {
      ExplodeOBJ = (GameObject) null;
      colliders = (Collider[]) null;
      bombs = (Collider[]) null;
      PlayerRigidbody = (Rigidbody) null;
    }
    else
    {
      this._canExplode = false;
      ((Component) ((Component) this).transform.GetChild(0)).GetComponent<AudioSource>().Play();
      await Task.Delay(Mathf.RoundToInt(((Component) ((Component) this).transform.GetChild(0)).GetComponent<AudioSource>().clip.length * 750f));
      ((Component) this).gameObject.GetComponent<AudioSource>().Play();
      ((Component) this).gameObject.GetComponent<Renderer>().enabled = false;
      ((Collider) ((Component) this).gameObject.GetComponent<BoxCollider>()).enabled = false;
      ExplodeOBJ = Object.Instantiate<GameObject>(this.Explosion);
      ExplodeOBJ.transform.SetParent(((Component) this).transform);
      ExplodeOBJ.transform.localPosition = Vector3.zero;
      ExplodeOBJ.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
      colliders = Physics.OverlapSphere(((Component) this).transform.position, this.Radius);
      Collider[] colliderArray1 = colliders;
      for (int index = 0; index < colliderArray1.Length; ++index)
      {
        Collider nearyby = colliderArray1[index];
        Rigidbody rig = ((Component) nearyby).GetComponent<Rigidbody>();
        rig?.AddExplosionForce(2500f * this.Multiplier, ((Component) this).transform.position, 8f);
        rig = (Rigidbody) null;
        nearyby = (Collider) null;
      }
      colliderArray1 = (Collider[]) null;
      bombs = Physics.OverlapSphere(((Component) this).transform.position, this.Radius);
      Collider[] colliderArray2 = bombs;
      for (int index = 0; index < colliderArray2.Length; ++index)
      {
        Collider nearyby = colliderArray2[index];
        ((Component) nearyby).GetComponent<BombDetonate>()?.Explode();
        ((Component) nearyby).GetComponent<MineDetonate>()?.Explode();
        ((Component) nearyby).GetComponent<global::Explode>()?.ExplodeObject();
        nearyby = (Collider) null;
      }
      colliderArray2 = (Collider[]) null;
      PlayerRigidbody = ((Component) GTPlayer.Instance).GetComponent<Rigidbody>();
      PlayerRigidbody.AddExplosionForce(2500f * this.Multiplier * Mathf.Sqrt(PlayerRigidbody.mass), ((Component) this).transform.position, (float) (5.0 + 0.75 * (double) this.Multiplier));
      Object.Destroy((Object) ((Component) this).gameObject, 3f);
      ExplodeOBJ = (GameObject) null;
      colliders = (Collider[]) null;
      bombs = (Collider[]) null;
      PlayerRigidbody = (Rigidbody) null;
    }
  }
}
