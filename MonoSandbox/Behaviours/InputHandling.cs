// Decompiled with JetBrains decompiler
// Type: MonoSandbox.Behaviours.InputHandling
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using UnityEngine;

#nullable disable
namespace MonoSandbox.Behaviours
{
  public class InputHandling : MonoBehaviour
  {
    public static float LeftTrigger;
    public static float RightTrigger;
    public static float LeftGrip;
    public static float RightGrip;
    public static bool LeftPrimary;
    public static bool RightPrimary;
    public static bool LeftSecondary;
    public static bool RightSecondary;

    public void Update()
    {
      InputHandling.LeftTrigger = ControllerInputPoller.instance.leftControllerIndexFloat;
      InputHandling.LeftGrip = ControllerInputPoller.instance.leftControllerGripFloat;
      InputHandling.RightTrigger = ControllerInputPoller.instance.rightControllerIndexFloat;
      InputHandling.RightGrip = ControllerInputPoller.instance.rightControllerGripFloat;
      InputHandling.LeftPrimary = ControllerInputPoller.instance.leftControllerPrimaryButton;
      InputHandling.LeftSecondary = ControllerInputPoller.instance.leftControllerSecondaryButton;
      InputHandling.RightPrimary = ControllerInputPoller.instance.rightControllerPrimaryButton;
      InputHandling.RightSecondary = ControllerInputPoller.instance.rightControllerSecondaryButton;
    }
  }
}
