using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum EnumSkinType
{
    skinSlot = 0,
    pantSlot = 1,
}

public class UI_ShopPageSkin : UICanvas
{
    [Header("Player")]
    [SerializeField] private Player player;

    [Header("Slot Skin Button")]
    [SerializeField] private BtnSlotSkin[] arrBtnSlotSkins;
    private BtnSlotSkin btnSlotSkinChoosing;

    [Header("Material Button")]
    [SerializeField] private BtnMaterialPant[] arrBtnMaterPant;
    [SerializeField] private BtnMaterialSkin[] arrBtnMaterSkin;
    private BtnMaterialPant btnMaterialPantChoosing;
    private BtnMaterialSkin btnMaterialSkinChoosing;

    [Header("Prefab")]
    [SerializeField] private BtnMaterialPant btnMaterPantPrefab;
    [SerializeField] private BtnMaterialSkin btnMaterSkinPrefab;

    [Header("Parent")]
    [SerializeField] private Transform containerbtnMaterPant;
    [SerializeField] private Transform containerbtnMaterSkin;
    [SerializeField] private GameObject scrollViewPant;
    [SerializeField] private GameObject scrollViewSkin;

    [Header("Camera View")]
    [SerializeField] private Transform containerCamera;
    [SerializeField] private CharaterShow charaterShowPrefab;
    private CharaterShow charaterShowing;

    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        charaterShowing = Instantiate(charaterShowPrefab, containerCamera);
        BtnMaterPant_Instantiate();
        BtnMaterSkin_Instantiate();

        player = CharacterManage.Ins.Player;

