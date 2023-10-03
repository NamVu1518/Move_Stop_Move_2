using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MakeDataDefault : MonoBehaviour
{

    public ScriptableDataUI scriptableDataUI;
    private DataDynamic dataDynamicDefault = new DataDynamic();
    private List<EnumMaterialsWeaponData> listFullEnumMater = new List<EnumMaterialsWeaponData>();

    private void Start()
    {
        SetDataDefault();
        
    }

    private void PantCreate()
    {
        int listCount = scriptableDataUI.listMaterialPantData.Count;
        dataDynamicDefault.exteriorOwners.pants = new Pant[listCount];

        for (int i = 0; i < listCount; i++)
        {
            dataDynamicDefault.exteriorOwners.pants[i] = new Pant();
            dataDynamicDefault.exteriorOwners.pants[i].pantData = (EnumMaterialsPantData)Enum.Parse(typeof(EnumMaterialsPantData), i.ToString());
            
            if (i == 0)
            {
                dataDynamicDefault.exteriorOwners.pants[i].ownerShip = EnumOwnerShip.Equipped;
                continue;
            }

            dataDynamicDefault.exteriorOwners.pants[i].ownerShip = EnumOwnerShip.BuyCoin;
        }
    }

    private void SkinCreate()
    {
        int listCount = scriptableDataUI.listMaterialSkinData.Count;
        dataDynamicDefault.exteriorOwners.skins = new Skin[listCount];

        for (int i = 0; i < listCount; i++)
        {
            dataDynamicDefault.exteriorOwners.skins[i] = new Skin();
            dataDynamicDefault.exteriorOwners.skins[i].skinData = (EnumMaterialsSkinData)Enum.Parse(typeof(EnumMaterialsSkinData), i.ToString());

            if (i == 0)
            {
                dataDynamicDefault.exteriorOwners.skins[i].ownerShip = EnumOwnerShip.Equipped;
                continue;
            }

            dataDynamicDefault.exteriorOwners.skins[i].ownerShip = EnumOwnerShip.BuyCoin;
        }
    }

    private void SetDataDefault()
    {
        dataDynamicDefault.exteriorOwners = new Exterior();

        SkinCreate();
        PantCreate();

        Debug.Log(true);


        EnumMaterialsWeaponData[] arrFullEnumMater = (EnumMaterialsWeaponData[])Enum.GetValues(typeof(EnumMaterialsWeaponData));
        listFullEnumMater = arrFullEnumMater.ToList();
        dataDynamicDefault.arrWeaponOwners = new WeaponOwner[scriptableDataUI.listShopPageInfo.Count];

        int lenghtMater = 3;

        for (int i = 0; i < scriptableDataUI.listShopPageInfo.Count; i++)
        {
            SetDataDefaultWeapon(ref dataDynamicDefault.arrWeaponOwners[i], i, ref lenghtMater);
        }

        scriptableDataUI.dataDynamic = dataDynamicDefault;
    }

    private void SetDataDefaultMater(ref MaterialOnwer materialOnwer, int index, ref List<EnumMaterialsWeaponData> enumMaterialsColorDatas)
    {
        materialOnwer = new MaterialOnwer();
        int rd = UnityEngine.Random.Range(0, enumMaterialsColorDatas.Count);
        materialOnwer.enumMaterial = enumMaterialsColorDatas[rd];
        enumMaterialsColorDatas.RemoveAt(rd);

        if (index == 0)
        {
            materialOnwer.enumOwnerShip = EnumOwnerShip.Equipped;
        }
        else
        {
            materialOnwer.enumOwnerShip = EnumOwnerShip.BuyCoin;
        }
    }

    private void SlotCreate(ref SlotMaterWeapon slotMaterWeapon, int countLenghtMater)
    {
        slotMaterWeapon = new SlotMaterWeapon();
        slotMaterWeapon.arrMaterial = new MaterialOnwer[countLenghtMater];

        List<EnumMaterialsWeaponData> enumMaterialsColorDatas = new List<EnumMaterialsWeaponData>(listFullEnumMater);

        for (int k = 0; k < slotMaterWeapon.arrMaterial.Length; k++)
        {
            SetDataDefaultMater(ref slotMaterWeapon.arrMaterial[k], k, ref enumMaterialsColorDatas);
        }
    }

    private void SetDataDefaultWeapon(ref WeaponOwner weaponOwner, int index, ref int countLenghtMater)
    {
        weaponOwner = new WeaponOwner();
        weaponOwner._name = scriptableDataUI.listShopPageInfo[index].nameWeapon;

        weaponOwner.enumBulletsData = (EnumBulletAndWeaponssData)Enum.Parse(typeof(EnumBulletAndWeaponssData), (index + 1).ToString());

        if (weaponOwner._name == "Arrow")
        {
            weaponOwner.ownerShip = EnumOwnerShip.Equipped;
        }
        else
        {
            weaponOwner.ownerShip = EnumOwnerShip.BuyCoin;
        }

        weaponOwner.arrSlots = new SlotMaterWeapon[scriptableDataUI.listShopPageInfo[index].slotMater];

        for (int j = 0; j < weaponOwner.arrSlots.Length; j++)
        {
            SlotCreate(ref weaponOwner.arrSlots[j], countLenghtMater);
        }

        countLenghtMater++;
    }
}
