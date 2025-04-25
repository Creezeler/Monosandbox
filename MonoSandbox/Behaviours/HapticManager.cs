// Decompiled with JetBrains decompiler
// Type: MonoSandbox.Behaviours.HapticManager
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using UnityEngine;

#nullable disable
namespace MonoSandbox.Behaviours
{
  public class HapticManager
  {
    public static void Haptic() => HapticManager.Haptic(HapticManager.HapticType.Create);

    public static void Haptic(HapticManager.HapticType hapticType)
    {
      if (hapticType == HapticManager.HapticType.Constant)
        GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tapHapticStrength / 10f, Time.deltaTime);
      else
        GorillaTagger.Instance.StartVibration(false, hapticType == HapticManager.HapticType.Create ? 0.1f : 0.5f, GorillaTagger.Instance.tapHapticDuration / 1.25f);
    }

    public enum HapticType
    {
      Create,
      Use,
      Constant,
    }
  }
}
