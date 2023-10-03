using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum EnumOwnerShip
{
    Equipped = 0,
    CanUse = 1,
    BuyCoin = 2,
    BuyMoney = 3,
}

public enum EnumMaterialsWeaponData
{
    white = 0,
    black = 1,
    red = 2,
    green = 3,
    blue = 4,
    yellow = 5,
    cyan = 6,
    magenta = 7,
    orange = 8,
    lightRed = 9,
    lightGreen = 10,
    lightBlue = 11,
    lightYellow = 12,
    lightCyan = 13,
    lightMagenta = 14,
    lightOrange = 15,
    darkRed = 16,
    darkGreen = 17,
    darkBlue = 18,
    darkYellow = 19,
    darkCyan = 20,
    darkMagenta = 21,
    darkOrange = 22,
}

public enum EnumMaterialsSkinData
{
    white = 0,
    black = 1,
    red = 2,
    green = 3,
    blue = 4,
    yellow = 5,
    cyan = 6,
    magenta = 7,
    orange = 8,
    lightRed = 9,
    lightGreen = 10,
    lightBlue = 11,
    lightYellow = 12,
    lightCyan = 13,
    lightMagenta = 14,
    lightOrange = 15,
    darkRed = 16,
    darkGreen = 17,
    darkBlue = 18,
    darkYellow = 19,
    darkCyan = 20,
    darkMagenta = 21,
    darkOrange = 22,
}


public enum EnumMaterialsPantData
{
    white = 0,
    black = 1,
    red = 2,
    green = 3,
    blue = 4,
    yellow = 5,
    cyan = 6,
    magenta = 7,
    orange = 8,
    lightRed = 9,
    lightGreen = 10,
    lightBlue = 11,
    lightYellow = 12,
    lightCyan = 13,
    lightMagenta = 14,
    lightOrange = 15,
    darkRed = 16,
    darkGreen = 17,
    darkBlue = 18,
    darkYellow = 19,
    darkCyan = 20,
    darkMagenta = 21,
    darkOrange = 22,
    batman = 23,
    chambi = 24,
    comi = 25,
    dabao = 26,
    onion = 27,
    pokemom = 28,
    rainbow = 29,
    skull = 30,
    vantim = 31,
}

[CreateAssetMenu(menuName = "DataUI", fileName = "DataUI")]
public class ScriptableDataUI : ScriptableObject
{
    public List<Material> listMaterialWeaponData = new List<Material>();

    public List<Material> listMaterialSkinData = new List<Material>();

    public List<Material> listMaterialPantData = new List<Material>();

    public List<ShopPageInfo> listShopPageInfo = new List<ShopPageInfo>();

    public DataDynamic dataDynamic = new DataDynamic();

    public Material TakeMaterial(EnumMaterialsWeaponData materialColor)
    {
        return listMaterialWeaponData[(int)materialColor];
    }

    public Material TakeMaterial(EnumMaterialsPantData materialColor)
    {
        return listMaterialPantData[(int)materialColor];
    }

    public Material TakeMaterial(EnumMaterialsSkinData materialColor)
    {
        return listMaterialSkinData[(int)materialColor];
    }
}




[System.Serializable]
public class ShopPageInfo
{
    public string nameWeapon;
    public GameObject cameraTexture;
    public string coin;
    public int slotMater;
}




[System.Serializable]
public class DataDynamic
{
    public WeaponOwner[] arrWeaponOwners;
    public Exterior exteriorOwners;
}





[System.Serializable] 
public class Exterior
{
    public Skin[] skins;
    public Pant[] pants;
}

[System.Serializable]
public class Skin
{
    public EnumMaterialsSkinData skinData;
    public EnumOwnerShip ownerShip;
}

[System.Serializable]
public class Pant
{
    public EnumMaterialsPantData pantData;
    public EnumOwnerShip ownerShip;
}




[System.Serializable]
public class WeaponOwner
{
    [Header("Weapon")]
    public string _name;
    public EnumBulletAndWeaponssData enumBulletsData;
    public EnumOwnerShip ownerShip;

    [Header("Slot")]
    public SlotMaterWeapon[] arrSlots;
}

[System.Serializable]
public class SlotMaterWeapon
{
    public MaterialOnwer[] arrMaterial;
}

[System.Serializable]
public class MaterialOnwer
{
    public EnumMaterialsWeaponData enumMaterial;
    public EnumOwnerShip enumOwnerShip;
}

