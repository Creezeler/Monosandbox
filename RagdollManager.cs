// Decompiled with JetBrains decompiler
// Type: RagdollManager
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using GorillaTag;
using MonoSandbox.Behaviours;
using UnityEngine;

#nullable disable
public class RagdollManager : PlacementHandling
{
  public bool UseGorilla;
  public GameObject Gorilla;
  public GameObject Body;

  public void Start() => this.Offset = 4.5f;

  public override GameObject CursorRef
  {
    get
    {
      GameObject primitive = GameObject.CreatePrimitive((PrimitiveType) 2);
      primitive.transform.localScale = new Vector3(0.4f, 0.3f, 0.4f);
      Object.Destroy((Object) primitive.GetComponent<Collider>());
      return primitive;
    }
  }

  public override void DrawCursor(RaycastHit hitInfo)
  {
    base.DrawCursor(hitInfo);
    this.Cursor.transform.position = Vector3.op_Addition(((RaycastHit) ref hitInfo).point, Vector3.op_Multiply(Vector3.up, 0.15f));
  }

  public override void Activated(RaycastHit hitInfo)
  {
    base.Activated(hitInfo);
    if (this.UseGorilla)
    {
      GameObject gameObject1 = Object.Instantiate<GameObject>(this.Gorilla);
      GameObject gameObject2 = gameObject1;
      ((Object) gameObject2).name = ((Object) gameObject2).name + "MonoObject_Ragdoll";
      gameObject1.transform.SetParent(this.SandboxContainer.transform, false);
      foreach (Transform componentsInChild in ((Component) gameObject1.transform.GetChild(1)).GetComponentsInChildren<Transform>())
      {
        ((Component) componentsInChild).gameObject.layer = 8;
        ((Object) componentsInChild).name = ((Object) componentsInChild).name + "MonoObject";
      }
      gameObject1.transform.position = Vector3.op_Addition(((RaycastHit) ref hitInfo).point, new Vector3(0.0f, 0.45f, 0.0f));
      GTColor.HSVRanges hsvRanges;
      // ISSUE: explicit constructor call
      ((GTColor.HSVRanges) ref hsvRanges).\u002Ector(0.0f, 1f, 0.8f, 0.6f, 1f, 1f);
      Material material = new Material(((Renderer) ((Component) gameObject1.transform.GetChild(0)).GetComponent<SkinnedMeshRenderer>()).material)
      {
        color = GTColor.RandomHSV(hsvRanges)
      };
      ((Renderer) gameObject1.GetComponentInChildren<SkinnedMeshRenderer>()).material = material;
    }
    else
    {
      GameObject gameObject3 = Object.Instantiate<GameObject>(this.Body);
      GameObject gameObject4 = gameObject3;
      ((Object) gameObject4).name = ((Object) gameObject4).name + "MonoObject_Ragdoll";
      gameObject3.transform.SetParent(this.SandboxContainer.transform, false);
      foreach (Transform componentsInChild in ((Component) gameObject3.transform.GetChild(0)).GetComponentsInChildren<Transform>())
      {
        ((Component) componentsInChild).gameObject.layer = 8;
        ((Object) componentsInChild).name = ((Object) componentsInChild).name + "MonoObject";
      }
      gameObject3.transform.position = Vector3.op_Addition(((RaycastHit) ref hitInfo).point, new Vector3(0.0f, 0.6f, 0.0f));
      gameObject3.transform.localScale = new Vector3(0.4f, 0.4f, 0.5f);
      ((Component) gameObject3.transform.GetChild(1)).GetComponent<Renderer>().material.color = Color.grey;
      Object.Destroy((Object) gameObject3.GetComponent<MeshCollider>());
    }
  }
}
