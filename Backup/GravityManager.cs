// Decompiled with JetBrains decompiler
// Type: GravityManager
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using MonoSandbox;
using MonoSandbox.Behaviours;
using UnityEngine;

#nullable disable
public class GravityManager : MonoBehaviour
{
  private bool primaryDown;
  private bool canPlace;
  public bool editMode = false;
  private GameObject Cursor = (GameObject) null;

  private void Update()
  {
    RaycastHit hit = RefCache.Hit;
    if (Object.op_Inequality((Object) this.Cursor, (Object) null))
    {
      bool flag = ((Object) ((Component) ((RaycastHit) ref hit).transform).gameObject).name.Contains("MonoObject") && Object.op_Inequality((Object) ((RaycastHit) ref hit).collider, (Object) null) && Object.op_Inequality((Object) ((RaycastHit) ref hit).collider.attachedRigidbody, (Object) null);
      this.Cursor.GetComponent<Renderer>().material.color = flag ? new Color(0.392f, 0.722f, 0.82f, 0.4509804f) : new Color(0.8314f, 0.2471f, 0.1569f, 0.4509804f);
      this.Cursor.transform.position = ((RaycastHit) ref hit).point;
      this.primaryDown = InputHandling.RightPrimary;
      if (this.primaryDown)
      {
        if (this.canPlace && ((Object) ((Component) ((RaycastHit) ref hit).transform).gameObject).name.Contains("MonoObject") && Object.op_Inequality((Object) ((RaycastHit) ref hit).collider, (Object) null) && Object.op_Inequality((Object) ((RaycastHit) ref hit).collider.attachedRigidbody, (Object) null))
        {
          Rigidbody attachedRigidbody = ((RaycastHit) ref hit).collider.attachedRigidbody;
          attachedRigidbody.useGravity = !attachedRigidbody.useGravity;
          HapticManager.Haptic(HapticManager.HapticType.Create);
          this.canPlace = false;
        }
      }
      else
        this.canPlace = true;
    }
    else if (this.editMode)
    {
      this.Cursor = GameObject.CreatePrimitive((PrimitiveType) 0);
      this.Cursor.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
      this.Cursor.GetComponent<Renderer>().material = new Material(RefCache.Selection);
      Object.Destroy((Object) this.Cursor.GetComponent<SphereCollider>());
    }
    else if (Object.op_Implicit((Object) this.Cursor))
      Object.Destroy((Object) this.Cursor.gameObject);
    if (this.editMode || !Object.op_Implicit((Object) this.Cursor))
      return;
    Object.Destroy((Object) this.Cursor.gameObject);
  }
}
