// Decompiled with JetBrains decompiler
// Type: SpringManager
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using MonoSandbox;
using MonoSandbox.Behaviours;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SpringManager : MonoBehaviour
{
  public List<GameObject> objectList = new List<GameObject>();
  public bool primaryDown;
  public bool canPlace;
  public bool editMode;
  public bool BasePlaced;
  public RaycastHit baseHit;
  public SpringJoint joint;
  public GameObject Cursor;

  public void Update()
  {
    RaycastHit hit = RefCache.Hit;
    if (Object.op_Inequality((Object) this.Cursor, (Object) null))
    {
      this.Cursor.transform.position = ((RaycastHit) ref hit).point;
      bool flag = ((Object) ((Component) ((RaycastHit) ref hit).transform).gameObject).name.Contains("MonoObject");
      this.Cursor.GetComponent<Renderer>().material.color = flag ? new Color(0.392f, 0.722f, 0.82f, 0.4509804f) : new Color(0.8314f, 0.2471f, 0.1569f, 0.4509804f);
      this.primaryDown = InputHandling.RightPrimary;
      if (this.primaryDown)
      {
        if (this.canPlace & flag && RefCache.HitExists)
        {
          PlaceJoint(hit);
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

    void PlaceJoint(RaycastHit hit)
    {
      if (!this.BasePlaced)
      {
        this.baseHit = hit;
        this.BasePlaced = true;
      }
      else
      {
        foreach (Object @object in ((RaycastHit) ref this.baseHit).transform)
        {
          if (@object.name.Contains("MSJoint"))
            return;
        }
        GameObject gameObject = new GameObject();
        ((Object) gameObject).name = "MSJoint MonoObject";
        gameObject.transform.SetParent(((RaycastHit) ref this.baseHit).transform, false);
        this.objectList.Add(gameObject);
        ((Joint) gameObject.AddComponent<FixedJoint>()).connectedBody = ((Component) ((RaycastHit) ref this.baseHit).transform).GetComponent<Rigidbody>();
        this.joint = ((Component) gameObject.transform).gameObject.AddComponent<SpringJoint>();
        this.joint.minDistance = Vector3.Distance(((RaycastHit) ref this.baseHit).transform.position, ((RaycastHit) ref hit).transform.position) - 1f;
        this.joint.damper = 30f;
        this.joint.spring = 10f;
        ((Joint) this.joint).massScale = 12f;
        ((Joint) this.joint).autoConfigureConnectedAnchor = false;
        ((Joint) this.joint).connectedBody = ((Component) ((RaycastHit) ref hit).transform).GetComponent<Rigidbody>();
        LineRenderer lineRenderer = gameObject.gameObject.AddComponent<LineRenderer>();
        ((Renderer) lineRenderer).material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.startWidth = 0.012f;
        lineRenderer.endWidth = 0.012f;
        lineRenderer.positionCount = 2;
        SpringLine springLine = gameObject.AddComponent<SpringLine>();
        springLine.makeLine = false;
        springLine.lineRenderer = lineRenderer;
        springLine.pointone = gameObject;
        springLine.pointtwo = ((Component) ((RaycastHit) ref hit).transform).gameObject;
        HapticManager.Haptic(HapticManager.HapticType.Create);
        this.BasePlaced = false;
      }
    }
  }
}
