// Decompiled with JetBrains decompiler
// Type: WeaponManager
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using GorillaLocomotion;
using MonoSandbox;
using MonoSandbox.Behaviours;
using UnityEngine;

#nullable disable
public class WeaponManager : MonoBehaviour
{
  public float weaponForce = 4f;
  private float trigger;
  private float lastShot;
  private readonly float weaponMult = 2f;
  private bool canFire = true;
  private bool canChange = true;
  public bool rightHanded = true;
  public bool editMode;
  public bool primary;
  public bool secondary;
  public int currentWeapon = 0;
  private int holdingWeapon = 100;
  private MyGradient colourGradient;
  private float colourTimestamp;
  public GameObject MelonCannonModel;
  public GameObject MelonModel;
  public GameObject MelonExplodeModel;
  public GameObject RevolverModel;
  public GameObject ShotgunModel;
  public GameObject SniperModel;
  public GameObject LaserGunModel;
  public GameObject ToolGunModel;
  public GameObject BananaGunModel;
  public GameObject HitPointParticle;
  public GameObject AssultRiffle;
  public GameObject LaserExplode;
  public GameObject HeldWeapon;
  public GameObject rightHand;
  public GameObject leftHand;
  public GameObject muzzleFlash;
  public GameObject laserFX;
  public GameObject itemsFolder = (GameObject) null;

  private void Start()
  {
    this.colourGradient = new MyGradient();
    this.colourGradient.AddKey(0.0f, Color.red);
    this.colourGradient.AddKey(1f, Color.Lerp(Color.red, Color.yellow, 0.5f));
    this.colourGradient.AddKey(2f, Color.yellow);
    this.colourGradient.AddKey(3f, Color.green);
    this.colourGradient.AddKey(4f, Color.blue);
    this.colourGradient.AddKey(5f, Color.magenta);
    this.colourGradient.AddKey(6f, Color.white);
    this.colourGradient.AddKey(7f, Color.black);
  }

