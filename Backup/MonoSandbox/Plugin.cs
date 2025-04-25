// Decompiled with JetBrains decompiler
// Type: MonoSandbox.Plugin
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using BepInEx;
using GorillaLocomotion;
using HarmonyLib;
using MonoSandbox.Behaviours;
using MonoSandbox.Behaviours.UI;
using Photon.Pun;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace MonoSandbox
{
  [BepInPlugin("goldentrophy.monosphere.dev.monosandbox", "MonoSandbox", "1.2.4")]
  public class Plugin : BaseUnityPlugin
  {
    public static bool InRoom;
    private bool _initialized;
    private LayerMask _layerMask;
    private AssetBundle _bundle;
    public GameObject _list;
    public GameObject _itemsContainer;
    public AudioClip _pageOpen;
    public AudioClip _itemOpen;
    private SandboxMenu _listManager;
    private BoxManager boxManager;
    private GravityManager gravityManager;
    private SphereManager sphereManager;
    private BeanManager beanManager;
    private CrateManager crateManager;
    private BathManager bathManager;
    private CouchManager couchManager;
    private RagdollManager ragdollManager;
    private AirStrikeManager airstrikeManager;
    private SpringManager springManager;
    private WeldManager weldManager;
    private FreezeManager freezeManager;
    private PhysGunManager physGunManager;
    private ThrusterManager thrusterManager;
    private C4Manager C4Control;
    private BalloonManager balloonManager;
    private WeaponManager weaponManager;
    private HammerManager hammerManager;
    private GrenadeManager grenadeManager;
    private bool hasInit;
    private bool lastInRoom;
    private bool lobbyWasModded;

    public void OnEnable()
    {
      if (!this._initialized)
        return;
      this.ragdollManager.IsEditing = false;
      this.springManager.editMode = false;
      this.weaponManager.editMode = false;
      this.thrusterManager.editMode = false;
      this.C4Control.editMode = false;
      this.boxManager.IsEditing = false;
      this.sphereManager.IsEditing = false;
      this.beanManager.IsEditing = false;
      this.crateManager.IsEditing = false;
      this.weldManager.editMode = false;
      this.bathManager.IsEditing = false;
      this.balloonManager.editMode = false;
      this.freezeManager.editMode = false;
      this.physGunManager.editMode = false;
      this.gravityManager.editMode = false;
      this.airstrikeManager.editMode = false;
      this.couchManager.IsEditing = false;
      this.hammerManager.editMode = false;
      this.grenadeManager.editMode = false;
    }

    public void OnDisable()
    {
      if (this._initialized)
      {
        this.ragdollManager.IsEditing = false;
        this.springManager.editMode = false;
        this.weaponManager.editMode = false;
        this.thrusterManager.editMode = false;
        this.C4Control.editMode = false;
        this.boxManager.IsEditing = false;
        this.sphereManager.IsEditing = false;
        this.beanManager.IsEditing = false;
        this.crateManager.IsEditing = false;
        this.weldManager.editMode = false;
        this.bathManager.IsEditing = false;
        this.balloonManager.editMode = false;
        this.freezeManager.editMode = false;
        this.physGunManager.editMode = false;
        this.gravityManager.editMode = false;
        this.airstrikeManager.editMode = false;
        this.couchManager.IsEditing = false;
        this.hammerManager.editMode = false;
        this.grenadeManager.editMode = false;
      }
      this._list?.SetActive(((Behaviour) this).enabled);
    }

    public Plugin()
    {
      new Harmony("goldentrophy.monosphere.dev.monosandbox").PatchAll(typeof (Plugin).Assembly);
    }

    public void OnGameInitialized()
    {
      ((Component) this).gameObject.AddComponent<InputHandling>();
      this._layerMask = GTPlayer.Instance.locomotionEnabledLayers;
      this._layerMask = LayerMask.op_Implicit(LayerMask.op_Implicit(this._layerMask) | 256);
      this._itemsContainer = Object.Instantiate<GameObject>(new GameObject());
      this._itemsContainer.transform.position = Vector3.zero;
      ((Object) this._itemsContainer).name = "ItemFolderMono";
      RefCache.SandboxContainer = this._itemsContainer;
      this._bundle = AssetBundle.LoadFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("MonoSandbox.Assets.sandboxbundle"));
      RefCache.Default = this._bundle.LoadAsset<Material>("Default");
      RefCache.Selection = this._bundle.LoadAsset<Material>("Selection");
      RefCache.PageSelection = this._bundle.LoadAsset<AudioClip>("Step1");
      RefCache.ItemSelection = this._bundle.LoadAsset<AudioClip>("Step2");
      this.C4Control = this._itemsContainer.AddComponent<C4Manager>();
      this.C4Control.C4Model = this._bundle.LoadAsset<GameObject>("C4_Weapon");
      this.C4Control.Mine = this._bundle.LoadAsset<GameObject>("Mine_02");
      this.C4Control.ExplodeModel = this._bundle.LoadAsset<GameObject>("Explosion");
      this.boxManager = this._itemsContainer.AddComponent<BoxManager>();
      this.sphereManager = this._itemsContainer.AddComponent<SphereManager>();
      this.sphereManager.Softbody = this._bundle.LoadAsset<GameObject>("BoneSphere");
      this.sphereManager.Entity = this._bundle.LoadAsset<GameObject>("Demon");
      this.beanManager = this._itemsContainer.AddComponent<BeanManager>();
      this.beanManager.Explosion = this._bundle.LoadAsset<GameObject>("Explosion");
      this.beanManager.Barrel = this._bundle.LoadAsset<GameObject>("Barrel");
      this.gravityManager = this._itemsContainer.AddComponent<GravityManager>();
      this.couchManager = this._itemsContainer.AddComponent<CouchManager>();
      this.couchManager.Couch = this._bundle.LoadAsset<GameObject>("Couch");
      this.crateManager = this._itemsContainer.AddComponent<CrateManager>();
      this.crateManager.Crate = this._bundle.LoadAsset<GameObject>("Crate");
      this.bathManager = this._itemsContainer.AddComponent<BathManager>();
      this.bathManager.Bath = this._bundle.LoadAsset<GameObject>("Bath");
      this.springManager = this._itemsContainer.AddComponent<SpringManager>();
      this.ragdollManager = this._itemsContainer.AddComponent<RagdollManager>();
      this.airstrikeManager = this._itemsContainer.AddComponent<AirStrikeManager>();
      this.airstrikeManager.ExplodeModel = this._bundle.LoadAsset<GameObject>("Explosion");
      this.airstrikeManager.CursorModel = this._bundle.LoadAsset<GameObject>("Cursor");
      this.airstrikeManager.AirStrikeModel = this._bundle.LoadAsset<GameObject>("Missile");
      this.thrusterManager = this._itemsContainer.AddComponent<ThrusterManager>();
      this.thrusterManager.ThrusterModel = this._bundle.LoadAsset<GameObject>("Thruster 1");
      this.thrusterManager.ThrustParticles = this._bundle.LoadAsset<GameObject>("Thruster 2");
      this.weaponManager = this._itemsContainer.AddComponent<WeaponManager>();
      this.weaponManager.ShotgunModel = this._bundle.LoadAsset<GameObject>("Shotgun");
      this.weaponManager.ToolGunModel = this._bundle.LoadAsset<GameObject>("ToolGun");
      this.weaponManager.RevolverModel = this._bundle.LoadAsset<GameObject>("Pistol");
      this.weaponManager.SniperModel = this._bundle.LoadAsset<GameObject>("SniperRifle");
      this.weaponManager.BananaGunModel = this._bundle.LoadAsset<GameObject>("Banan");
      this.weaponManager.LaserGunModel = this._bundle.LoadAsset<GameObject>("LaserGun");
      this.weaponManager.MelonCannonModel = this._bundle.LoadAsset<GameObject>("Cannon");
      this.weaponManager.MelonModel = this._bundle.LoadAsset<GameObject>("Melon");
      this.weaponManager.MelonExplodeModel = this._bundle.LoadAsset<GameObject>("MelonExplode");
      this.weaponManager.HitPointParticle = this._bundle.LoadAsset<GameObject>("HitPoint");
      this.weaponManager.AssultRiffle = this._bundle.LoadAsset<GameObject>("AssaultRifle");
      this.weaponManager.LaserExplode = this._bundle.LoadAsset<GameObject>("Explosion 2");
      this.weldManager = this._itemsContainer.AddComponent<WeldManager>();
      this.freezeManager = this._itemsContainer.AddComponent<FreezeManager>();
      this.balloonManager = this._itemsContainer.AddComponent<BalloonManager>();
      this.balloonManager.Balloon = this._bundle.LoadAsset<GameObject>("Balloon");
      this.ragdollManager.Body = this._bundle.LoadAsset<GameObject>("Body");
      this.ragdollManager.Gorilla = this._bundle.LoadAsset<GameObject>("GorillaBody");
      this.physGunManager = this._itemsContainer.AddComponent<PhysGunManager>();
      this.hammerManager = this._itemsContainer.AddComponent<HammerManager>();
      this.hammerManager.asset = this._bundle.LoadAsset<GameObject>("Hammer_Weapon");
      this.grenadeManager = this._itemsContainer.AddComponent<GrenadeManager>();
      this.grenadeManager.Grenade = this._bundle.LoadAsset<GameObject>("Grenade");
      this.grenadeManager.Explode = this._bundle.LoadAsset<GameObject>("Explosion");
      this._initialized = true;
      this._list = Object.Instantiate<GameObject>(this._bundle.LoadAsset<GameObject>("List"));
      this._listManager = this._list.AddComponent<SandboxMenu>();
      this._listManager._text = this._bundle.LoadAsset<GameObject>("Temp");
      ((Object) this._list).name = "List";
      this._list.SetActive(false);
      ((Component) this._list.transform.GetChild(0).GetChild(0).GetChild(1)).GetComponent<Text>().text = "1.2.4";
      if (!this._initialized)
        return;
      this.ragdollManager.IsEditing = false;
      this.springManager.editMode = false;
      this.weaponManager.editMode = false;
      this.thrusterManager.editMode = false;
      this.C4Control.editMode = false;
      this.boxManager.IsEditing = false;
      this.sphereManager.IsEditing = false;
      this.beanManager.IsEditing = false;
      this.crateManager.IsEditing = false;
      this.weldManager.editMode = false;
      this.bathManager.IsEditing = false;
      this.balloonManager.editMode = false;
      this.freezeManager.editMode = false;
      this.physGunManager.editMode = false;
      this.gravityManager.editMode = false;
      this.airstrikeManager.editMode = false;
      this.couchManager.IsEditing = false;
      this.hammerManager.editMode = false;
      this.grenadeManager.editMode = false;
    }

    public void OnJoin()
    {
      if (NetworkSystem.Instance.GameModeString.Contains("MODDED_"))
        Plugin.InRoom = true;
      foreach (Component component in this._itemsContainer.transform)
        component.gameObject.SetActive(true);
    }

    public void OnLeave()
    {
      Plugin.InRoom = false;
      foreach (Component component in this._itemsContainer.transform)
        component.gameObject.SetActive(false);
      this._list.SetActive(false);
    }

    public void Update()
    {
      if (Object.op_Inequality((Object) GTPlayer.Instance, (Object) null))
      {
        if (!this.hasInit)
        {
          this.hasInit = true;
          this.OnGameInitialized();
        }
        if (PhotonNetwork.InRoom && !this.lastInRoom)
          this.OnJoin();
        if (!PhotonNetwork.InRoom && this.lastInRoom)
          this.OnLeave();
        this.lastInRoom = PhotonNetwork.InRoom;
      }
      if (Object.op_Inequality((Object) GTPlayer.Instance, (Object) null))
        RefCache.HitExists = Physics.Raycast(GTPlayer.Instance.rightControllerTransform.position, GTPlayer.Instance.rightControllerTransform.forward, ref RefCache.Hit, 2000f, LayerMask.op_Implicit(this._layerMask));
      if (Plugin.InRoom && ((Behaviour) this).enabled && this._initialized)
      {
        bool flag = (double) InputHandling.LeftGrip > 0.60000002384185791;
        if (this._list.activeInHierarchy)
        {
          this.boxManager.IsEditing = this._listManager.objectButtons[0] || this._listManager.objectButtons[7];
          this.boxManager.IsPlane = this._listManager.objectButtons[7];
          this.sphereManager.IsEditing = this._listManager.objectButtons[1] || this._listManager.objectButtons[11] || this._listManager.funButtons[0];
          this.sphereManager.IsSoftbody = this._listManager.objectButtons[11];
          this.sphereManager.IsEnemy = this._listManager.funButtons[0];
          this.beanManager.IsEditing = this._listManager.objectButtons[2] || this._listManager.objectButtons[4] || this._listManager.objectButtons[5];
          this.beanManager.IsBarrel = this._listManager.objectButtons[4];
          this.beanManager.IsWheel = this._listManager.objectButtons[5];
          this.ragdollManager.IsEditing = this._listManager.objectButtons[8] || this._listManager.objectButtons[9];
          this.ragdollManager.UseGorilla = this._listManager.objectButtons[9];
          this.crateManager.IsEditing = this._listManager.objectButtons[3];
          this.couchManager.IsEditing = this._listManager.objectButtons[6];
          this.bathManager.IsEditing = this._listManager.objectButtons[10];
          this.weaponManager.editMode = this._listManager.weaponButtons[0] || this._listManager.weaponButtons[1] || this._listManager.weaponButtons[2] || this._listManager.weaponButtons[4] || this._listManager.weaponButtons[3] || this._listManager.weaponButtons[7] || this._listManager.weaponButtons[8] || this._listManager.toolButtons[4];
          this.C4Control.editMode = this._listManager.weaponButtons[5] || this._listManager.weaponButtons[9];
          this.C4Control.IsMine = this._listManager.weaponButtons[9];
          this.airstrikeManager.editMode = this._listManager.weaponButtons[6];
          this.hammerManager.editMode = this._listManager.weaponButtons[11];
          this.grenadeManager.editMode = this._listManager.weaponButtons[10];
          if (this._listManager.weaponButtons[0])
            this.weaponManager.currentWeapon = 0;
          else if (this._listManager.weaponButtons[1])
            this.weaponManager.currentWeapon = 1;
          else if (this._listManager.weaponButtons[4])
            this.weaponManager.currentWeapon = 2;
          else if (this._listManager.weaponButtons[3])
            this.weaponManager.currentWeapon = 3;
          else if (this._listManager.weaponButtons[7])
            this.weaponManager.currentWeapon = 4;
          else if (this._listManager.weaponButtons[8])
            this.weaponManager.currentWeapon = 5;
          else if (this._listManager.toolButtons[4])
            this.weaponManager.currentWeapon = 6;
          else if (this._listManager.weaponButtons[2])
            this.weaponManager.currentWeapon = 7;
          this.weldManager.editMode = this._listManager.toolButtons[0];
          this.thrusterManager.editMode = this._listManager.toolButtons[1];
          this.springManager.editMode = this._listManager.toolButtons[2];
          this.physGunManager.editMode = this._listManager.toolButtons[3];
          this.freezeManager.editMode = this._listManager.toolButtons[5];
          this.gravityManager.editMode = this._listManager.toolButtons[6];
          this.balloonManager.editMode = this._listManager.toolButtons[7];
          if (this._listManager.utilButtons[0])
          {
            foreach (Component component in this._itemsContainer.transform)
              Object.Destroy((Object) component.gameObject);
          }
          if (this._listManager.utilButtons[1] && this.thrusterManager.objectList.Count > 0)
          {
            foreach (GameObject gameObject in this.thrusterManager.objectList)
            {
              if (!Object.op_Equality((Object) gameObject, (Object) null))
                Object.Destroy((Object) gameObject);
            }
            this.thrusterManager.objectList.Clear();
          }
          if (this._listManager.utilButtons[2] && this.springManager.objectList.Count > 0)
          {
            foreach (GameObject gameObject in this.springManager.objectList)
            {
              if (!Object.op_Equality((Object) gameObject, (Object) null))
                Object.Destroy((Object) gameObject);
            }
            this.springManager.objectList.Clear();
          }
          if (this._listManager.utilButtons[3] && this.balloonManager.objectList.Count > 0)
          {
            foreach (GameObject gameObject in this.balloonManager.objectList)
            {
              if (!Object.op_Equality((Object) gameObject, (Object) null))
                Object.Destroy((Object) gameObject);
            }
            this.balloonManager.objectList.Clear();
          }
        }
        if (this._list.activeSelf == flag)
          return;
        this._list.SetActive(flag);
      }
      else
      {
        if (Object.op_Inequality((Object) this._list, (Object) null) && this._list.activeSelf)
          this._list.SetActive(false);
        if (this._initialized)
        {
          this.ragdollManager.IsEditing = false;
          this.springManager.editMode = false;
          this.weaponManager.editMode = false;
          this.thrusterManager.editMode = false;
          this.C4Control.editMode = false;
          this.boxManager.IsEditing = false;
          this.sphereManager.IsEditing = false;
          this.beanManager.IsEditing = false;
          this.crateManager.IsEditing = false;
          this.weldManager.editMode = false;
          this.bathManager.IsEditing = false;
          this.balloonManager.editMode = false;
          this.freezeManager.editMode = false;
          this.physGunManager.editMode = false;
          this.gravityManager.editMode = false;
          this.airstrikeManager.editMode = false;
          this.couchManager.IsEditing = false;
          this.hammerManager.editMode = false;
          this.grenadeManager.editMode = false;
        }
      }
    }
  }
}
