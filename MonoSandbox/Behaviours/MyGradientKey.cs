// Decompiled with JetBrains decompiler
// Type: MonoSandbox.Behaviours.MyGradientKey
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using UnityEngine;

#nullable disable
namespace MonoSandbox.Behaviours
{
  public struct MyGradientKey(float t, Color color)
  {
    public float t { get; set; } = t;

    public Color Color { get; set; } = color;
  }
}