  private void Update()
  {
    this.trigger = InputHandling.RightTrigger;
    this.primary = InputHandling.RightPrimary;
    this.secondary = InputHandling.RightSecondary;
    if (Object.op_Equality((Object) this.itemsFolder, (Object) null))
      this.itemsFolder = ((Component) this).gameObject;
    if (Object.op_Equality((Object) this.rightHand, (Object) null) || Object.op_Equality((Object) this.leftHand, (Object) null))
    {
      this.rightHand = RefCache.RHand;
      this.leftHand = RefCache.LHand;
    }
    if (this.editMode && this.holdingWeapon != this.currentWeapon)
    {
      Object.Destroy((Object) this.HeldWeapon);
      this.WeaponStuff(this.currentWeapon);
    }
    if (this.editMode && (double) this.trigger > 0.5)
    {
      if (this.canFire)
      {
        if ((double) Time.time > (double) this.lastShot + 0.5)
        {
          if (this.holdingWeapon == 0)
          {
            this.lastShot = Time.time;
            this.HeldWeapon.GetComponent<AudioSource>().PlayOneShot(this.HeldWeapon.GetComponent<AudioSource>().clip);
            ((Component) this.HeldWeapon.transform).GetComponentInChildren<ParticleSystem>().Play();
            this.HeldWeapon.GetComponent<SineGunAnimation>().Play();
            RaycastHit raycastHit;
            Physics.Raycast(this.HeldWeapon.transform.GetChild(0).position, this.HeldWeapon.transform.GetChild(0).forward, ref raycastHit, 1000f, LayerMask.op_Implicit(GTPlayer.Instance.locomotionEnabledLayers));
            GameObject gameObject = Object.Instantiate<GameObject>(this.HitPointParticle);
            gameObject.transform.position = ((RaycastHit) ref raycastHit).point;
            gameObject.transform.forward = ((RaycastHit) ref raycastHit).normal;
            gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            ((Component) ((RaycastHit) ref raycastHit).transform).GetComponent<Rigidbody>()?.AddExplosionForce(600f * this.weaponForce * this.weaponMult, ((RaycastHit) ref raycastHit).point, 3f);
            ((Component) ((RaycastHit) ref raycastHit).transform).GetComponent<BombDetonate>()?.Explode();
            ((Component) ((RaycastHit) ref raycastHit).transform).GetComponent<Explode>()?.ExplodeObject();
            Object.Destroy((Object) gameObject, 3f);
            HapticManager.Haptic(HapticManager.HapticType.Use);
            this.canFire = false;
          }
          if (this.holdingWeapon == 5)
          {
            this.lastShot = Time.time;
            this.HeldWeapon.GetComponent<AudioSource>().PlayOneShot(this.HeldWeapon.GetComponent<AudioSource>().clip);
            ((Component) this.HeldWeapon.transform).GetComponentInChildren<ParticleSystem>().Play();
            this.HeldWeapon.GetComponent<SineGunAnimation>().Play();
            RaycastHit raycastHit;
            Physics.Raycast(this.HeldWeapon.transform.GetChild(0).position, Vector3.op_UnaryNegation(this.HeldWeapon.transform.GetChild(0).up), ref raycastHit, 1000f, LayerMask.op_Implicit(GTPlayer.Instance.locomotionEnabledLayers));
            GameObject gameObject = Object.Instantiate<GameObject>(this.HitPointParticle);
            gameObject.transform.position = ((RaycastHit) ref raycastHit).point;
            gameObject.transform.forward = ((RaycastHit) ref raycastHit).normal;
            gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            ((Component) ((RaycastHit) ref raycastHit).transform).GetComponent<Rigidbody>()?.AddExplosionForce(650f * this.weaponForce * this.weaponMult, ((RaycastHit) ref raycastHit).point, 3f);
            ((Component) ((RaycastHit) ref raycastHit).transform).GetComponent<BombDetonate>()?.Explode();
            ((Component) ((RaycastHit) ref raycastHit).transform).GetComponent<Explode>()?.ExplodeObject();
            Object.Destroy((Object) gameObject, 3f);
            HapticManager.Haptic(HapticManager.HapticType.Use);
            this.canFire = false;
          }
          if (this.holdingWeapon == 6)
          {
            this.lastShot = Time.time;
            this.HeldWeapon.GetComponent<AudioSource>().PlayOneShot(this.HeldWeapon.GetComponent<AudioSource>().clip);
            this.HeldWeapon.GetComponent<SineGunAnimation>().Play();
            RaycastHit raycastHit;
            Physics.Raycast(this.HeldWeapon.transform.GetChild(6).position, Vector3.op_UnaryNegation(this.HeldWeapon.transform.GetChild(6).right), ref raycastHit, 1000f, LayerMask.op_Implicit(GTPlayer.Instance.locomotionEnabledLayers));
            if (((Object) ((RaycastHit) ref raycastHit).transform).name.Contains("MonoObject") && Object.op_Inequality((Object) ((Component) ((RaycastHit) ref raycastHit).transform).GetComponent<Renderer>(), (Object) null))
              ((Component) ((RaycastHit) ref raycastHit).transform).GetComponent<Renderer>().material.color = this.colourGradient.Evaluate(this.colourTimestamp);
            HapticManager.Haptic(HapticManager.HapticType.Use);
            this.canFire = false;
          }
        }
        if ((double) Time.time > (double) this.lastShot + 0.75)
        {
          if (this.holdingWeapon == 7)
          {
            this.lastShot = Time.time;
            ((Component) GTPlayer.Instance).GetComponent<Rigidbody>().AddForce(Vector3.op_Multiply(Vector3.op_Multiply(Vector3.op_UnaryNegation(this.HeldWeapon.transform.GetChild(0).forward), this.weaponForce), 2200f));
            this.HeldWeapon.GetComponent<AudioSource>().PlayOneShot(this.HeldWeapon.GetComponent<AudioSource>().clip);
            ((Component) this.HeldWeapon.transform).GetComponentInChildren<ParticleSystem>().Play();
            this.HeldWeapon.GetComponent<SineGunAnimation>().Play();
            RaycastHit raycastHit;
            Physics.Raycast(this.HeldWeapon.transform.GetChild(0).position, this.HeldWeapon.transform.GetChild(0).forward, ref raycastHit, 1000f, LayerMask.op_Implicit(GTPlayer.Instance.locomotionEnabledLayers));
            GameObject gameObject = Object.Instantiate<GameObject>(this.HitPointParticle);
            gameObject.transform.position = ((RaycastHit) ref raycastHit).point;
            gameObject.transform.forward = ((RaycastHit) ref raycastHit).normal;
            gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            ((Component) ((RaycastHit) ref raycastHit).transform).GetComponent<Rigidbody>()?.AddExplosionForce(900f * this.weaponForce * this.weaponMult, ((RaycastHit) ref raycastHit).point, 4f);
            ((Component) ((RaycastHit) ref raycastHit).transform).GetComponent<BombDetonate>()?.Explode();
            ((Component) ((RaycastHit) ref raycastHit).transform).GetComponent<Explode>()?.ExplodeObject();
            Object.Destroy((Object) gameObject, 3f);
            HapticManager.Haptic(HapticManager.HapticType.Use);
            this.canFire = false;
          }
          if (this.holdingWeapon == 1)
          {
            this.lastShot = Time.time;
            ((Component) GTPlayer.Instance).GetComponent<Rigidbody>().AddForce(Vector3.op_Multiply(Vector3.op_Multiply(Vector3.op_UnaryNegation(this.HeldWeapon.transform.GetChild(0).forward), this.weaponForce), 2200f));
            this.HeldWeapon.GetComponent<AudioSource>().PlayOneShot(this.HeldWeapon.GetComponent<AudioSource>().clip);
            ((Component) this.HeldWeapon.transform).GetComponentInChildren<ParticleSystem>().Play();
            this.HeldWeapon.GetComponent<SineGunAnimation>().Play();
            RaycastHit raycastHit;
            Physics.Raycast(this.HeldWeapon.transform.GetChild(0).position, this.HeldWeapon.transform.GetChild(0).forward, ref raycastHit, 1000f, LayerMask.op_Implicit(GTPlayer.Instance.locomotionEnabledLayers));
            GameObject gameObject = Object.Instantiate<GameObject>(this.HitPointParticle);
            gameObject.transform.position = ((RaycastHit) ref raycastHit).point;
            gameObject.transform.forward = ((RaycastHit) ref raycastHit).normal;
            gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            ((Component) ((RaycastHit) ref raycastHit).transform).GetComponent<Rigidbody>()?.AddExplosionForce(900f * this.weaponForce * this.weaponMult, ((RaycastHit) ref raycastHit).point, 4f);
            ((Component) ((RaycastHit) ref raycastHit).transform).GetComponent<BombDetonate>()?.Explode();
            ((Component) ((RaycastHit) ref raycastHit).transform).GetComponent<Explode>()?.ExplodeObject();
            Object.Destroy((Object) gameObject, 3f);
            HapticManager.Haptic(HapticManager.HapticType.Use);
            this.canFire = false;
          }
        }
        if ((double) Time.time > (double) this.lastShot + 1.25)
        {
          if (this.holdingWeapon == 2)
          {
            this.lastShot = Time.time;
            this.HeldWeapon.GetComponent<AudioSource>().PlayOneShot(this.HeldWeapon.GetComponent<AudioSource>().clip);
            ((Component) this.HeldWeapon.transform).GetComponentInChildren<ParticleSystem>().Play();
            this.HeldWeapon.GetComponent<SineGunAnimation>().Play();
            GameObject gameObject = Object.Instantiate<GameObject>(this.MelonModel);
            gameObject.transform.position = this.HeldWeapon.transform.GetChild(1).position;
            ((Component) gameObject.transform).GetComponent<Rigidbody>().AddForce(Vector3.op_Multiply(this.rightHand.transform.up, 2500f));
            ((Component) gameObject.transform).GetComponent<Rigidbody>().angularVelocity = Vector3.op_Multiply(this.rightHand.transform.up, 100f);
            Bullet bullet = gameObject.AddComponent<Bullet>();
            bullet.gunIndex = 2;
            bullet.melonExplode = this.MelonExplodeModel;
            HapticManager.Haptic(HapticManager.HapticType.Use);
            this.canFire = false;
          }
          if (this.holdingWeapon == 3)
          {
            this.lastShot = Time.time;
            this.HeldWeapon.GetComponent<AudioSource>().PlayOneShot(this.HeldWeapon.GetComponent<AudioSource>().clip);
            ((Component) this.HeldWeapon.transform).GetComponentInChildren<ParticleSystem>().Play();
            this.HeldWeapon.GetComponent<SineGunAnimation>().Play();
            ((Component) GTPlayer.Instance).GetComponent<Rigidbody>().AddForce(Vector3.op_Multiply(Vector3.op_Multiply(Vector3.op_UnaryNegation(this.HeldWeapon.transform.GetChild(0).forward), this.weaponForce), 4500f));
            RaycastHit raycastHit;
            Physics.Raycast(this.HeldWeapon.transform.GetChild(0).position, this.HeldWeapon.transform.GetChild(0).forward, ref raycastHit, 1000f, LayerMask.op_Implicit(GTPlayer.Instance.locomotionEnabledLayers));
            GameObject gameObject = Object.Instantiate<GameObject>(this.HitPointParticle);
            gameObject.transform.position = ((RaycastHit) ref raycastHit).point;
            gameObject.transform.forward = ((RaycastHit) ref raycastHit).normal;
            gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            ((Component) ((RaycastHit) ref raycastHit).transform).GetComponent<Rigidbody>()?.AddExplosionForce(1000f * this.weaponForce * this.weaponMult, ((RaycastHit) ref raycastHit).point, 4f);
            ((Component) ((RaycastHit) ref raycastHit).transform).GetComponent<BombDetonate>()?.Explode();
            ((Component) ((RaycastHit) ref raycastHit).transform).GetComponent<Explode>()?.ExplodeObject();
            Object.Destroy((Object) gameObject, 3f);
            HapticManager.Haptic(HapticManager.HapticType.Use);
            this.canFire = false;
          }
          if (this.holdingWeapon == 4)
          {
            this.lastShot = Time.time;
            this.HeldWeapon.GetComponent<AudioSource>().PlayOneShot(this.HeldWeapon.GetComponent<AudioSource>().clip);
            ((Component) this.HeldWeapon.transform).GetComponentInChildren<ParticleSystem>().Play();
            this.HeldWeapon.GetComponent<SineGunAnimation>().Play();
            ((Component) GTPlayer.Instance).GetComponent<Rigidbody>().AddForce(Vector3.op_Multiply(Vector3.op_Multiply(Vector3.op_UnaryNegation(this.HeldWeapon.transform.GetChild(0).forward), this.weaponForce), 4500f));
            RaycastHit raycastHit;
            Physics.Raycast(this.HeldWeapon.transform.GetChild(0).position, this.HeldWeapon.transform.GetChild(0).forward, ref raycastHit, 1000f, LayerMask.op_Implicit(GTPlayer.Instance.locomotionEnabledLayers));
            GameObject gameObject = Object.Instantiate<GameObject>(this.LaserExplode);
            gameObject.transform.SetParent(this.itemsFolder.transform);
            gameObject.transform.localPosition = ((RaycastHit) ref raycastHit).point;
            gameObject.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            Object.Destroy((Object) gameObject, 3f);
            foreach (Component component in Physics.OverlapSphere(((RaycastHit) ref raycastHit).point, 24f))
              component.GetComponent<Rigidbody>()?.AddExplosionForce(10000f, ((RaycastHit) ref raycastHit).point, 8f);
            foreach (Collider collider in Physics.OverlapSphere(((RaycastHit) ref raycastHit).point, 24f))
            {
              ((Component) collider).GetComponent<BombDetonate>()?.Explode();
              ((Component) collider).GetComponent<MineDetonate>()?.Explode();
              ((Component) collider).GetComponent<Explode>()?.ExplodeObject();
            }
            Rigidbody component1 = ((Component) GTPlayer.Instance).GetComponent<Rigidbody>();
            component1.AddExplosionForce(10000f * Mathf.Sqrt(component1.mass), ((RaycastHit) ref raycastHit).point, 8f);
            HapticManager.Haptic(HapticManager.HapticType.Use);
            this.canFire = false;
          }
        }
      }
    }
    else
      this.canFire = true;
    if (this.editMode && this.holdingWeapon == 6)
    {
      if (this.primary && !this.secondary)
      {
        this.colourTimestamp = Mathf.Clamp(this.colourTimestamp + Time.deltaTime * 1.8f, 0.0f, 7f);
        ((Component) this.HeldWeapon.transform.GetChild(0)).GetComponent<Renderer>().material.color = this.colourGradient.Evaluate(this.colourTimestamp);
      }
      else if (!this.primary && this.secondary)
      {
        this.colourTimestamp = Mathf.Clamp(this.colourTimestamp - Time.deltaTime * 1.8f, 0.0f, 7f);
        ((Component) this.HeldWeapon.transform.GetChild(0)).GetComponent<Renderer>().material.color = this.colourGradient.Evaluate(this.colourTimestamp);
      }
    }
    if (this.editMode || !Object.op_Inequality((Object) this.HeldWeapon, (Object) null))
      return;
    Object.Destroy((Object) this.HeldWeapon);
    this.HeldWeapon = (GameObject) null;
    this.holdingWeapon = 100;
  }

