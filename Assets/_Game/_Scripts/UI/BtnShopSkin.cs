using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnShopSkin : ButtonBase
{
    void Start()
    {
        button.onClick.AddListener(OnChoose);    
    }

    private void OnChoose()
    {
        GameManager.Ins.ShopSkin();
    }
}
