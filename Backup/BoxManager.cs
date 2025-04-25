// Decompiled with JetBrains decompiler
// Type: BoxManager
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using MonoSandbox;
using MonoSandbox.Behaviours;
using UnityEngine;

#nullable disable
public class BoxManager : PlacementHandling
{
  public bool IsPlane;

  public override GameObject CursorRef
  {
    get
    {
      GameObject primitive = GameObject.CreatePrimitive((PrimitiveType) 3);
      primitive.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
      Object.Destroy((Object) primitive.GetComponent<Collider>());
      return primitive;
    }
  }

  public override void DrawCursor(RaycastHit hitInfo)
  {
    base.DrawCursor(hitInfo);
    this.Cursor.transform.position = Vector3.op_Addition(((RaycastHit) ref hitInfo).point, Vector3.op_Division(this.Cursor.transform.forward, 4f));
    this.Cursor.transform.forward = ((RaycastHit) ref hitInfo).normal;
  }

  public override void Activated(RaycastHit hitInfo)
  {
    base.Activated(hitInfo);
    if (!this.IsPlane)
    {
      GameObject primitive = GameObject.CreatePrimitive((PrimitiveType) 3);
      GameObject gameObject = new GameObject();
      gameObject.AddComponent<BoxCollider>();
      primitive.layer = 8;
      ((Object) primitive).name = ((Object) primitive).name + "MonoObject";
      gameObject.layer = 0;
      gameObject.transform.SetParent(primitive.transform, false);
      primitive.transform.SetParent(this.SandboxContainer.transform, false);
      Rigidbody rigidbody = primitive.AddComponent<Rigidbody>();
      rigidbody.useGravity = true;
      rigidbody.mass = 2.5f;
      primitive.transform.forward = ((RaycastHit) ref hitInfo).normal;
      primitive.transform.position = Vector3.op_Addition(((RaycastHit) ref hitInfo).point, Vector3.op_Division(primitive.transform.forward, 4f));
      primitive.GetComponent<Renderer>().material = RefCache.Default;
      primitive.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }
    else
    {
      GameObject primitive = GameObject.CreatePrimitive((PrimitiveType) 3);
      GameObject gameObject = new GameObject();
      gameObject.AddComponent<BoxCollider>();
      primitive.layer = 8;
      ((Object) primitive).name = ((Object) primitive).name + "MonoObject";
      gameObject.layer = 0;
      gameObject.transform.SetParent(primitive.transform, false);
      primitive.transform.SetParent(this.SandboxContainer.transform, false);
      Rigidbody rigidbody = primitive.AddComponent<Rigidbody>();
      rigidbody.useGravity = true;
      rigidbody.mass = 2.5f;
      primitive.transform.forward = ((RaycastHit) ref hitInfo).normal;
      primitive.transform.position = Vector3.op_Addition(((RaycastHit) ref hitInfo).point, Vector3.op_Division(primitive.transform.forward, 4f));
      primitive.GetComponent<Renderer>().material = RefCache.Default;
      primitive.transform.localScale = new Vector3(0.6f, 0.6f, 0.1f);
    }
  }

  public void LateUpdate()
  {
    if (!Object.op_Inequality((Object) this.Cursor, (Object) null))
      return;
    this.Cursor.transform.localScale = this.IsPlane ? new Vector3(0.6f, 0.6f, 0.1f) : new Vector3(0.4f, 0.4f, 0.4f);
  }
}
