// Decompiled with JetBrains decompiler
// Type: MonoSandbox.Behaviours.SineScaleOut
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using UnityEngine;

#nullable disable
namespace MonoSandbox.Behaviours
{
  public class SineScaleOut : MonoBehaviour
  {
    public float Delay = 0.0f;
    public float Speed = 6f;
    private bool _playing;
    private Vector3 _origin;
    private float _time;
    private float _scale;

    public void Start()
    {
      if ((double) this.Delay > 0.0)
        this.Invoke("Play", this.Delay);
      else
        this.Play();
    }

    public void Play()
    {
      this._playing = true;
      this._time = -1.57079637f;
      this._origin = ((Component) this).transform.localScale;
    }

    public void Update()
    {
      if (!this._playing)
        return;
      this._time += Time.deltaTime * this.Speed;
      this._scale = Mathf.Sin(this._time);
      ((Component) this).transform.localScale = Vector3.op_Multiply(this._origin, Mathf.Abs(this._scale));
      if ((double) this._time >= 0.0)
        Object.Destroy((Object) ((Component) this).gameObject);
    }
  }
}
