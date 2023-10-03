using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnMaterialPant : ButtonBase
{
    private UI_ShopPageSkin uI_ShopPageSkin;
    private EnumMaterialsPantData enumMaterialsPantData;
    public EnumMaterialsPantData EnumMaterialsPantData
    {
        get { return enumMaterialsPantData; }
    }

    [SerializeField] private GameObject selectSquare;
    public GameObject SelectSquare
    {
        set { selectSquare.SetActive(value); }
        get { return selectSquare; }
    }

    [SerializeField] private Image imgMater;

    void Start()
    {
        button.onClick.AddListener(OnChoose);
    }

    public void OnInit(EnumMaterialsPantData enumMaterialsPantData, UI_ShopPageSkin uI_ShopPageSkin)
    {
        this.enumMaterialsPantData = enumMaterialsPantData;
        this.uI_ShopPageSkin = uI_ShopPageSkin;
        imgMater.color = DataManager.Ins.GetMaterial(enumMaterialsPantData).color; 
    }

    private void OnChoose()
    {
        uI_ShopPageSkin.MaterialBtn_Choose(this);
    }
}
