// Decompiled with JetBrains decompiler
// Type: MonoSandbox.Patches.PlayerInitializePatch
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using HarmonyLib;
using UnityEngine;

#nullable disable
namespace MonoSandbox.Patches
{
  [HarmonyPatch(typeof (GorillaTagger), "Start")]
  public class PlayerInitializePatch
  {
    public static void Postfix(GorillaTagger __instance)
    {
      RefCache.LHand = ((Component) __instance.offlineVRRig.leftHandTransform.parent.Find("palm.01.L")).gameObject;
      RefCache.RHand = ((Component) __instance.offlineVRRig.rightHandTransform.parent.Find("palm.01.R")).gameObject;
    }
  }
}
