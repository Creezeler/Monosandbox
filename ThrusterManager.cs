// Decompiled with JetBrains decompiler
// Type: ThrusterManager
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using MonoSandbox;
using MonoSandbox.Behaviours;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ThrusterManager : MonoBehaviour
{
  public List<GameObject> objectList = new List<GameObject>();
  public bool primaryDown;
  public bool canPlace;
  public bool editMode;
  public float multiplier = 4f;
  public GameObject Cursor;
  public GameObject itemsFolder;
  public GameObject ThrusterModel;
  public GameObject ThrustParticles;

  public void Start() => this.itemsFolder = ((Component) this).gameObject;

  public void Update()
  {
    RaycastHit hit = RefCache.Hit;
    if (Object.op_Inequality((Object) this.Cursor, (Object) null))
    {
      bool flag = Object.op_Inequality((Object) ((RaycastHit) ref hit).collider, (Object) null) && Object.op_Inequality((Object) ((RaycastHit) ref hit).collider.attachedRigidbody, (Object) null) && ((Object) ((Component) ((RaycastHit) ref hit).transform).gameObject).name.Contains("MonoObject");
      this.Cursor.GetComponent<Renderer>().material.color = flag ? new Color(0.392f, 0.722f, 0.82f, 0.4509804f) : new Color(0.8314f, 0.2471f, 0.1569f, 0.4509804f);
      this.Cursor.transform.position = ((RaycastHit) ref hit).point;
      this.Cursor.transform.forward = Vector3.op_UnaryNegation(((RaycastHit) ref hit).normal);
      this.primaryDown = InputHandling.RightPrimary;
      if (this.primaryDown)
      {
        if (this.canPlace & flag)
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.ThrusterModel);
          gameObject.transform.localScale = new Vector3(10f, 10f, 10f);
          gameObject.transform.SetParent(((Component) ((RaycastHit) ref hit).collider).transform, true);
          gameObject.transform.position = ((RaycastHit) ref hit).point;
          ((Object) gameObject).name = "Thruster MonoObject";
          this.objectList.Add(gameObject);
          ThrusterControls thrusterControls = gameObject.AddComponent<ThrusterControls>();
          thrusterControls.rb = ((RaycastHit) ref hit).collider.attachedRigidbody;
          thrusterControls.multiplier = this.multiplier;
          thrusterControls.particle = Object.Instantiate<GameObject>(this.ThrustParticles);
          gameObject.GetComponent<Renderer>().material.color = Color.black;
          gameObject.transform.forward = Vector3.op_UnaryNegation(((RaycastHit) ref hit).normal);
          HapticManager.Haptic(HapticManager.HapticType.Create);
          this.canPlace = false;
        }
      }
      else
        this.canPlace = true;
    }
    else if (this.editMode)
    {
      this.Cursor = Object.Instantiate<GameObject>(this.ThrusterModel);
      this.Cursor.transform.localScale = new Vector3(10f, 10f, 10f);
      this.Cursor.GetComponent<Renderer>().material = new Material(RefCache.Selection);
    }
    else if (Object.op_Inequality((Object) this.Cursor, (Object) null))
      Object.Destroy((Object) this.Cursor.gameObject);
    if (this.editMode || !Object.op_Inequality((Object) this.Cursor, (Object) null))
      return;
    Object.Destroy((Object) this.Cursor.gameObject);
  }
}
