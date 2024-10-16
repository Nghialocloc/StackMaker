using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas_Soon : UICanvas
{
    public override void Open()
    {
        base.Open();
        LevelManager.Ins.status = GameState.MainMenu;
        LevelManager.Ins.OnReset();
    }

    public void ReturnMain()
    {
        UIManager.Ins.CloseUI<UICanvas_Soon>();
        UIManager.Ins.OpenUI<UICanvas_MainMenu>();
    }


}
