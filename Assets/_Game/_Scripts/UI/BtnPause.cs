using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnPause : ButtonBase
{
    
    void Start()
    {
        button.onClick.AddListener(OnPause);
    }

    private void OnPause()
    {
        UIManager.Ins.CloseUI<UI_GameMenu>();
        UIManager.Ins.OpenUI<UI_PauseMenu>();
        GameManager.Ins.Pause();
    }
}