        SlotSkinBtn_Default();
        MaterialBtn_Default();
    }





    private void SlotSkinBtn_Default()
    {
        btnSlotSkinChoosing = arrBtnSlotSkins[0];
        SlotSkinBtn_OnChoose(arrBtnSlotSkins[0]);
        SlotSkinBtn_All_ChangeMater();
    }

    public void SlotSkinBtn_OnChoose(BtnSlotSkin btnSlotSkin)
    {
        btnSlotSkinChoosing.SelectSquare.SetActive(false);
        btnSlotSkinChoosing = btnSlotSkin;
        btnSlotSkinChoosing.SelectSquare.SetActive(true);

        MaterialBtn_Display(btnSlotSkinChoosing);
    }


    public void SlotSkinBtn_ChangeMater()
    {
        btnSlotSkinChoosing.ChangeMater(DataManager.Ins.GetMaterialEquippedInThisSlot(btnSlotSkinChoosing.Type).color);
        ChangeMaterialCharaterShowing(btnSlotSkinChoosing);

        player.SetSkinsMaterial();
    }

    public void SlotSkinBtn_All_ChangeMater()
    {
        for (int i = 0; i < arrBtnSlotSkins.Length; i++)
        {
            EnumSkinType enumSkinType = (EnumSkinType)Enum.Parse(typeof(EnumSkinType), i.ToString());
            arrBtnSlotSkins[i].ChangeMater(DataManager.Ins.GetMaterialEquippedInThisSlot(enumSkinType).color);
            ChangeMaterialCharaterShowing(arrBtnSlotSkins[i]);
        }

        player.SetSkinsMaterial();
    }





    private void BtnMaterPant_Instantiate()
    {
        arrBtnMaterPant = new BtnMaterialPant[DataManager.Ins.EnumMaterialsPantLenght()];

        for (int i = 0; i < DataManager.Ins.EnumMaterialsPantLenght(); i++)
        {
            BtnMaterialPant btnMaterialPantTemp = Instantiate(btnMaterPantPrefab, containerbtnMaterPant);
            btnMaterialPantTemp.OnInit((EnumMaterialsPantData)Enum.Parse(typeof(EnumMaterialsPantData), i.ToString()), this);
            arrBtnMaterPant[i] = btnMaterialPantTemp;
        }
    }

    private void BtnMaterSkin_Instantiate()
    {
        arrBtnMaterSkin = new BtnMaterialSkin[DataManager.Ins.EnumMaterialsSkinLenght()];

        for (int i = 0; i < DataManager.Ins.EnumMaterialsSkinLenght(); i++)
        {
            BtnMaterialSkin btnMaterialSkinTemp = Instantiate(btnMaterSkinPrefab, containerbtnMaterSkin);
            btnMaterialSkinTemp.OnInit((EnumMaterialsSkinData)Enum.Parse(typeof(EnumMaterialsSkinData), i.ToString()), this);
            arrBtnMaterSkin[i] = btnMaterialSkinTemp;
        }
    }

    private void MaterialBtn_Default()
    {
        for (int i = 0; i < arrBtnMaterPant.Length; i++)
        {
            if (DataManager.Ins.IsThisMaterialOwnerShip(arrBtnMaterPant[i].EnumMaterialsPantData, EnumOwnerShip.Equipped))
            {
                MaterialBtn_Choose(arrBtnMaterPant[i]);
                break;
            }
        }

        for (int i = 0; i < arrBtnMaterSkin.Length; i++)
        {
            if (DataManager.Ins.IsThisMaterialOwnerShip(arrBtnMaterSkin[i].EnumMaterialsSkinData, EnumOwnerShip.Equipped))
            {
                MaterialBtn_Choose(arrBtnMaterSkin[i]);
                break;
            }
        }
    }

    public void MaterialBtn_Choose(BtnMaterialSkin btnMaterSkin)
    {
        if (btnMaterialSkinChoosing == btnMaterSkin)
        {
            return;
        }

        if (btnMaterialSkinChoosing != null)
        {
            btnMaterialSkinChoosing.SelectSquare.SetActive(false);
            DataManager.Ins.ChangeDataOwnerShipOfMaterial(btnMaterialSkinChoosing.EnumMaterialsSkinData, EnumOwnerShip.CanUse);
        }

        btnMaterialSkinChoosing = btnMaterSkin;
        DataManager.Ins.ChangeDataOwnerShipOfMaterial(btnMaterialSkinChoosing.EnumMaterialsSkinData, EnumOwnerShip.Equipped);
        btnMaterialSkinChoosing.SelectSquare.SetActive(true);

        SlotSkinBtn_ChangeMater();
    }

    public void MaterialBtn_Choose(BtnMaterialPant btnMaterPant)
    {
        if (btnMaterialPantChoosing == btnMaterPant)
        {
            return;
        }

        if (btnMaterialPantChoosing != null)
        {
            btnMaterialPantChoosing.SelectSquare.SetActive(false);
            DataManager.Ins.ChangeDataOwnerShipOfMaterial(btnMaterialPantChoosing.EnumMaterialsPantData, EnumOwnerShip.CanUse);
        }

        btnMaterialPantChoosing = btnMaterPant;
        DataManager.Ins.ChangeDataOwnerShipOfMaterial(btnMaterialPantChoosing.EnumMaterialsPantData, EnumOwnerShip.Equipped);
        btnMaterialPantChoosing.SelectSquare.SetActive(true);

        SlotSkinBtn_ChangeMater();
    }

    public void MaterialBtn_Display(BtnSlotSkin btnSlotSkin)
    {
        if (btnSlotSkin.Type == EnumSkinType.skinSlot)
        {
            scrollViewPant.gameObject.SetActive(false);
            scrollViewSkin.gameObject.SetActive(true);
        }
        else
        {
            scrollViewSkin.gameObject.SetActive(false);
            scrollViewPant.gameObject.SetActive(true);
        }
    }




    public void ChangeMaterialCharaterShowing(BtnSlotSkin btnSlotSkin) 
    {
        if (btnSlotSkin.Type == EnumSkinType.skinSlot)
        {
            charaterShowing.Skin.material = DataManager.Ins.GetMaterialEquippedInThisSlot(btnSlotSkin.Type);
        } 
        else if (btnSlotSkin.Type == EnumSkinType.pantSlot)
        {
            charaterShowing.Pant.material = DataManager.Ins.GetMaterialEquippedInThisSlot(btnSlotSkin.Type);
        }
    }
}
