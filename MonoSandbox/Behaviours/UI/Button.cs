// Decompiled with JetBrains decompiler
// Type: MonoSandbox.Behaviours.UI.Button
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using GorillaExtensions;
using UnityEngine;

#nullable disable
namespace MonoSandbox.Behaviours.UI
{
  public class Button : MonoBehaviour
  {
    public SandboxMenu _list;
    public GameObject _text;
    public int _buttonIndex;
    private Vector3 _btnScale;
    private Vector3 _txtScale;
    private Color _flipColour;
    private bool _flipping;
    private bool _active;
    private float _scale = 1f;
    private float _sine;
    private float _time;
    private float _lastTime;

    public void Start()
    {
      this._active = this._list._currentPage == 0 && this._list.objectButtons[this._buttonIndex] || this._list._currentPage == 1 && this._list.weaponButtons[this._buttonIndex] || this._list._currentPage == 2 && this._list.toolButtons[this._buttonIndex] || this._list._currentPage == 3 && this._list.utilButtons[this._buttonIndex] || this._list._currentPage == 4 && this._list.funButtons[this._buttonIndex];
      ((Component) this).gameObject.GetComponent<Renderer>().material.color = Color32.op_Implicit(this._active ? new Color32((byte) 71, (byte) 121, (byte) 196, byte.MaxValue) : new Color32((byte) 215, (byte) 225, (byte) 239, byte.MaxValue));
      this._btnScale = ((Component) this).transform.localScale;
      this._txtScale = this._text.transform.localScale;
    }

    public void Update()
    {
      bool flag = this._list._currentPage == 0 && this._list.objectButtons[this._buttonIndex] || this._list._currentPage == 1 && this._list.weaponButtons[this._buttonIndex] || this._list._currentPage == 2 && this._list.toolButtons[this._buttonIndex] || this._list._currentPage == 3 && this._list.utilButtons[this._buttonIndex] || this._list._currentPage == 4 && this._list.funButtons[this._buttonIndex];
      if (this._active != flag)
      {
        this._active = flag;
        this._flipping = true;
        this._time = -1.57079637f;
        this._flipColour = Color32.op_Implicit(flag ? new Color32((byte) 71, (byte) 121, (byte) 196, byte.MaxValue) : new Color32((byte) 215, (byte) 225, (byte) 239, byte.MaxValue));
      }
      if (this._flipping)
      {
        this._time += Time.deltaTime * 18f;
        this._sine = Mathf.Sin(this._time);
        this._scale = Mathf.Abs(this._sine);
        if ((double) this._time > 0.0)
          ((Component) this).gameObject.GetComponent<Renderer>().material.color = this._flipColour;
        if ((double) this._time >= 1.5707963705062866)
        {
          this._scale = 1f;
          this._flipping = false;
        }
      }
      ((Component) this).transform.localScale = GTExt.WithY(this._btnScale, this._btnScale.y * this._scale);
      this._text.transform.localScale = GTExt.WithY(this._txtScale, this._txtScale.y * this._scale);
    }

    public void OnTriggerEnter(Collider other)
    {
      GorillaTriggerColliderHandIndicator colliderHandIndicator;
      if (!((Component) other).TryGetComponent<GorillaTriggerColliderHandIndicator>(ref colliderHandIndicator) || colliderHandIndicator.isLeftHand || (double) Time.time <= (double) this._lastTime + 0.25)
        return;
      this._lastTime = Time.time;
      GorillaTagger.Instance.StartVibration(colliderHandIndicator.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 2f, GorillaTagger.Instance.tapHapticDuration);
      bool[] array1 = this._list.GetArray();
      this._list.Clear();
      this._list.PlayAudio(true);
      if (array1[this._buttonIndex])
        return;
      bool[] array2 = this._list.GetArray();
      array2[this._buttonIndex] = true;
      this._list.SetArray(array2);
    }
  }
}
