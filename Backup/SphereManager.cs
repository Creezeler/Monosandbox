// Decompiled with JetBrains decompiler
// Type: SphereManager
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using MonoSandbox;
using MonoSandbox.Behaviours;
using UnityEngine;

#nullable disable
public class SphereManager : PlacementHandling
{
  public bool IsEnemy;
  public bool IsSoftbody;
  public GameObject Softbody;
  public GameObject Entity;

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
    this.Cursor.transform.position = Vector3.op_Addition(((RaycastHit) ref hitInfo).point, Vector3.op_Division(this.Cursor.transform.forward, 4f));
    this.Cursor.transform.forward = ((RaycastHit) ref hitInfo).normal;
  }

  public override void Activated(RaycastHit hitInfo)
  {
    base.Activated(hitInfo);
    if (!this.IsSoftbody && !this.IsEnemy)
    {
      GameObject primitive = GameObject.CreatePrimitive((PrimitiveType) 0);
      Rigidbody rigidbody = primitive.AddComponent<Rigidbody>();
      GameObject gameObject = new GameObject();
      gameObject.AddComponent<SphereCollider>();
      primitive.layer = 8;
      ((Object) primitive).name = ((Object) primitive).name + "MonoObject";
      gameObject.layer = 0;
      gameObject.transform.SetParent(primitive.transform, false);
      primitive.transform.SetParent(this.SandboxContainer.transform, false);
      rigidbody.useGravity = true;
      rigidbody.mass = 3.5f;
      primitive.transform.forward = ((RaycastHit) ref hitInfo).normal;
      primitive.transform.position = Vector3.op_Addition(((RaycastHit) ref hitInfo).point, Vector3.op_Division(primitive.transform.forward, 4f));
      primitive.GetComponent<Renderer>().material = RefCache.Default;
      primitive.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }
    else if (this.IsSoftbody && !this.IsEnemy)
    {
      GameObject gameObject = Object.Instantiate<GameObject>(this.Softbody);
      gameObject.layer = 8;
      ((Object) gameObject).name = ((Object) gameObject).name + "MonoObject";
      gameObject.transform.SetParent(this.SandboxContainer.transform, false);
      foreach (Transform componentInChild in ((Component) gameObject.transform.GetChild(0)).GetComponentInChildren<Transform>())
        ((Object) componentInChild).name = ((Object) componentInChild).name + "MonoObject";
      gameObject.transform.forward = ((RaycastHit) ref hitInfo).normal;
      gameObject.transform.position = Vector3.op_Addition(((RaycastHit) ref hitInfo).point, Vector3.op_Division(gameObject.transform.forward, 4f));
      ((Component) gameObject.transform.GetChild(1)).GetComponent<Renderer>().material = RefCache.Default;
      gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
      gameObject.AddComponent<BoneSphere>();
    }
    else
    {
      if (this.IsSoftbody || !this.IsEnemy)
        return;
      GameObject gameObject = Object.Instantiate<GameObject>(this.Entity);
      ((Object) gameObject).name = "MonoObject";
      gameObject.AddComponent<SphereCollider>();
      Enemy enemy = gameObject.AddComponent<Enemy>();
      enemy.Health = 40f;
      enemy.Defence = 1.75f;
      gameObject.transform.SetParent(this.SandboxContainer.transform, false);
      gameObject.transform.position = Vector3.op_Addition(((RaycastHit) ref hitInfo).point, Vector3.op_Division(Vector3.up, 2f));
    }
  }
}
