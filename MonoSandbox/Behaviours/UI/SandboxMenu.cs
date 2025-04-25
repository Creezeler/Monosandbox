// Decompiled with JetBrains decompiler
// Type: MonoSandbox.Behaviours.UI.SandboxMenu
// Assembly: MonoSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9715068-C229-4EA2-B292-9AF3D905BE89
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag\BepInEx\plugins\MonoSandbox.dll

using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace MonoSandbox.Behaviours.UI
{
  public class SandboxMenu : MonoBehaviour
  {
    public GameObject _menu;
    public GameObject _text;
    public GameObject _objParent;
    public GameObject _toolParent;
    public GameObject _utilsParent;
    public GameObject _weaponsParent;
    public GameObject _funParent;
    public GameObject _sideBtnParent;
    public GameObject _sender;
    public int _currentPage;
    public bool[] objectButtons = new bool[12];
    public bool[] weaponButtons = new bool[12];
    public bool[] toolButtons = new bool[8];
    public bool[] utilButtons = new bool[4];
    public bool[] funButtons = new bool[1];
    public string[] objectNames = new string[12]
    {
      "Box",
      "Sphere",
      "Bean",
      "Crate",
      "Barrel",
      "Wheel",
      "Couch",
      "Plane",
      "Body",
      "Gorilla",
      "Bathtub",
      "Soft Sphere"
    };
    public string[] weaponNames = new string[12]
    {
      "Revolver",
      "Shotgun",
      "Rifle",
      "Sniper",
      "Melon Cannon",
      "C4",
      "Airstrike",
      "Laser Gun",
      "Banana Gun",
      "Mine",
      "Grenade",
      "Hammer"
    };
    public string[] toolNames = new string[8]
    {
      "Weld",
      "Thruster",
      "Spring",
      "Gravity Gun",
      "Colourize",
      "Freeze",
      "Toggle Gravity",
      "Balloon"
    };
    public string[] utilNames = new string[4]
    {
      "Remove All",
      "Remove Thrusters",
      "Remove Springs",
      "Remove Balloons"
    };
    public string[] funNames = new string[1]{ "Entity" };
    private Canvas _canvas;
    private AudioSource _audioSource;

    public void Start()
    {
      this._menu = ((Component) ((Component) this).transform.GetChild(1)).gameObject;
      this._canvas = ((Component) this._menu.transform.GetChild(0)).gameObject.GetComponent<Canvas>();
      this._objParent = new GameObject();
      this._weaponsParent = new GameObject();
      this._toolParent = new GameObject();
      this._utilsParent = new GameObject();
      this._funParent = new GameObject();
      this._sideBtnParent = new GameObject();
      ((Object) this._sideBtnParent).name = "SideButtons";
      this._sideBtnParent.transform.SetParent(this._menu.transform, false);
      this.AddPage(this.objectNames, "Objects", 4, this._objParent, 0);
      this.AddPage(this.weaponNames, "Weapons", 4, this._weaponsParent, 1);
      this.AddPage(this.toolNames, "Tools", 4, this._toolParent, 2);
      this.AddPage(this.utilNames, "Utils", 4, this._utilsParent, 3);
      this.AddPage(this.funNames, "Fun", 4, this._funParent, 4);
      ((Component) this).transform.SetParent(RefCache.LHand.transform, false);
      ((Component) this).transform.localPosition = new Vector3(0.0f, 0.14f, 0.075f);
      ((Component) this).transform.localScale = Vector3.op_Multiply(Vector3.one, 0.5f);
      ((Component) this).transform.localEulerAngles = new Vector3(0.0f, 90f, -5f);
      this._audioSource = ((Component) this).gameObject.AddComponent<AudioSource>();
      this._audioSource.playOnAwake = false;
      this._audioSource.volume = 0.4f;
      this._audioSource.spatialBlend = 1f;
      this._audioSource.clip = RefCache.PageSelection;
      this._audioSource.Play();
    }

    public void Update()
    {
      if (Object.op_Equality((Object) this._objParent, (Object) null) || Object.op_Equality((Object) this._weaponsParent, (Object) null) || Object.op_Equality((Object) this._utilsParent, (Object) null) || Object.op_Equality((Object) this._toolParent, (Object) null) || Object.op_Equality((Object) this._funParent, (Object) null))
        return;
      if (this._currentPage == 0 && !this._objParent.activeSelf)
      {
        this._objParent.SetActive(true);
        ((Component) ((Component) this._canvas).transform.GetChild(0)).gameObject.SetActive(true);
      }
      else if (this._currentPage != 0 && this._objParent.activeSelf)
      {
        this._objParent.SetActive(false);
        ((Component) ((Component) this._canvas).transform.GetChild(0)).gameObject.SetActive(false);
      }
      if (this._currentPage == 1 && !this._weaponsParent.activeSelf)
      {
        this._weaponsParent.SetActive(true);
        ((Component) ((Component) this._canvas).transform.GetChild(2)).gameObject.SetActive(true);
      }
      else if (this._currentPage != 1 && this._weaponsParent.activeSelf)
      {
        this._weaponsParent.SetActive(false);
        ((Component) ((Component) this._canvas).transform.GetChild(2)).gameObject.SetActive(false);
      }
      if (this._currentPage == 2 && !this._toolParent.activeSelf)
      {
        this._toolParent.SetActive(true);
        ((Component) ((Component) this._canvas).transform.GetChild(4)).gameObject.SetActive(true);
      }
      else if (this._currentPage != 2 && this._toolParent.activeSelf)
      {
        this._toolParent.SetActive(false);
        ((Component) ((Component) this._canvas).transform.GetChild(4)).gameObject.SetActive(false);
      }
      if (this._currentPage == 3 && !this._utilsParent.activeSelf)
      {
        this._utilsParent.SetActive(true);
        ((Component) ((Component) this._canvas).transform.GetChild(6)).gameObject.SetActive(true);
      }
      else if (this._currentPage != 3 && this._utilsParent.activeSelf)
      {
        this._utilsParent.SetActive(false);
        ((Component) ((Component) this._canvas).transform.GetChild(6)).gameObject.SetActive(false);
      }
      if (this._currentPage == 4 && !this._funParent.activeSelf)
      {
        this._funParent.SetActive(true);
        ((Component) ((Component) this._canvas).transform.GetChild(8)).gameObject.SetActive(true);
      }
      else
      {
        if (this._currentPage == 4 || !this._funParent.activeSelf)
          return;
        this._funParent.SetActive(false);
        ((Component) ((Component) this._canvas).transform.GetChild(8)).gameObject.SetActive(false);
      }
    }

    public void AddPage(
      string[] buttonNames,
      string pageName,
      int perline,
      GameObject buttonParent,
      int pIndex)
    {
      int num1 = 0;
      buttonParent.transform.SetParent(this._menu.transform, false);
      ((Object) buttonParent).name = pageName;
      GameObject gameObject1 = new GameObject();
      ((Object) gameObject1).name = pageName;
      GameObject gameObject2 = gameObject1;
      gameObject2.transform.parent = ((Component) this._canvas).transform;
      GameObject primitive1 = GameObject.CreatePrimitive((PrimitiveType) 3);
      primitive1.layer = 18;
      ((Collider) primitive1.GetComponent<BoxCollider>()).isTrigger = true;
      primitive1.transform.SetParent(this._sideBtnParent.transform, false);
      primitive1.transform.localScale = new Vector3(0.02f, 0.07f, 0.225f);
      primitive1.transform.localPosition = new Vector3(0.0f, (float) (0.085000000894069672 - (double) pIndex * 0.10000000149011612), 0.745f);
      primitive1.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
      GameObject gameObject3 = Object.Instantiate<GameObject>(this._text);
      gameObject3.transform.SetParent(((Component) this._canvas).transform, false);
      ((Object) gameObject3).name = pageName;
      ((Transform) gameObject3.GetComponent<RectTransform>()).eulerAngles = new Vector3(0.0f, 90f, 0.0f);
      gameObject3.transform.position = Vector3.op_Addition(primitive1.transform.position, new Vector3(-0.015f, 0.0f, 0.0f));
      gameObject3.GetComponent<Text>().text = pageName.ToUpper();
      ((Graphic) gameObject3.GetComponent<Text>()).color = Color.black;
      PageButton pageButton = primitive1.AddComponent<PageButton>();
      pageButton._pageIndex = pIndex;
      pageButton._text = gameObject3;
      pageButton._list = this;
      for (int index = 0; index < buttonNames.Length; ++index)
      {
        GameObject primitive2 = GameObject.CreatePrimitive((PrimitiveType) 3);
        primitive2.layer = 18;
        ((Collider) primitive2.GetComponent<BoxCollider>()).isTrigger = true;
        primitive2.transform.localScale = new Vector3(0.025f, 0.145f, 0.145f);
        primitive2.transform.SetParent(buttonParent.transform, false);
        int num2 = (int) Mathf.Floor((float) (index / perline));
        primitive2.transform.localPosition = new Vector3(0.02f, (float) -num2 * (primitive2.transform.localScale.y + 0.03f), (float) (perline - 1 - num1) * (primitive2.transform.localScale.z + 0.02f));
        ++num1;
        if (num1 == perline)
          num1 = 0;
        GameObject gameObject4 = Object.Instantiate<GameObject>(this._text);
        gameObject4.transform.SetParent(gameObject2.transform, false);
        ((Transform) gameObject4.GetComponent<RectTransform>()).eulerAngles = new Vector3(0.0f, 90f, 0.0f);
        gameObject4.transform.position = Vector3.op_Addition(primitive2.transform.position, new Vector3(-0.015f, 0.0f, 0.0f));
        gameObject4.GetComponent<Text>().text = buttonNames[index].ToUpper();
        ((Graphic) gameObject4.GetComponent<Text>()).color = Color.black;
        Button button = primitive2.AddComponent<Button>();
        button._buttonIndex = index;
        button._text = gameObject4;
        button._list = this;
        primitive2.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
      }
    }

    public void Clear()
    {
      this.objectButtons = new bool[12];
      this.weaponButtons = new bool[12];
      this.toolButtons = new bool[8];
      this.utilButtons = new bool[4];
      this.funButtons = new bool[1];
    }

    public void PlayAudio(bool item)
    {
      this._audioSource.PlayOneShot(item ? RefCache.ItemSelection : RefCache.PageSelection);
    }

    public bool[] GetArray()
    {
      int currentPage = this._currentPage;
      if (true)
        ;
      bool[] array;
      switch (currentPage)
      {
        case 0:
          array = this.objectButtons;
          break;
        case 1:
          array = this.weaponButtons;
          break;
        case 2:
          array = this.toolButtons;
          break;
        case 3:
          array = this.utilButtons;
          break;
        case 4:
          array = this.funButtons;
          break;
        default:
          throw new IndexOutOfRangeException();
      }
      if (true)
        ;
      return array;
    }

    public void SetArray(bool[] array)
    {
      switch (this._currentPage)
      {
        case 0:
          this.objectButtons = array;
          break;
        case 1:
          this.weaponButtons = array;
          break;
        case 2:
          this.toolButtons = array;
          break;
        case 3:
          this.utilButtons = array;
          break;
        case 4:
          this.funButtons = array;
          break;
      }
    }
  }
}