  private void WeaponStuff(int index)
  {
    switch (index)
    {
      case 0:
        this.holdingWeapon = 0;
        this.HeldWeapon = Object.Instantiate<GameObject>(this.RevolverModel);
        this.HeldWeapon.transform.eulerAngles = new Vector3(0.0f, 90f, -90f);
        this.HeldWeapon.transform.localPosition = new Vector3(-0.02f, 0.0f, 0.035f);
        if (this.rightHanded)
          this.HeldWeapon.transform.SetParent(this.rightHand.transform, false);
        this.HeldWeapon.AddComponent<SineGunAnimation>().Efficiency = 1.5f;
        break;
      case 1:
        this.holdingWeapon = 1;
        this.HeldWeapon = Object.Instantiate<GameObject>(this.ShotgunModel);
        this.HeldWeapon.transform.eulerAngles = new Vector3(0.0f, 90f, -90f);
        this.HeldWeapon.transform.localPosition = new Vector3(-0.02f, 0.0f, 0.035f);
        if (this.rightHanded)
          this.HeldWeapon.transform.SetParent(this.rightHand.transform, false);
        this.HeldWeapon.AddComponent<SineGunAnimation>().Efficiency = 1.3f;
        break;
      case 2:
        this.holdingWeapon = 2;
        this.HeldWeapon = Object.Instantiate<GameObject>(this.MelonCannonModel);
        this.HeldWeapon.transform.eulerAngles = new Vector3(0.0f, 90f, -90f);
        this.HeldWeapon.transform.localPosition = new Vector3(-0.025f, 0.25f, -0.1f);
        if (this.rightHanded)
          this.HeldWeapon.transform.SetParent(this.rightHand.transform, false);
        this.HeldWeapon.AddComponent<SineGunAnimation>().Efficiency = 1.8f;
        break;
      case 3:
        this.holdingWeapon = 3;
        this.HeldWeapon = Object.Instantiate<GameObject>(this.SniperModel);
        this.HeldWeapon.transform.eulerAngles = new Vector3(0.0f, 90f, -90f);
        this.HeldWeapon.transform.localPosition = new Vector3(-0.02f, 0.0f, 0.035f);
        if (this.rightHanded)
          this.HeldWeapon.transform.SetParent(this.rightHand.transform, false);
        this.HeldWeapon.AddComponent<SineGunAnimation>().Efficiency = 1.3f;
        break;
      case 4:
        this.holdingWeapon = 4;
        this.HeldWeapon = Object.Instantiate<GameObject>(this.LaserGunModel);
        this.HeldWeapon.transform.eulerAngles = new Vector3(0.0f, 90f, -90f);
        this.HeldWeapon.transform.localPosition = new Vector3(-0.02f, 0.0f, 0.035f);
        if (this.rightHanded)
          this.HeldWeapon.transform.SetParent(this.rightHand.transform, false);
        this.HeldWeapon.AddComponent<SineGunAnimation>().Efficiency = 1.3f;
        break;
      case 5:
        this.holdingWeapon = 5;
        this.HeldWeapon = Object.Instantiate<GameObject>(this.BananaGunModel);
        this.HeldWeapon.transform.eulerAngles = new Vector3(0.0f, 0.0f, 180f);
        this.HeldWeapon.transform.localScale = new Vector3(45f, 45f, 45f);
        this.HeldWeapon.transform.localPosition = new Vector3(-0.04f, 0.085f, -0.055f);
        if (this.rightHanded)
          this.HeldWeapon.transform.SetParent(this.rightHand.transform, false);
        SineGunAnimation sineGunAnimation = this.HeldWeapon.AddComponent<SineGunAnimation>();
        sineGunAnimation.Efficiency = 1.5f;
        sineGunAnimation.UseX = true;
        break;
      case 6:
        this.holdingWeapon = 6;
        this.HeldWeapon = Object.Instantiate<GameObject>(this.ToolGunModel);
        GameObject gameObject = new GameObject();
        gameObject.transform.SetParent(this.HeldWeapon.transform, false);
        gameObject.transform.localPosition = new Vector3(-0.265f, 0.1f, 0.0f);
        gameObject.transform.localEulerAngles = new Vector3(0.0f, -90f, 0.0f);
        this.HeldWeapon.transform.eulerAngles = new Vector3(0.0f, 90f, -90f);
        this.HeldWeapon.transform.localScale = new Vector3(1f, 1f, 1f);
        this.HeldWeapon.transform.localPosition = new Vector3(-0.03f, 0.02f, 0.035f);
        if (this.rightHanded)
          this.HeldWeapon.transform.SetParent(this.rightHand.transform, false);
        this.HeldWeapon.AddComponent<SineGunAnimation>().Efficiency = 0.5f;
        ((Component) this.HeldWeapon.transform.GetChild(0)).GetComponent<Renderer>().material.color = this.colourGradient.Evaluate(this.colourTimestamp);
        break;
      case 7:
        this.holdingWeapon = 7;
        this.HeldWeapon = Object.Instantiate<GameObject>(this.AssultRiffle);
        this.HeldWeapon.transform.eulerAngles = new Vector3(0.0f, 90f, -90f);
        this.HeldWeapon.transform.localPosition = new Vector3(-0.02f, 0.048f, 0.021f);
        this.HeldWeapon.transform.SetParent(this.rightHand.transform, false);
        this.HeldWeapon.AddComponent<SineGunAnimation>();
        break;
    }
  }
}
