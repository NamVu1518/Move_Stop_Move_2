using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnBuyCoin : ButtonBuyBase
{
    public TextMeshProUGUI textFee;
    public UI_ShopPage _shopPage;

    void Start()
    {
        button.onClick.AddListener(BuyWeapon);
    }

    public void SetTextFee(string text)
    {
        textFee.SetText(text);
    } 

    public void BuyWeapon()
    {
        if (DataManager.Ins.DataPlayer.Coin >= int.Parse(DataManager.Ins.ScriptableDataUI.listShopPageInfo[_shopPage.NunWeaponIndex].coin))
        {
            _shopPage.BuyBtn_SetActiveTrueType(EnumOwnerShip.CanUse);
            DataManager.Ins.ChangeDataOwnerShipOfWeapon(_shopPage.NunWeaponIndex, EnumOwnerShip.CanUse);
            DataManager.Ins.DataPlayer.Coin -= int.Parse(DataManager.Ins.ScriptableDataUI.listShopPageInfo[_shopPage.NunWeaponIndex].coin);
            DataManager.Ins.SaveDataPlayer();
        }
    }
}
