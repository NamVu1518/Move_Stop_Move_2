using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum GameState { 
    MainMenu = 0, 
    GamePlay = 1, 
    Finish = 2, 
    Revive = 3, 
    Setting = 4,
    Pause = 5,
}

public class GameManager : Singleton<GameManager>
{
    private static GameState currentGameState;

    [Header("Player")]
    [SerializeField] private Player player;

    public static void ChangeState(GameState state)
    {
        currentGameState = state;
    }

    public static bool IsState(GameState state) => currentGameState == state;

    private void Awake()
    {
        DataManager.Ins.LoadDataPlayer();
        DataManager.Ins.LoadDataDynamic();

        ChangeState(GameState.MainMenu);

        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
    }

    private void Start()
    {
        UIManager.Ins.OpenUI<UI_MainMenu>();
    }

    public void MainMenu()
    {
        ChangeState(GameState.MainMenu);
    }

    public void Play()
    {
        PoolSimple.DespawnAll();
        CharacterManage.Ins.OnInit();
        ChangeState(GameState.GamePlay);
    }

    public void ShopWeapon()
    {
        UIManager.Ins.CloseUI<UI_MainMenu>();
        UIManager.Ins.OpenUI<UI_ShopPage>();
    }

    public void ShopSkin()
    {
        UIManager.Ins.CloseUI<UI_MainMenu>();
        UIManager.Ins.OpenUI<UI_ShopPageSkin>();
    }

    public void NewGame()
    {
        PoolSimple.DespawnAll();
        CharacterManage.Ins.OnInit();
        ChangeState(GameState.GamePlay);
    }

    public void Pause()
    {
        ChangeState(GameState.Pause);
    }

    public void Resume()
    {
        ChangeState(GameState.GamePlay);
    }

    public void Lose()
    {
        DataManager.Ins.DataPlayer.Coin += player.Kill * 50;
        DataManager.Ins.SaveDataPlayer();

        UIManager.Ins.CloseUI<UI_GameMenu>();
        UIManager.Ins.OpenUI<UI_LoseMenu>();
        ChangeState(GameState.Finish);
    }

    public void Win()
    {
        DataManager.Ins.DataPlayer.Coin += player.Kill * 50 * 5;
        DataManager.Ins.SaveDataPlayer();

        Debug.Log(DataManager.Ins.DataPlayer.Coin);

        UIManager.Ins.CloseUI<UI_GameMenu>();
        UIManager.Ins.OpenUI<UI_WinMenu>();
        ChangeState(GameState.Finish);
    }

    private void OnApplicationQuit()
    {
        DataManager.Ins.SaveDataPlayer();
        DataManager.Ins.SaveDataDynamic();
    }
}