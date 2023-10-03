using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_MainMenu : UICanvas
{
    [SerializeField] private TextMeshProUGUI textCoin;

    private void OnEnable()
    {
        SetTextCoin(DataManager.Ins.DataPlayer.Coin.ToString());
    }

    public void SetTextCoin(string coin) 
    {
        textCoin.SetText(coin);
    }
}
