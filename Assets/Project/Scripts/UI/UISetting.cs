using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetting : UICanvas
{
    public override void Open()
    {
        base.Open();
        LevelManager.Ins.status = GameState.Setting;
    }

    public void ReturnfromSetting()
    {
        UIManager.Ins.CloseUI<UISetting>();
        UIManager.Ins.OpenUI<UICanvas_Gameplay>();
    }

    public void ReturnToMenu()
    {
        LevelManager.Ins.OnReset();
        UIManager.Ins.CloseUI<UISetting>();
        UIManager.Ins.OpenUI<UICanvas_MainMenu>();
    }
}
