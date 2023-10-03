using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BtnNewGame : ButtonBase
{
    private void Start()
    {
        button.onClick.AddListener(OnNewGame);
    }

    private void OnNewGame()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<UI_GameMenu>();
        GameManager.Ins.NewGame();
    }
}
