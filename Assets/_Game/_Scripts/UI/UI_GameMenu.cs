using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_GameMenu : UICanvas
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private void Update()
    {
        _textMeshPro.SetText("Alive: " + CharacterManage.Ins.numAliveCharater.ToString());   
    }
}
