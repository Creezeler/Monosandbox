// Decompiled with JetBrains decompiler
// Type: MonoSandbox.Behaviours.Tools.LimitVelocity
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using UnityEngine;

#nullable disable
namespace MonoSandbox.Behaviours.Tools
{
  public class LimitVelocity : MonoBehaviour
  {
    public float maxSpeed = 3f;
    private Rigidbody rb = (Rigidbody) null;

    private void Start() => this.rb = ((Component) this).GetComponent<Rigidbody>();

    private void FixedUpdate()
    {
      if (Object.op_Equality((Object) this.rb, (Object) null))
        this.rb = ((Component) this).GetComponent<Rigidbody>();
      ((Component) this).gameObject.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(this.rb.velocity, this.maxSpeed);
    }
  }
}
