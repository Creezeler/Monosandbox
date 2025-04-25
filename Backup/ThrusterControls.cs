// Decompiled with JetBrains decompiler
// Type: ThrusterControls
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using MonoSandbox.Behaviours;
using UnityEngine;

#nullable disable
public class ThrusterControls : MonoBehaviour
{
  public Rigidbody rb;
  public GameObject particle;
  private float gripDown;
  public float multiplier = 4f;

  private void Start()
  {
    this.particle.transform.SetParent(((Component) this).transform, false);
    this.particle.transform.localEulerAngles = new Vector3(180f, 0.0f, 0.0f);
    this.particle.transform.localPosition = new Vector3(0.0f, 0.0f, -0.014f);
    this.particle.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
  }

  private void Update()
  {
    this.gripDown = InputHandling.RightGrip;
    if ((double) this.gripDown > 0.30000001192092896)
    {
      if (!this.particle.GetComponent<AudioSource>().isPlaying)
      {
        this.particle.GetComponent<ParticleSystem>().Play(true);
        this.particle.GetComponent<AudioSource>().Play();
      }
      HapticManager.Haptic(HapticManager.HapticType.Constant);
      this.rb.AddForceAtPosition(Vector3.op_Multiply(Vector3.op_Multiply(((Component) this).transform.forward, 10f), this.multiplier), ((Component) this).transform.position);
    }
    else if (this.particle.GetComponent<ParticleSystem>().isPlaying)
    {
      this.particle.GetComponent<ParticleSystem>().Stop(true);
      this.particle.GetComponent<AudioSource>().Stop();
    }
  }
}
