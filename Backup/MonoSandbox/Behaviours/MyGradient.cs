// Decompiled with JetBrains decompiler
// Type: MonoSandbox.Behaviours.MyGradient
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace MonoSandbox.Behaviours
{
  public class MyGradient
  {
    private readonly List<MyGradientKey> _keys;

    public MyGradient() => this._keys = new List<MyGradientKey>();

    public int Count => this._keys.Count;

    public MyGradientKey this[int index]
    {
      get => this._keys[index];
      set
      {
        this._keys[index] = value;
        this.sortKeys();
      }
    }

    public void AddKey(float t, Color color) => this.AddKey(new MyGradientKey(t, color));

    public void AddKey(MyGradientKey key)
    {
      this._keys.Add(key);
      this.sortKeys();
    }

    public void InsertKey(int index, float t, Color color)
    {
      this.InsertKey(index, new MyGradientKey(t, color));
    }

    public void InsertKey(int index, MyGradientKey key)
    {
      this._keys.Insert(index, key);
      this.sortKeys();
    }

    public void RemoveKey(int index)
    {
      this._keys.RemoveAt(index);
      this.sortKeys();
    }

    public void RemoveInRange(float min, float max)
    {
      for (int index = this._keys.Count - 1; index >= 0; --index)
      {
        if ((double) this._keys[index].t >= (double) min && (double) this._keys[index].t <= (double) max)
          this._keys.RemoveAt(index);
      }
      this.sortKeys();
    }

    public void Clear() => this._keys.Clear();

    private void sortKeys()
    {
      this._keys.Sort((Comparison<MyGradientKey>) ((a, b) => a.t.CompareTo(b.t)));
    }

    private (int l, int r) getNeighborKeys(float t)
    {
      int num = this.Count - 1;
      for (int index = 0; index <= num; ++index)
      {
        if ((double) this._keys[index].t >= (double) t)
          return index == 0 ? (-1, index) : (index - 1, index);
      }
      return (num, -1);
    }

    public Color Evaluate(float t)
    {
      if (this.Count == 0)
        return new Color(0.0f, 0.0f, 0.0f, 0.0f);
      (int num1, int num2) = this.getNeighborKeys(t);
      if (num1 < 0)
        return this._keys[num2].Color;
      if (num2 < 0)
        return this._keys[num1].Color;
      MyGradientKey key = this._keys[num1];
      Color color1 = key.Color;
      key = this._keys[num2];
      Color color2 = key.Color;
      key = this._keys[num1];
      double t1 = (double) key.t;
      key = this._keys[num2];
      double t2 = (double) key.t;
      double num3 = (double) t;
      double num4 = (double) Mathf.InverseLerp((float) t1, (float) t2, (float) num3);
      return Color.Lerp(color1, color2, (float) num4);
    }
  }
}
