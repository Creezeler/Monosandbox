// Decompiled with JetBrains decompiler
// Type: BeanManager
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using MonoSandbox;
using MonoSandbox.Behaviours;
using UnityEngine;

#nullable disable
public class BeanManager : PlacementHandling
{
  public bool IsBarrel;
  public bool IsWheel;
  public GameObject Barrel;
  public GameObject Explosion;

  public void Start() => this.Offset = 4.5f;

  public override GameObject CursorRef
  {
    get
    {
      GameObject primitive = GameObject.CreatePrimitive((PrimitiveType) 0);
      primitive.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
      Object.Destroy((Object) primitive.GetComponent<Collider>());
      return primitive;
    }
  }

  public override void DrawCursor(RaycastHit hitInfo)
  {
    base.DrawCursor(hitInfo);
    this.Cursor.transform.position = Vector3.op_Addition(((RaycastHit) ref hitInfo).point, Vector3.op_Division(this.Cursor.transform.up, 4.5f));
    this.Cursor.transform.up = ((RaycastHit) ref hitInfo).normal;
  }

  public override void Activated(RaycastHit hitInfo)
  {
    base.Activated(hitInfo);
    if (this.IsBarrel)
    {
      GameObject gameObject1 = Object.Instantiate<GameObject>(this.Barrel);
      gameObject1.AddComponent<BoxCollider>().size = new Vector3(0.025f, 0.025f, 0.025f);
      Rigidbody rigidbody = gameObject1.AddComponent<Rigidbody>();
      GameObject gameObject2 = new GameObject();
      gameObject2.AddComponent<BoxCollider>().size = new Vector3(0.025f, 0.025f, 0.025f);
      Explode explode = gameObject1.AddComponent<Explode>();
      explode.Multiplier = 4f;
      explode.Explosion = this.Explosion;
      gameObject1.layer = 8;
      ((Object) gameObject1).name = ((Object) gameObject1).name + "MonoObject";
      gameObject2.layer = 0;
      gameObject2.transform.SetParent(gameObject1.transform, false);
      gameObject1.transform.SetParent(this.SandboxContainer.transform, false);
      rigidbody.useGravity = true;
      rigidbody.mass = 3.5f;
      gameObject1.transform.up = ((RaycastHit) ref hitInfo).normal;
      gameObject1.transform.position = Vector3.op_Addition(((RaycastHit) ref hitInfo).point, Vector3.op_Division(gameObject1.transform.up, 2.5f));
      gameObject1.transform.localScale = new Vector3(15f, 15f, 15f);
    }
    else if (this.IsWheel)
    {
      GameObject primitive = GameObject.CreatePrimitive((PrimitiveType) 2);
      primitive.transform.localScale = new Vector3(0.3f, 0.05f, 0.3f);
      Rigidbody rigidbody = primitive.AddComponent<Rigidbody>();
      GameObject gameObject = new GameObject();
      gameObject.AddComponent<BoxCollider>();
      primitive.layer = 8;
      ((Object) primitive).name = ((Object) primitive).name + "MonoObject";
      gameObject.layer = 0;
      gameObject.transform.SetParent(primitive.transform, false);
      primitive.transform.SetParent(this.SandboxContainer.transform, false);
      rigidbody.useGravity = true;
      rigidbody.mass = 3.5f;
      primitive.transform.up = ((RaycastHit) ref hitInfo).normal;
      primitive.transform.position = Vector3.op_Addition(((RaycastHit) ref hitInfo).point, Vector3.op_Division(primitive.transform.up, 2.5f));
      primitive.GetComponent<Renderer>().material = RefCache.Default;
    }
    else
    {
      GameObject primitive = GameObject.CreatePrimitive((PrimitiveType) 1);
      Rigidbody rigidbody = primitive.AddComponent<Rigidbody>();
      GameObject gameObject = new GameObject();
      gameObject.AddComponent<CapsuleCollider>().height = 2f;
      primitive.layer = 8;
      ((Object) primitive).name = ((Object) primitive).name + "MonoObject";
      gameObject.layer = 0;
      gameObject.transform.SetParent(primitive.transform, false);
      primitive.transform.SetParent(this.SandboxContainer.transform, false);
      rigidbody.useGravity = true;
      rigidbody.mass = 3.5f;
      primitive.transform.up = ((RaycastHit) ref hitInfo).normal;
      primitive.transform.position = Vector3.op_Addition(((RaycastHit) ref hitInfo).point, Vector3.op_Division(primitive.transform.up, 2.5f));
      primitive.GetComponent<Renderer>().material = RefCache.Default;
      primitive.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }
  }
}
