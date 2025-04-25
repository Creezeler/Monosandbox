// Decompiled with JetBrains decompiler
// Type: CouchManager
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using MonoSandbox.Behaviours;
using UnityEngine;

#nullable disable
public class CouchManager : PlacementHandling
{
  public GameObject Couch;

  public override GameObject CursorRef
  {
    get
    {
      GameObject primitive = GameObject.CreatePrimitive((PrimitiveType) 3);
      primitive.transform.localScale = new Vector3(1.25f, 0.6f, 0.55f);
      Object.Destroy((Object) primitive.GetComponent<Collider>());
      return primitive;
    }
  }

  public override void DrawCursor(RaycastHit hitInfo)
  {
    base.DrawCursor(hitInfo);
    this.Cursor.transform.position = Vector3.op_Addition(((RaycastHit) ref hitInfo).point, Vector3.op_Multiply(Vector3.up, 0.4f));
  }

  public override void Activated(RaycastHit hitInfo)
  {
    base.Activated(hitInfo);
    GameObject gameObject = Object.Instantiate<GameObject>(this.Couch);
    Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
    gameObject.layer = 8;
    gameObject.AddComponent<BoxCollider>();
    ((Object) gameObject).name = ((Object) gameObject).name + "MonoObject";
    gameObject.transform.SetParent(this.SandboxContainer.transform, false);
    rigidbody.useGravity = true;
    rigidbody.mass = 8f;
    gameObject.transform.position = Vector3.op_Addition(((RaycastHit) ref hitInfo).point, Vector3.op_Multiply(Vector3.up, 0.4f));
    gameObject.transform.localScale = new Vector3(100f, 100f, 100f);
  }
}
