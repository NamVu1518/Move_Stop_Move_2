using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BtnPlay : ButtonBase
{
    private void Start()
    {
        button.onClick.AddListener(OnPlay);
    }

    public void OnPlay()
    {
        UIManager.Ins.CloseUI<UI_MainMenu>();
        UIManager.Ins.OpenUI<UI_GameMenu>();
        GameManager.Ins.Play();
    }
}
