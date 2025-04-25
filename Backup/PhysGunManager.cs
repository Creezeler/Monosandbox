// Decompiled with JetBrains decompiler
// Type: PhysGunManager
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using MonoSandbox;
using MonoSandbox.Behaviours;
using UnityEngine;
using UnityEngine.XR;

#nullable disable
public class PhysGunManager : MonoBehaviour
{
  private float trigger;
  private bool primary;
  private bool canFreeze;
  private bool shooting;
  public bool editMode;
  public bool inRoom;
  private Vector2 joystick;
  private FixedJoint joint;
  private GameObject BasePoint;
  private GameObject Cursor;
  private GameObject itemsFolder;
  private GameObject rightHand;

  private void Start()
  {
    this.itemsFolder = ((Component) this).gameObject;
    this.rightHand = RefCache.RHand;
  }

  private void Update()
  {
    RaycastHit hit = RefCache.Hit;
    if (Object.op_Equality((Object) this.itemsFolder, (Object) null))
    {
      this.itemsFolder = ((Component) this).gameObject;
      this.rightHand = RefCache.RHand;
    }
    if (this.editMode)
    {
      if (Object.op_Equality((Object) this.Cursor, (Object) null))
      {
        this.Cursor = GameObject.CreatePrimitive((PrimitiveType) 0);
        this.Cursor.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        this.Cursor.GetComponent<Renderer>().material = new Material(RefCache.Selection);
        Object.Destroy((Object) this.Cursor.GetComponent<SphereCollider>());
      }
      this.Cursor.transform.position = ((RaycastHit) ref hit).point;
      this.trigger = InputHandling.RightTrigger;
      this.primary = InputHandling.RightPrimary;
      this.joystick = ControllerInputPoller.Primary2DAxis((XRNode) 5);
      this.Cursor.GetComponent<Renderer>().material.color = !Object.op_Inequality((Object) ((RaycastHit) ref hit).collider, (Object) null) || !Object.op_Inequality((Object) ((RaycastHit) ref hit).collider.attachedRigidbody, (Object) null) || !((Object) ((Component) ((RaycastHit) ref hit).transform).gameObject).name.Contains("MonoObject") ? new Color(1f, 0.0f, 0.0f, 0.4509804f) : new Color(0.0f, 1f, 1f, 0.4509804f);
      if (Object.op_Equality((Object) this.BasePoint, (Object) null))
      {
        this.BasePoint = Object.Instantiate<GameObject>(new GameObject());
        this.BasePoint.transform.SetParent(this.rightHand.transform, false);
        this.joint = this.BasePoint.AddComponent<FixedJoint>();
        ((Joint) this.joint).autoConfigureConnectedAnchor = true;
      }
      if (Object.op_Inequality((Object) this.BasePoint.GetComponent<Rigidbody>(), (Object) null) && this.BasePoint.GetComponent<Rigidbody>().useGravity)
      {
        this.BasePoint.GetComponent<Rigidbody>().constraints = (RigidbodyConstraints) 126;
        this.BasePoint.GetComponent<Rigidbody>().useGravity = false;
      }
      if ((double) this.trigger > 0.60000002384185791)
      {
        this.Cursor.GetComponent<Renderer>().enabled = false;
        if (Object.op_Inequality((Object) ((RaycastHit) ref hit).collider, (Object) null) && Object.op_Inequality((Object) ((RaycastHit) ref hit).collider.attachedRigidbody, (Object) null) && ((Object) ((Component) ((RaycastHit) ref hit).transform).gameObject).name.Contains("MonoObject") && !this.shooting)
        {
          if (Object.op_Inequality((Object) ((RaycastHit) ref hit).rigidbody, (Object) null))
          {
            ((Joint) this.joint).connectedBody = ((RaycastHit) ref hit).collider.attachedRigidbody;
            ((Joint) this.joint).connectedBody.constraints = (RigidbodyConstraints) 0;
            ((Joint) this.joint).connectedMassScale = 1f;
          }
          this.shooting = true;
        }
        if (this.primary)
        {
          if (this.canFreeze && Object.op_Inequality((Object) ((Joint) this.joint).connectedBody, (Object) null))
          {
            if (((Component) ((Joint) this.joint).connectedBody).GetComponent<Rigidbody>().constraints == 0)
              ((Component) ((Joint) this.joint).connectedBody).GetComponent<Rigidbody>().constraints = (RigidbodyConstraints) 126;
            else
              ((Joint) this.joint).connectedBody.constraints = (RigidbodyConstraints) 0;
            this.canFreeze = false;
          }
        }
        else
          this.canFreeze = true;
        if ((double) this.joystick.y <= 0.10000000149011612 && (double) this.joystick.y >= -0.10000000149011612)
          return;
        this.BasePoint.transform.localPosition = new Vector3(this.BasePoint.transform.localPosition.x, this.BasePoint.transform.localPosition.y + 1f * Time.deltaTime * this.joystick.y, this.BasePoint.transform.localPosition.z);
      }
      else
      {
        this.Cursor.GetComponent<Renderer>().enabled = true;
        this.shooting = false;
        if (Object.op_Inequality((Object) ((Joint) this.joint).connectedBody, (Object) null))
        {
          ((Joint) this.joint).connectedBody = (Rigidbody) null;
          this.BasePoint.transform.localPosition = Vector3.zero;
        }
      }
    }
    else if (Object.op_Inequality((Object) this.Cursor, (Object) null))
    {
      Object.Destroy((Object) this.Cursor);
      ((Joint) this.joint).connectedBody = (Rigidbody) null;
    }
  }

  private void Leave()
  {
  }
}
