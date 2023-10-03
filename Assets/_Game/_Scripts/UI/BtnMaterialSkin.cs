using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnMaterialSkin : ButtonBase
{ 
    private UI_ShopPageSkin uI_ShopPageSkin;
    private EnumMaterialsSkinData enumMaterialsSkinData;
    public EnumMaterialsSkinData EnumMaterialsSkinData
    {
        get { return enumMaterialsSkinData; }
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

    public void OnInit(EnumMaterialsSkinData enumMaterialsSkinData, UI_ShopPageSkin uI_ShopPageSkin)
    {
        this.enumMaterialsSkinData = enumMaterialsSkinData;
        this.uI_ShopPageSkin = uI_ShopPageSkin;
        imgMater.color = DataManager.Ins.GetMaterial(enumMaterialsSkinData).color;
    }

    private void OnChoose()
    {
        uI_ShopPageSkin.MaterialBtn_Choose(this);
    }
}
