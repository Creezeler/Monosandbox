// Decompiled with JetBrains decompiler
// Type: Softbody
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using UnityEngine;

#nullable disable
public static class Softbody
{
  public static Softbody.ColliderShape Shape;
  public static float ColliderSize;
  public static float RigidbodyMass;
  public static float Spring;
  public static float Damper;
  public static RigidbodyConstraints Constraints;

  public static void Init(
    Softbody.ColliderShape shape,
    float collidersize,
    float rigidbodymass,
    float spring,
    float damper,
    RigidbodyConstraints constraints)
  {
    Softbody.Shape = shape;
    Softbody.ColliderSize = collidersize;
    Softbody.RigidbodyMass = rigidbodymass;
    Softbody.Spring = spring;
    Softbody.Damper = damper;
    Softbody.Constraints = constraints;
  }

  public static Rigidbody AddCollider(ref GameObject go)
  {
    return Softbody.AddCollider(ref go, Softbody.Shape, Softbody.ColliderSize, Softbody.RigidbodyMass);
  }

  public static SpringJoint AddSpring(ref GameObject go1, ref GameObject go2)
  {
    return Softbody.AddSpring(ref go1, ref go2, Softbody.Spring, Softbody.Damper);
  }

  public static Rigidbody AddCollider(
    ref GameObject go,
    Softbody.ColliderShape shape,
    float size,
    float mass)
  {
    switch (shape)
    {
      case Softbody.ColliderShape.Box:
        go.AddComponent<BoxCollider>().size = new Vector3(size, size, size);
        break;
      case Softbody.ColliderShape.Sphere:
        go.AddComponent<SphereCollider>().radius = size;
        break;
    }
    Rigidbody rigidbody = go.AddComponent<Rigidbody>();
    rigidbody.mass = mass;
    rigidbody.drag = 0.0f;
    rigidbody.angularDrag = 10f;
    rigidbody.constraints = Softbody.Constraints;
    return rigidbody;
  }

  public static SpringJoint AddSpring(
    ref GameObject go1,
    ref GameObject go2,
    float spring,
    float damper)
  {
    SpringJoint springJoint = go1.AddComponent<SpringJoint>();
    ((Joint) springJoint).connectedBody = go2.GetComponent<Rigidbody>();
    springJoint.spring = spring;
    springJoint.damper = damper;
    return springJoint;
  }

  public enum ColliderShape
  {
    Box,
    Sphere,
  }
}
