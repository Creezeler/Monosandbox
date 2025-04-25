// Decompiled with JetBrains decompiler
// Type: BoneSphere
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using UnityEngine;

#nullable disable
public class BoneSphere : MonoBehaviour
{
  [Header("Bones")]
  public GameObject root = (GameObject) null;
  public GameObject x = (GameObject) null;
  public GameObject x2 = (GameObject) null;
  public GameObject y = (GameObject) null;
  public GameObject y2 = (GameObject) null;
  public GameObject z = (GameObject) null;
  public GameObject z2 = (GameObject) null;
  [Header("Spring Joint Settings")]
  [Tooltip("Strength of spring")]
  public float Spring = 800f;
  [Tooltip("Higher the value the faster the spring oscillation stops")]
  public float Damper = 0.2f;
  [Header("Other Settings")]
  public Softbody.ColliderShape Shape = Softbody.ColliderShape.Sphere;
  public float ColliderSize = 1f / 500f;
  public float RigidbodyMass = 0.5f;
  public bool ViewLines = true;

  private void Start()
  {
    this.root = ((Component) ((Component) this).transform.GetChild(0).GetChild(0)).gameObject;
    this.x = ((Component) ((Component) this).transform.GetChild(0).GetChild(1)).gameObject;
    this.x2 = ((Component) ((Component) this).transform.GetChild(0).GetChild(2)).gameObject;
    this.y = ((Component) ((Component) this).transform.GetChild(0).GetChild(3)).gameObject;
    this.y2 = ((Component) ((Component) this).transform.GetChild(0).GetChild(4)).gameObject;
    this.z = ((Component) ((Component) this).transform.GetChild(0).GetChild(5)).gameObject;
    this.z2 = ((Component) ((Component) this).transform.GetChild(0).GetChild(6)).gameObject;
    Softbody.Init(this.Shape, this.ColliderSize, this.RigidbodyMass, this.Spring, this.Damper, (RigidbodyConstraints) 112);
    Softbody.AddCollider(ref this.root, Softbody.ColliderShape.Sphere, 0.005f, 10f);
    Softbody.AddCollider(ref this.x);
    Softbody.AddCollider(ref this.x2);
    Softbody.AddCollider(ref this.y);
    Softbody.AddCollider(ref this.y2);
    Softbody.AddCollider(ref this.z);
    Softbody.AddCollider(ref this.z2);
    Softbody.AddSpring(ref this.x, ref this.root);
    Softbody.AddSpring(ref this.x2, ref this.root);
    Softbody.AddSpring(ref this.y, ref this.root);
    Softbody.AddSpring(ref this.y2, ref this.root);
    Softbody.AddSpring(ref this.z, ref this.root);
    Softbody.AddSpring(ref this.z2, ref this.root);
  }
}
