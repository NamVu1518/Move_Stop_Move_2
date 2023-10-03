using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSlot : ButtonBase
{
    [SerializeField] private int index;
    [SerializeField] private UI_ShopPage shopPage;
    [SerializeField] private GameObject selectSquare;
    [SerializeField] private Image imgMater;

    public int Index
    {
        get { return index; }
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
        shopPage.SlotBtn_OnChoose(this);
    }

    public void ChangeMater(Color color)
    {
        imgMater.color = color;
    }
}
