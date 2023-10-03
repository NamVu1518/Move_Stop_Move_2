using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_LoseMenu : UICanvas
{
    [SerializeField] private TextMeshProUGUI _textKillCount;
    [SerializeField] private TextMeshProUGUI _textTopCount;
    [SerializeField] private TextMeshProUGUI _textCoin;
    private Player _player;

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();

        _textKillCount.SetText("Kill: " + _player.Kill.ToString());
        _textCoin.SetText("Coin: +" + _player.Kill * 50);

        string top = "TOP: " + CharacterManage.Ins.numAliveCharater.ToString();
        _textTopCount.SetText(top);
    }
}
