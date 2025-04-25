// Decompiled with JetBrains decompiler
// Type: CrateManager
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using MonoSandbox.Behaviours;
using UnityEngine;

#nullable disable
public class CrateManager : PlacementHandling
{
  public GameObject Crate;

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
    GameObject gameObject1 = Object.Instantiate<GameObject>(this.Crate);
    Rigidbody rigidbody = gameObject1.AddComponent<Rigidbody>();
    GameObject gameObject2 = new GameObject();
    gameObject2.AddComponent<BoxCollider>();
    gameObject1.layer = 8;
    ((Object) gameObject1).name = ((Object) gameObject1).name + "MonoObject";
    gameObject2.layer = 0;
    gameObject2.transform.SetParent(gameObject1.transform, false);
    gameObject1.transform.SetParent(this.SandboxContainer.transform, false);
    rigidbody.useGravity = true;
    rigidbody.mass = 2.5f;
    gameObject1.transform.forward = ((RaycastHit) ref hitInfo).normal;
    gameObject1.transform.position = Vector3.op_Addition(((RaycastHit) ref hitInfo).point, Vector3.op_Division(gameObject1.transform.forward, 4f));
  }
}
