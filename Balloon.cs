// Decompiled with JetBrains decompiler
// Type: Balloon
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using System;
using UnityEngine;

#nullable disable
public class Balloon : MonoBehaviour
{
  public float power;
  public float maxSpeed;

  private void Start()
  {
    this.maxSpeed = Mathf.Clamp((float) (0.20000000298023224 + (double) this.power * 0.40000000596046448), -5f, 5f);
  }

  private void FixedUpdate()
  {
    Vector3 velocity = ((Component) this).gameObject.GetComponent<Rigidbody>().velocity;
    Vector3 vector3 = Vector3.op_Multiply(((Vector3) ref velocity).normalized, this.maxSpeed);
    ((Component) this).gameObject.GetComponent<Rigidbody>().velocity = vector3;
    ((Component) this).gameObject.GetComponent<Rigidbody>().AddForce(0.0f, -Physics.gravity.y + (float) Math.Pow(3.0, 4.0) + this.power, 0.0f);
  }
}
