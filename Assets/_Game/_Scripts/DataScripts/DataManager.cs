using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] private ScriptableDataGamePlay scriptableDataGamePlay;
    [SerializeField] private ScriptableDataUI scriptableDataUI;
    private DataDynamic dataDynamic;
    private DataPlayer dataPlayer = new DataPlayer();


    public int numCountMaterial => scriptableDataUI.listMaterialWeaponData.Count;

    public ScriptableDataGamePlay ScriptableDataGamePlay
    {
        get { return scriptableDataGamePlay; }
    }

    public ScriptableDataUI ScriptableDataUI
    {
        get { return scriptableDataUI; }
    }

    public DataDynamic DataDynamic
    {
        get { return dataDynamic; }
    }

    public DataPlayer DataPlayer
    {
        set { dataPlayer = value; }
        get { return dataPlayer; }
    }

    #region Get Asset From Data
    public BulletData TakeWeaponData(EnumBulletAndWeaponssData enumWeaponsData)
    {
        return scriptableDataGamePlay.TakeBulletsOriginalData(enumWeaponsData);
    }

    public Material GetMaterial(EnumMaterialsWeaponData enumMaterialsColor)
    {
        return scriptableDataUI.TakeMaterial(enumMaterialsColor);
    }

    public Material GetMaterial(EnumMaterialsPantData enumMaterialsColor)
    {
        return scriptableDataUI.TakeMaterial(enumMaterialsColor);
    }

    public Material GetMaterial(EnumMaterialsSkinData enumMaterialsColor)
    {
        return scriptableDataUI.TakeMaterial(enumMaterialsColor);
    }

    public Weapon GetWeapon(EnumBulletAndWeaponssData enumWeapons)
    {
        return scriptableDataGamePlay.GetWeapon(enumWeapons);
    }
    #endregion

    #region Data process

    public void LoadDataPlayer()
    {
        dataPlayer = dataPlayer.LoadData();
    }

    public void SaveDataPlayer()
    {
        dataPlayer.SaveData(dataPlayer);
    }

    public void SaveDataDynamic()
    {
        string path = Path.Combine(Application.dataPath, "_Game/_Data/DataAssetPlayer.txt");

        WorkWithFile.WriteJSON<DataDynamic>(dataDynamic, path);
    }

    public void LoadDataDynamic()
    {
        string path = Path.Combine(Application.dataPath, "_Game/_Data/DataAssetPlayer.txt");

        if (File.Exists(path) == false)
        {
            WorkWithFile.WriteJSON<DataDynamic>(scriptableDataUI.dataDynamic, path);
        }
        else
        {
            if (WorkWithFile.IsFileEmpty(path))
            {
                WorkWithFile.WriteJSON<DataDynamic>(scriptableDataUI.dataDynamic, path);
            }
        }

        dataDynamic = WorkWithFile.ReadJSON<DataDynamic>(path);
    }

    public void ChangeDataOwnerShipOfWeapon(int index, EnumOwnerShip enumOwnerShipChange)
    {
        dataDynamic.arrWeaponOwners[index].ownerShip = enumOwnerShipChange;
    }

    public void ChangeDataOwnerShipOfMaterial(int indexWeapon, int indexSlot, EnumMaterialsWeaponData enumMaterialToChange, EnumOwnerShip enumOwnerShipChange)
    {
        dataDynamic.arrWeaponOwners[indexWeapon].arrSlots[indexSlot].arrMaterial[GetIndexMaterial(indexWeapon, indexSlot, enumMaterialToChange)].enumOwnerShip = enumOwnerShipChange;
    }

    public void ChangeDataOwnerShipOfMaterial(EnumMaterialsSkinData enumMaterialToChange, EnumOwnerShip enumOwnerShipChange)
    {
        dataDynamic.exteriorOwners.skins[GetIndexMaterial(enumMaterialToChange)].ownerShip = enumOwnerShipChange;
    }

    public void ChangeDataOwnerShipOfMaterial(EnumMaterialsPantData enumMaterialToChange, EnumOwnerShip enumOwnerShipChange)
    {
        dataDynamic.exteriorOwners.pants[GetIndexMaterial(enumMaterialToChange)].ownerShip = enumOwnerShipChange;
    }
    #endregion

    #region Get Data
    public int WeaponAmount => scriptableDataUI.dataDynamic.arrWeaponOwners.Length;

    public int SlotsAmount(int indexWeapon)
    {
        return scriptableDataUI.listShopPageInfo[indexWeapon].slotMater;
    }

    public int EnumBulletsAndWeaponsDataLenght() 
    {
        EnumBulletAndWeaponssData[] enumBulletsDatas = (EnumBulletAndWeaponssData[])Enum.GetValues(typeof(EnumBulletAndWeaponssData));
        return (int)enumBulletsDatas.Length;
    } 

    public int EnumMaterialsWeaponLenght()
    {
        EnumMaterialsWeaponData[] enumMaterialColorDatas = (EnumMaterialsWeaponData[])Enum.GetValues(typeof(EnumMaterialsWeaponData));
        return (int)enumMaterialColorDatas.Length;
    }

    public int EnumMaterialsSkinLenght()
    {
        EnumMaterialsSkinData[] enumMaterialsSkinData = (EnumMaterialsSkinData[])Enum.GetValues(typeof(EnumMaterialsSkinData));
        return (int)enumMaterialsSkinData.Length;
    }

    public int EnumMaterialsPantLenght()
    {
        EnumMaterialsPantData[] enumMaterialsPantData = (EnumMaterialsPantData[])Enum.GetValues(typeof(EnumMaterialsPantData));
        return (int)enumMaterialsPantData.Length;
    }
    #endregion

    #region Get Data Form Data Dynamic
    public bool IsThisWeaponOwnerShip(int index, EnumOwnerShip enumOwnerShip)
    {
        if (dataDynamic.arrWeaponOwners[index].ownerShip == enumOwnerShip)
        {
            return true;
        }

        return false;
    }

    public bool IsThisMaterialOwnerShip(int indexWeapon, int indexSlot, EnumMaterialsWeaponData enumMaterialsColorData, EnumOwnerShip enumOwnerShip)
    {
        if (dataDynamic.arrWeaponOwners[indexWeapon].arrSlots[indexSlot].arrMaterial[GetIndexMaterial(indexWeapon, indexSlot, enumMaterialsColorData)].enumOwnerShip == enumOwnerShip)
        {
            return true;
        }

        return false;
    }

    public bool IsThisMaterialOwnerShip(EnumMaterialsPantData enumMaterialsPantData, EnumOwnerShip enumOwnerShip)
    {
        return dataDynamic.exteriorOwners.pants[(int)enumMaterialsPantData].ownerShip == enumOwnerShip;
    }

    public bool IsThisMaterialOwnerShip(EnumMaterialsSkinData enumMaterialsSkinData, EnumOwnerShip enumOwnerShip)
    {
        return dataDynamic.exteriorOwners.skins[(int)enumMaterialsSkinData].ownerShip == enumOwnerShip;
    }

    public int GetIndexMaterial(int indexWeapon, int indexSlot, EnumMaterialsWeaponData enumMaterialsColorData)
    {
        for (int i = 0; i < MaterialAmount(indexWeapon, indexSlot); i++)
        {
            if (GetEnumMaterialWeaponDynamicData(indexWeapon, indexSlot, i) == enumMaterialsColorData)
            {
                return i;
            }
        }

        Debug.LogError("Can Not Get Index");

        return -1;
    }

    public int GetIndexMaterial(EnumMaterialsSkinData enumMaterialsSkinData)
    {
        for (int i = 0; i < EnumMaterialsSkinLenght(); i++)
        {
            if (GetEnumMaterialSkinDynamicData(i) == enumMaterialsSkinData)
            {
                return i;
            }
        }

        Debug.LogError("Can Not Get Index");

        return -1;
    }

    public int GetIndexMaterial(EnumMaterialsPantData enumMaterialsPantData)
    {
        for (int i = 0; i < EnumMaterialsPantLenght(); i++)
        {
            if (GetEnumMaterialPantDynamicData(i) == enumMaterialsPantData)
            {
                return i;
            }
        }

        Debug.LogError("Can Not Get Index");

        return -1;
    }

    public int MaterialAmount(int indexWeapon, int indexSlot)
    {
        return scriptableDataUI.dataDynamic.arrWeaponOwners[indexWeapon].arrSlots[indexSlot].arrMaterial.Length;
    }

    public EnumBulletAndWeaponssData GetEnumWeapon(int index)
    {
        return (EnumBulletAndWeaponssData)Enum.Parse(typeof(EnumBulletAndWeaponssData), index.ToString());
    }

    public EnumBulletAndWeaponssData GetEnumWeaponEquipped()
    {
        EnumBulletAndWeaponssData weapon;

        for (int i = 0; i < WeaponAmount; i++)
        {
            if (IsThisWeaponOwnerShip(i, EnumOwnerShip.Equipped) == true)
            {
                weapon = (EnumBulletAndWeaponssData)Enum.Parse(typeof(EnumBulletAndWeaponssData), (i + 1).ToString());
                
                return weapon;
            }
        }

        Debug.LogError("Dont have this weapon");

        return EnumBulletAndWeaponssData.none;
    }

    public Material GetMaterialDynamicData(int indexWeapon, int indexSlot, int indexMaterial)
    {
        return GetMaterial(dataDynamic.arrWeaponOwners[indexWeapon].arrSlots[indexSlot].arrMaterial[indexMaterial].enumMaterial);
    }

    public EnumMaterialsWeaponData GetEnumMaterialWeaponDynamicData(int indexWeapon, int indexSlot, int indexMaterial)
    {
        return dataDynamic.arrWeaponOwners[indexWeapon].arrSlots[indexSlot].arrMaterial[indexMaterial].enumMaterial;
    }

    public EnumMaterialsSkinData GetEnumMaterialSkinDynamicData(int indexMaterial)
    {
        return dataDynamic.exteriorOwners.skins[indexMaterial].skinData;
    }

    public EnumMaterialsPantData GetEnumMaterialPantDynamicData(int indexMaterial)
    {
        return dataDynamic.exteriorOwners.pants[indexMaterial].pantData;
    }

    public Material GetMaterialEquippedInThisSlot(int indexWeapon, int indexSlot)
    {
        for (int i = 0; i < dataDynamic.arrWeaponOwners[indexWeapon].arrSlots[indexSlot].arrMaterial.Length; i++)
        {
            if (dataDynamic.arrWeaponOwners[indexWeapon].arrSlots[indexSlot].arrMaterial[i].enumOwnerShip == EnumOwnerShip.Equipped)
            {
                return GetMaterial(dataDynamic.arrWeaponOwners[indexWeapon].arrSlots[indexSlot].arrMaterial[i].enumMaterial);
            }
        }

        Debug.LogError("Dont have material");
        
        return null;
    }

    public Material GetMaterialEquippedInThisSlot(EnumSkinType enumBtnSlotSkinType)
    {
        switch (enumBtnSlotSkinType)
        {
            case EnumSkinType.skinSlot:
                {
                    for (int i = 0; i < dataDynamic.exteriorOwners.skins.Length; i++)
                    {
                        if (dataDynamic.exteriorOwners.skins[i].ownerShip == EnumOwnerShip.Equipped)
                        {
                            return GetMaterial(dataDynamic.exteriorOwners.skins[i].skinData);
                        }
                    }
                    break;
                }
            case EnumSkinType.pantSlot:
                {
                    for (int i = 0; i < dataDynamic.exteriorOwners.pants.Length; i++)
                    {
                        if (dataDynamic.exteriorOwners.pants[i].ownerShip == EnumOwnerShip.Equipped)
                        {
                            return GetMaterial(dataDynamic.exteriorOwners.pants[i].pantData);
                        }
                    }
                    break;
                }
            default:
                {
                    Debug.LogError("EnumBtnSlotSkinType is not define");
                    break;
                }
        }

        Debug.LogError("Dont have material");

        return null;
    }

    public EnumMaterialsWeaponData GetEnumMaterialEquippedInThisSlot(int indexWeapon, int indexSlot)
    {
        for (int i = 0; i < dataDynamic.arrWeaponOwners[indexWeapon].arrSlots[indexSlot].arrMaterial.Length; i++)
        {
            if (dataDynamic.arrWeaponOwners[indexWeapon].arrSlots[indexSlot].arrMaterial[i].enumOwnerShip == EnumOwnerShip.Equipped)
            {
                return dataDynamic.arrWeaponOwners[indexWeapon].arrSlots[indexSlot].arrMaterial[i].enumMaterial;
            }
        }

        Debug.LogError("Dont have material");

        return EnumMaterialsWeaponData.black;
    }

    public Material[] GetMaterialArrayEquippedInThisWeapon(int indexWeapon)
    {
        Material[] materials = new Material[dataDynamic.arrWeaponOwners[indexWeapon].arrSlots.Length];

        for (int i = 0; i < dataDynamic.arrWeaponOwners[indexWeapon].arrSlots.Length; i++)
        {
            materials[i] = GetMaterialEquippedInThisSlot(indexWeapon, i);
        }

        return materials;
    }

    
    #endregion
}
