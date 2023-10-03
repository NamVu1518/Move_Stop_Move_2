using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnResume : ButtonBase
{
    void Start()
    {
        button.onClick.AddListener(OnResume);
    }
    private void OnResume()
    {
        UIManager.Ins.CloseUI<UI_PauseMenu>();
        UIManager.Ins.OpenUI<UI_GameMenu>();
        GameManager.Ins.Resume();
    }
}
