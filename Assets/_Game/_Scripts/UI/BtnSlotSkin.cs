using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSlotSkin : ButtonBase
{
    [SerializeField] private EnumSkinType type;
    [SerializeField] private UI_ShopPageSkin shopPageSkin;
    [SerializeField] private GameObject selectSquare;
    [SerializeField] private Image imgMater;

    public EnumSkinType Type
    {
        get { return type; }
    }

    public GameObject SelectSquare
    {
        set { selectSquare.SetActive(value); }
        get { return selectSquare; }
    }

    void Start()
    {
        button.onClick.AddListener(OnChoose);
    }

    private void OnChoose()
    {
        shopPageSkin.SlotSkinBtn_OnChoose(this);
    }

    public void ChangeMater(Color color)
    {
        imgMater.color = color;
    }

    public void ChangeMater(Sprite sprite)
    {
        imgMater.sprite = sprite;
    }
}
