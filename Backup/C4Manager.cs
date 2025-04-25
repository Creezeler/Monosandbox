// Decompiled with JetBrains decompiler
// Type: C4Manager
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using MonoSandbox;
using MonoSandbox.Behaviours;
using UnityEngine;

#nullable disable
public class C4Manager : MonoBehaviour
{
  public bool primaryDown;
  public bool canPlace;
  public bool editMode;
  public bool IsMine;
  public float multiplier = 4f;
  public GameObject Explosive;
  public GameObject C4Model;
  public GameObject Mine;
  public GameObject ExplodeModel;
  public GameObject itemsFolder = (GameObject) null;
  private bool _isMine;

  public void Start() => this.itemsFolder = ((Component) this).gameObject;

  public void DrawCursor()
  {
    if (Object.op_Inequality((Object) this.Explosive, (Object) null))
      Object.Destroy((Object) this.Explosive);
    this.Explosive = Object.Instantiate<GameObject>(this.IsMine ? this.Mine : this.C4Model);
    this.Explosive.transform.localScale = this.IsMine ? Vector3.op_Multiply(Vector3.one, 1f) : Vector3.op_Multiply(Vector3.one, 1.4f);
    this.Explosive.GetComponent<Renderer>().material = new Material(RefCache.Selection);
    Object.Destroy((Object) this.Explosive.GetComponent<Collider>());
  }

  public void Update()
  {
    RaycastHit hit = RefCache.Hit;
    if (Object.op_Inequality((Object) this.Explosive, (Object) null))
    {
      if (this.IsMine != this._isMine)
      {
        this._isMine = this.IsMine;
        this.DrawCursor();
      }
      this.Explosive.transform.position = ((RaycastHit) ref hit).point;
      if (RefCache.HitExists && this.IsMine)
        this.Explosive.transform.up = ((RaycastHit) ref hit).normal;
      else if (RefCache.HitExists && !this.IsMine)
        this.Explosive.transform.forward = ((RaycastHit) ref hit).normal;
      this.Explosive.GetComponent<Renderer>().material.color = new Color(0.392f, 0.722f, 0.82f, 0.4509804f);
      this.primaryDown = InputHandling.RightPrimary;
      if (this.primaryDown && RefCache.HitExists)
      {
        if (this.canPlace && !this.IsMine)
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.C4Model);
          Object.Destroy((Object) gameObject.GetComponent<MeshCollider>());
          gameObject.AddComponent<BoxCollider>();
          gameObject.transform.SetParent(this.itemsFolder.transform, false);
          gameObject.transform.position = ((RaycastHit) ref hit).point;
          gameObject.transform.forward = ((RaycastHit) ref hit).normal;
          gameObject.transform.localScale = Vector3.op_Multiply(Vector3.one, 1.4f);
          BombDetonate bombDetonate = gameObject.AddComponent<BombDetonate>();
          bombDetonate.ExplosionOBJ = this.ExplodeModel;
          bombDetonate.multiplier = this.multiplier;
        }
        else if (this.canPlace && this.IsMine)
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.Mine);
          gameObject.transform.SetParent(this.itemsFolder.transform, false);
          gameObject.transform.position = ((RaycastHit) ref hit).point;
          gameObject.transform.up = ((RaycastHit) ref hit).normal;
          gameObject.transform.localScale = Vector3.one;
          MineDetonate mineDetonate = gameObject.AddComponent<MineDetonate>();
          mineDetonate.Explosion = this.ExplodeModel;
          mineDetonate.Multiplier = this.multiplier;
        }
        HapticManager.Haptic(HapticManager.HapticType.Create);
        this.canPlace = false;
      }
      else if (RefCache.HitExists)
        this.canPlace = true;
    }
    else if (this.editMode)
      this.DrawCursor();
    else if (Object.op_Inequality((Object) this.Explosive, (Object) null))
      Object.Destroy((Object) this.Explosive.gameObject);
    if (this.editMode || !Object.op_Implicit((Object) this.Explosive))
      return;
    Object.Destroy((Object) this.Explosive.gameObject);
  }
}
