using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnMaterialWeapon : ButtonBase
{
    private UI_ShopPage uI_ShopPage;

    private EnumMaterialsWeaponData enumMaterialsWeapon;
    public EnumMaterialsWeaponData EnumMaterialsWeapon
    {
        set { enumMaterialsWeapon = value; }
        get { return enumMaterialsWeapon; }
    }

    [SerializeField] private GameObject selectSquare;
    public GameObject SelectSquare
    {
        set { if (value != null) { selectSquare.SetActive(value); } }
        get { return selectSquare; } 
    }
    
    [SerializeField] private Image imgMater;
    public Image ImageMater
    {
        set { if (value != null) { imgMater = value;  } }
        get { return imgMater; }
    }



    void Start()
    {
        button.onClick.AddListener(OnChoose);    
    }

    public void OnInit(EnumMaterialsWeaponData enumMaterialsWeaponData, UI_ShopPage shopPage)
    {
        imgMater.color = DataManager.Ins.GetMaterial(enumMaterialsWeaponData).color;
        enumMaterialsWeapon = enumMaterialsWeaponData;
        uI_ShopPage = shopPage;
    } 

    private void OnChoose()
    {
        uI_ShopPage.MaterialBtn_Choose(this);
    }
}
