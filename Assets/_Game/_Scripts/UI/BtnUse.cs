using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnUse : ButtonBuyBase
{
    [SerializeField] private UI_ShopPage _shopPage;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(UseWeapon);
    }

    public void UseWeapon()
    {
        _shopPage.BuyBtn_SetActiveTrueType(EnumOwnerShip.Equipped);
        
        for (int i = 0; i < DataManager.Ins.DataDynamic.arrWeaponOwners.Length; i++)
        {
            if (DataManager.Ins.DataDynamic.arrWeaponOwners[i].ownerShip == EnumOwnerShip.Equipped)
            {
                DataManager.Ins.ChangeDataOwnerShipOfWeapon(i, EnumOwnerShip.CanUse);
                break;
            }
        }
        
        DataManager.Ins.ChangeDataOwnerShipOfWeapon(_shopPage.NunWeaponIndex, EnumOwnerShip.Equipped);
    }
}
