using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnQuit : ButtonBase
{

    void Start()
    {
        button.onClick.AddListener(OnQuit);
    }

    private void OnQuit()
    {
        Application.Quit();
    }
}
