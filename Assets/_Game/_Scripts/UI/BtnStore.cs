using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnStore : ButtonBase
{
    void Start()
    {
        button.onClick.AddListener(OnShop);
    }
    
    private void OnShop()
    {
        GameManager.Ins.ShopWeapon();
    }
}
