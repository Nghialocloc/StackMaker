using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas_MainMenu : UICanvas
{
    public override void Open()
    {
        base.Open();
        LevelManager.Ins.status = GameState.MainMenu;
    }


    public void StartGame()
    {
        UIManager.Ins.CloseUI<UICanvas_MainMenu>();
        UIManager.Ins.OpenUI<UICanvas_Gameplay>();
        LevelManager.Ins.OnInit();
    }

    public void ResumeGame()
    {
        LevelManager.Ins.index = PlayerPrefs.GetInt("level", 0);
        UIManager.Ins.CloseUI<UICanvas_MainMenu>();
        UIManager.Ins.OpenUI<UICanvas_Gameplay>();
        LevelManager.Ins.OnInit();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
