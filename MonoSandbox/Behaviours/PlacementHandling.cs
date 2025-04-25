// Decompiled with JetBrains decompiler
// Type: MonoSandbox.Behaviours.PlacementHandling
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using UnityEngine;

#nullable disable
namespace MonoSandbox.Behaviours
{
  public class PlacementHandling : MonoBehaviour
  {
    public float Offset = 4f;
    public bool IsEditing;
    public bool IsActivated;
    public bool Placed;
    public GameObject Cursor;
    public GameObject SandboxContainer;

    public virtual GameObject CursorRef { get; }

    public virtual void Activated(RaycastHit hitInfo)
    {
      HapticManager.Haptic(HapticManager.HapticType.Create);
    }

    public virtual void DrawCursor(RaycastHit hitInfo)
    {
    }

    private void Start() => this.SandboxContainer = RefCache.SandboxContainer;

    private void Update()
    {
      if (!Object.op_Implicit((Object) this.SandboxContainer))
        this.SandboxContainer = RefCache.SandboxContainer;
      if (this.IsEditing && !Object.op_Implicit((Object) this.Cursor))
      {
        this.Cursor = this.CursorRef;
        this.Cursor.GetComponent<Renderer>().material = new Material(RefCache.Selection);
      }
      else if (!this.IsEditing && Object.op_Implicit((Object) this.Cursor))
        Object.Destroy((Object) this.Cursor);
      RaycastHit hit = RefCache.Hit;
      if (!this.IsEditing || !Object.op_Implicit((Object) this.Cursor))
        return;
      if (this.Cursor.activeSelf != RefCache.HitExists)
        this.Cursor.SetActive(RefCache.HitExists);
      this.Cursor.GetComponent<Renderer>().material.color = new Color(0.392f, 0.722f, 0.82f, 0.4509804f);
      if (RefCache.HitExists)
        this.DrawCursor(hit);
      this.IsActivated = InputHandling.RightPrimary;
      if (this.IsActivated && !this.Placed)
      {
        this.Placed = true;
        this.Activated(hit);
      }
      else if (!this.IsActivated && this.Placed)
        this.Placed = false;
    }
  }
}
