// Decompiled with JetBrains decompiler
// Type: MonoSandbox.Behaviours.SineGunAnimation
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using GorillaExtensions;
using UnityEngine;

#nullable disable
namespace MonoSandbox.Behaviours
{
  public class SineGunAnimation : MonoBehaviour
  {
    public float Efficiency = 1.2f;
    public float Speed = 12f;
    public bool UseX;
    private bool _playing;
    private Vector3 _origin;
    private float _time;
    private float _scale;

    public void Start()
    {
      Quaternion localRotation = ((Component) this).transform.localRotation;
      this._origin = ((Quaternion) ref localRotation).eulerAngles;
    }

    public void Play()
    {
      this._playing = true;
      this._time = 0.0f;
    }

    public void Update()
    {
      if (!this._playing)
        return;
      this._time += (float) ((double) Time.deltaTime * (double) this.Speed / 1.1000000238418579);
      this._scale = Mathf.Sin(this._time);
      ((Component) this).transform.localEulerAngles = this.UseX ? GTExt.WithX(this._origin, this._origin.x - this.Speed * Mathf.Abs(1f - this._scale)) : GTExt.WithZ(this._origin, this._origin.z - this.Speed * Mathf.Abs(1f - this._scale));
      if ((double) this._time >= 1.5707963705062866)
      {
        ((Component) this).transform.localEulerAngles = this._origin;
        this._playing = false;
      }
    }
  }
}
