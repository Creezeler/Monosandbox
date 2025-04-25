// Decompiled with JetBrains decompiler
// Type: MonoSandbox.Behaviours.Enemy
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using GorillaLocomotion;
using UnityEngine;

#nullable disable
namespace MonoSandbox.Behaviours
{
  public class Enemy : MonoBehaviour
  {
    public float Health = 20f;
    public float Defence = 2f;
    private bool _isAttacking;

    public void Update()
    {
      ((Component) this).transform.LookAt(((Component) GorillaTagger.Instance.headCollider).transform.position);
      if ((double) Vector3.Distance(((Component) this).transform.position, ((Component) GorillaTagger.Instance.headCollider).transform.position) > 1.5)
      {
        ((Component) this).transform.position = Vector3.MoveTowards(((Component) this).transform.position, ((Component) GorillaTagger.Instance.headCollider).transform.position, 4f * Time.deltaTime);
        this._isAttacking = false;
      }
      else if (!this._isAttacking)
      {
        this._isAttacking = true;
        ((Component) this).GetComponent<AudioSource>().Play();
        Rigidbody component = ((Component) GTPlayer.Instance).GetComponent<Rigidbody>();
        component.AddExplosionForce(9000f * Mathf.Sqrt(component.mass), ((Component) this).transform.position, 12.3f);
      }
      if ((double) this.Health > 0.0)
        return;
      Object.Destroy((Object) ((Component) this).gameObject);
    }

    public void Damage(float damage, float criticalChance, float criticalMultiplier)
    {
      if ((double) Random.Range(1, 100) < (double) criticalChance)
        this.Health = Mathf.Clamp(this.Health - (damage * criticalMultiplier + (float) Random.Range(-2, 2)) / this.Defence, 0.0f, float.PositiveInfinity);
      else
        this.Health = Mathf.Clamp(this.Health - (damage + (float) Random.Range(-4, 4)) / this.Defence, 0.0f, float.PositiveInfinity);
    }
  }
}
