using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShopPage : UICanvas
{
    [Header("Shop Page Info")]
    [SerializeField] private TextMeshProUGUI _nameWeaponText;
    [SerializeField] private Button _buttonBuy;
    [SerializeField] private TextMeshProUGUI _feeText;

    [Header("Camera")]
    [SerializeField] private Transform cameraContainer;

    [Header("Button Buy Type")]
    [SerializeField] private Button[] arrayBtnBuyType;
    [SerializeField] private BtnBuyCoin btnBuyCoin;

    [Header("Button Slot")]
    public BtnSlot[] arrayBtnSlot;
    private BtnSlot btnSlotChoosing;

    [Header("Button Color")]
    [SerializeField] private BtnMaterialWeapon btnMater;
    [SerializeField] private RectTransform containerBtnMater;
    public List<BtnMaterialWeapon> listBtnColors = new List<BtnMaterialWeapon>();
    private BtnMaterialWeapon btnMaterialChoosing;

    private GameObject _cameraTexture;
    private GameObject weaponShowing;



    private int numWeaponIndex = 0;
    public int NunWeaponIndex
    { 
        set { numWeaponIndex = value; }
        get { return numWeaponIndex; }
    }

    internal int maxIndex => DataManager.Ins.ScriptableDataUI.listShopPageInfo.Count - 1;

    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        BuyBtn_SetActiveFalseAllExcute();
        InstantiateColorButton(btnMater, containerBtnMater);
        ChangeDataWeaponPage(numWeaponIndex);
    }

    private void InstantiateColorButton(BtnMaterialWeapon btnColor, RectTransform btnColorParentTransform)
    {
        for (int i = 0; i < DataManager.Ins.numCountMaterial; i++)
        {
            BtnMaterialWeapon btnColorTamp = Instantiate(btnColor, btnColorParentTransform);
            btnColorTamp.OnInit((EnumMaterialsWeaponData)Enum.Parse(typeof(EnumMaterialsWeaponData), i.ToString()), this);      
            listBtnColors.Add(btnColorTamp);
        }
    }

    public void ChangeDataWeaponPage(int indexWeapon)
    {
        _nameWeaponText.SetText(DataManager.Ins.ScriptableDataUI.listShopPageInfo[indexWeapon].nameWeapon);
        _feeText.SetText(DataManager.Ins.ScriptableDataUI.listShopPageInfo[indexWeapon].coin);
        GetCamera(DataManager.Ins.ScriptableDataUI.listShopPageInfo[indexWeapon].cameraTexture);        
        BuyBtn_SetActiveTrueType(DataManager.Ins.DataDynamic.arrWeaponOwners[indexWeapon].ownerShip);
        SlotBtn_Default();
        MaterialBtn_Default();
        

        if (btnBuyCoin.button.gameObject.activeInHierarchy == true)
        {
            btnBuyCoin.SetTextFee(DataManager.Ins.ScriptableDataUI.listShopPageInfo[indexWeapon].coin + " Coin");
        }
    }



    private void GetCamera(GameObject camera)
    {
        if (_cameraTexture != null)
        {
            Destroy(_cameraTexture);
        }

        _cameraTexture = Instantiate(camera, cameraContainer);
        weaponShowing = _cameraTexture.GetComponent<WeaponShowCamera>().RawWeapon;
    }


        
    public void BuyBtn_SetActiveFalseAllExcute()
    {
        for (int i = 0; i < arrayBtnBuyType.Length; i++)
        {
            arrayBtnBuyType[i].gameObject.SetActive(false);
        }
    }

    public void BuyBtn_SetActiveFalseAllExcute(EnumOwnerShip enumOwnerShip)
    {
        for (int i = 0; i < arrayBtnBuyType.Length; i++)
        {
            if (i == (int)enumOwnerShip)
            {
                arrayBtnBuyType[i].gameObject.SetActive(true);
            }
            else
            {
                arrayBtnBuyType[i].gameObject.SetActive(false);
            }
        }
    }

    public void BuyBtn_SetActiveTrueType(EnumOwnerShip enumOwnerShip)
    {
        switch(enumOwnerShip)
        {
            case EnumOwnerShip.Equipped:
                {
                    BuyBtn_SetActiveFalseAllExcute(enumOwnerShip);
                    break;
                }
            case EnumOwnerShip.CanUse:
                {
                    BuyBtn_SetActiveFalseAllExcute(enumOwnerShip);
                    break;
                }
            case EnumOwnerShip.BuyCoin:
                {
                    BuyBtn_SetActiveFalseAllExcute(enumOwnerShip);
                    break;
                }
            case EnumOwnerShip.BuyMoney:
                {
                    BuyBtn_SetActiveFalseAllExcute(enumOwnerShip);
                    break;
                }
            default:
                {
                    Debug.LogError("Not have this type");
                    break;
                }
        }
    }




    private void SlotBtn_Default()
    {
        SlotBtn_Display(DataManager.Ins.SlotsAmount(numWeaponIndex));
        btnSlotChoosing = arrayBtnSlot[0];
        SlotBtn_OnChoose(arrayBtnSlot[0]);
        SlotBtn_All_ChangeMater();
        
    }

    private void SlotBtn_Display(int slotAmount)
    {
        for (int i = 0; i < arrayBtnSlot.Length; i++)
        {
            if (i < slotAmount)
            {
                arrayBtnSlot[i].button.gameObject.SetActive(true);
                continue;
            }

            arrayBtnSlot[i].button.gameObject.SetActive(false);
        }
    }

    public void SlotBtn_OnChoose(BtnSlot btnSlot)
    {
        btnSlotChoosing.SelectSquare.SetActive(false);
        btnSlotChoosing = btnSlot;
        btnSlotChoosing.SelectSquare.SetActive(true);

        MaterialBtn_Display(numWeaponIndex);
        MaterialBtn_Default();
    }


    public void SlotBtn_ChangeMater()
    {
        btnSlotChoosing.ChangeMater(DataManager.Ins.GetMaterialEquippedInThisSlot(numWeaponIndex, btnSlotChoosing.Index).color);
        SetMaterialForWeaponShow(weaponShowing, DataManager.Ins.GetMaterialArrayEquippedInThisWeapon(numWeaponIndex));
    }

    public void SlotBtn_All_ChangeMater()
    {
        for (int i = 0; i < arrayBtnSlot.Length; i++)
        {
            if (!arrayBtnSlot[i].button.IsActive())
            {
                return;
            }

            arrayBtnSlot[i].ChangeMater(DataManager.Ins.GetMaterialEquippedInThisSlot(numWeaponIndex, arrayBtnSlot[i].Index).color);
        }
    }


    public void MaterialBtn_Default()
    {
        if (btnMaterialChoosing != null)
        {
            btnMaterialChoosing.SelectSquare.SetActive(false);
            btnMaterialChoosing = null;
        }
        
        MaterialBtn_Choose(listBtnColors[(int)DataManager.Ins.GetEnumMaterialEquippedInThisSlot(numWeaponIndex, btnSlotChoosing.Index)]);
    }

    public void MaterialBtn_Choose(BtnMaterialWeapon btnColor)
    {
        if (btnMaterialChoosing == btnColor)
        {
            return;
        }

        if (btnMaterialChoosing != null)
        {
            btnMaterialChoosing.SelectSquare.SetActive(false);
            DataManager.Ins.ChangeDataOwnerShipOfMaterial(numWeaponIndex, btnSlotChoosing.Index, btnMaterialChoosing.EnumMaterialsWeapon, EnumOwnerShip.CanUse);
        }

        btnMaterialChoosing = btnColor;
        DataManager.Ins.ChangeDataOwnerShipOfMaterial(numWeaponIndex, btnSlotChoosing.Index, btnMaterialChoosing.EnumMaterialsWeapon, EnumOwnerShip.Equipped);
        btnMaterialChoosing.SelectSquare.SetActive(true);

        SlotBtn_ChangeMater();
    }

    public void MaterialBtn_Display(int indexWeapon)
    {
        for (int i = 0; i < listBtnColors.Count; i++)
        {
            for (int j = 0; j < DataManager.Ins.MaterialAmount(indexWeapon, btnSlotChoosing.Index); j++)
            {
                if (listBtnColors[i].EnumMaterialsWeapon == DataManager.Ins.GetEnumMaterialWeaponDynamicData(indexWeapon, btnSlotChoosing.Index, j))
                {
                    listBtnColors[i].button.gameObject.SetActive(true);
                    break;
                }

                listBtnColors[i].button.gameObject.SetActive(false);
            }
        }
    }




    public void SetMaterialForWeaponShow(GameObject weaponShowing, Material[] materials)
    {
        weaponShowing.GetComponent<Renderer>().materials = materials;
    }
}
