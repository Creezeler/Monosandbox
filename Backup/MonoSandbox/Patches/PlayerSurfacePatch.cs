// Decompiled with JetBrains decompiler
// Type: MonoSandbox.Patches.PlayerSurfacePatch
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using GorillaLocomotion;
using HarmonyLib;
using UnityEngine;

#nullable disable
namespace MonoSandbox.Patches
{
  [HarmonyPatch(typeof (GTPlayer), "GetSlidePercentage")]
  public class PlayerSurfacePatch
  {
    public static void Prefix(RaycastHit raycastHit)
    {
      MineDetonate mineDetonate;
      if (!Object.op_Implicit((Object) ((RaycastHit) ref raycastHit).collider) || !((Component) ((RaycastHit) ref raycastHit).collider).gameObject.TryGetComponent<MineDetonate>(ref mineDetonate))
        return;
      mineDetonate.Explode();
    }
  }
}
