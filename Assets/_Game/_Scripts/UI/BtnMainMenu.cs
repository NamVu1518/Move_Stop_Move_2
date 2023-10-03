using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnMainMenu : ButtonBase
{
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(OnMainMenu);
    }

    private void OnMainMenu()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<UI_MainMenu>();
        GameManager.Ins.MainMenu();
    }
}
