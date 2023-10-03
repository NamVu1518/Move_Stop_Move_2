using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnArrowLeft : ButtonBase
{
    [SerializeField] private UI_ShopPage _shopPage;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(GoToLeft);
    }
    
    private void GoToLeft()
    {
        _shopPage.NunWeaponIndex--;

        if(_shopPage.NunWeaponIndex < 0)
        {
            _shopPage.NunWeaponIndex = _shopPage.maxIndex;
        }

        _shopPage.ChangeDataWeaponPage(_shopPage.NunWeaponIndex);
    }
}
