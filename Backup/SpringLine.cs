// Decompiled with JetBrains decompiler
// Type: SpringLine
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using UnityEngine;

#nullable disable
public class SpringLine : MonoBehaviour
{
  public GameObject pointone;
  public GameObject pointtwo;
  public bool makeLine = false;
  public LineRenderer lineRenderer = (LineRenderer) null;

  private void Update()
  {
    if (Object.op_Equality((Object) this.lineRenderer, (Object) null) && this.makeLine)
    {
      this.lineRenderer = this.pointone.gameObject.AddComponent<LineRenderer>();
      ((Renderer) this.lineRenderer).material = new Material(Shader.Find("Sprites/Default"));
      this.lineRenderer.startColor = Color.grey;
      this.lineRenderer.endColor = Color.grey;
      this.lineRenderer.startWidth = 0.02f;
      this.lineRenderer.endWidth = 0.02f;
      this.lineRenderer.positionCount = 1;
      this.lineRenderer.SetPosition(0, this.pointone.transform.position);
    }
    else if (this.makeLine)
    {
      this.lineRenderer.SetPosition(0, this.pointone.transform.position);
      if (Object.op_Inequality((Object) this.pointtwo, (Object) null))
      {
        if (this.lineRenderer.positionCount == 1)
          this.lineRenderer.positionCount = 2;
        this.lineRenderer.SetPosition(1, this.pointtwo.transform.position);
      }
    }
    this.lineRenderer.SetPosition(0, this.pointone.transform.position);
    this.lineRenderer.SetPosition(1, this.pointtwo.transform.position);
  }
}
