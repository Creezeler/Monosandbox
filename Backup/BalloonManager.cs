// Decompiled with JetBrains decompiler
// Type: BalloonManager
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using MonoSandbox;
using MonoSandbox.Behaviours;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class BalloonManager : MonoBehaviour
{
  public List<GameObject> objectList = new List<GameObject>();
  public float balloonPower = 2f;
  public float maxSpeed = 1.5f;
  public bool primaryDown;
  public bool editMode;
  public bool canPlace;
  public GameObject Cursor;
  public GameObject itemsFolder;
  public GameObject Balloon;

  public void Start() => this.itemsFolder = ((Component) this).gameObject;

  public void Update()
  {
    if (this.editMode)
    {
      if (!Object.op_Implicit((Object) this.Cursor))
      {
        this.Cursor = GameObject.CreatePrimitive((PrimitiveType) 0);
        this.Cursor.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        this.Cursor.GetComponent<Renderer>().material = new Material(RefCache.Selection);
        Object.Destroy((Object) this.Cursor.GetComponent<SphereCollider>());
      }
      RaycastHit hit = RefCache.Hit;
      this.Cursor.transform.position = ((RaycastHit) ref hit).point;
      bool flag = Object.op_Inequality((Object) ((RaycastHit) ref hit).collider, (Object) null) && Object.op_Inequality((Object) ((RaycastHit) ref hit).collider.attachedRigidbody, (Object) null) && ((Object) ((Component) ((RaycastHit) ref hit).transform).gameObject).name.Contains("MonoObject");
      this.Cursor.GetComponent<Renderer>().material.color = flag ? new Color(0.392f, 0.722f, 0.82f, 0.4509804f) : new Color(0.8314f, 0.2471f, 0.1569f, 0.4509804f);
      this.primaryDown = InputHandling.RightPrimary;
      if (this.primaryDown)
      {
        if (!(this.canPlace & flag) || !RefCache.HitExists)
          return;
        foreach (Object @object in ((RaycastHit) ref hit).transform)
        {
          if (@object.name.Contains("Balloon MonoObject"))
            return;
        }
        GameObject gameObject = Object.Instantiate<GameObject>(this.Balloon);
        gameObject.layer = 8;
        ((Object) gameObject).name = "Balloon MonoObject";
        this.objectList.Add(gameObject);
        gameObject.transform.parent = this.itemsFolder.transform;
        gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        gameObject.transform.position = Vector3.op_Addition(Vector3.op_Addition(((RaycastHit) ref hit).point, Vector3.op_Multiply(Vector3.up, 0.3f)), Vector3.op_Division(this.Cursor.transform.forward, 3f));
        global::Balloon balloon = gameObject.AddComponent<global::Balloon>();
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
        springLine.pointone = ((Component) gameObject.transform.GetChild(0)).gameObject;
        springLine.pointtwo = ((Component) ((RaycastHit) ref hit).transform).gameObject;
        SpringJoint springJoint = gameObject.AddComponent<SpringJoint>();
        springJoint.maxDistance = 0.0f;
        springJoint.spring = 20f;
        springJoint.damper = 10f;
        ((Joint) springJoint).connectedBody = ((RaycastHit) ref hit).collider.attachedRigidbody;
        balloon.power = this.balloonPower;
        gameObject.GetComponent<Rigidbody>().constraints = (RigidbodyConstraints) 112;
        HapticManager.Haptic(HapticManager.HapticType.Create);
        this.canPlace = false;
      }
      else
        this.canPlace = true;
    }
    else if (Object.op_Inequality((Object) this.Cursor, (Object) null))
      Object.Destroy((Object) this.Cursor);
  }
}
