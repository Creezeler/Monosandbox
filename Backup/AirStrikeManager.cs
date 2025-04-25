// Decompiled with JetBrains decompiler
// Type: AirStrikeManager
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using MonoSandbox;
using MonoSandbox.Behaviours;
using UnityEngine;

#nullable disable
public class AirStrikeManager : MonoBehaviour
{
  public bool primaryDown;
  public bool canPlace;
  public bool editMode;
  public GameObject Cursor;
  public GameObject AirStrikeModel;
  public GameObject CursorModel;
  public GameObject ExplodeModel;
  private GameObject itemsFolder = (GameObject) null;

  public void Start() => this.itemsFolder = ((Component) this).gameObject;

  public void Update()
  {
    RaycastHit hit = RefCache.Hit;
    if (Object.op_Inequality((Object) this.Cursor, (Object) null))
    {
      this.Cursor.transform.position = ((RaycastHit) ref hit).point;
      this.Cursor.transform.forward = ((RaycastHit) ref hit).normal;
      this.primaryDown = InputHandling.RightPrimary;
      if (this.primaryDown)
      {
        if (this.canPlace)
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.AirStrikeModel);
          gameObject.transform.SetParent(this.itemsFolder.transform, false);
          gameObject.transform.position = Vector3.op_Addition(((RaycastHit) ref hit).point, new Vector3(0.0f, 80f, 0.0f));
          gameObject.transform.localScale = new Vector3(50f, 50f, 50f);
          Airstrike airstrike = gameObject.AddComponent<Airstrike>();
          airstrike.StrikeLocation = ((RaycastHit) ref hit).point;
          airstrike.ExplosionOBJ = this.ExplodeModel;
          HapticManager.Haptic(HapticManager.HapticType.Create);
          this.canPlace = false;
        }
      }
      else
        this.canPlace = true;
    }
    else if (this.editMode)
    {
      this.Cursor = Object.Instantiate<GameObject>(this.CursorModel);
      this.Cursor.transform.localScale = new Vector3(20f, 20f, 20f);
    }
    else if (Object.op_Implicit((Object) this.Cursor))
      Object.Destroy((Object) this.Cursor.gameObject);
    if (this.editMode || !Object.op_Implicit((Object) this.Cursor))
      return;
    Object.Destroy((Object) this.Cursor.gameObject);
  }
}
