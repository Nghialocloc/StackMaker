using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvas_Finish : UICanvas
{
    [SerializeField] private Text pointText;

    public override void Open()
    {
        base.Open();
        LevelManager.Ins.status = GameState.Finish;
        pointText.text = "Result: " + LevelManager.Ins.currentPlayer.collectedPoint;
    }

    // Khoi tao va chuyen sang level tiep theo
    // Neu het level roi thi chuyen sang man hinh doi
    public void NextLevel()
    {
        if (LevelManager.Ins.index >= LevelManager.Ins.levels.Length)
        {
            UIManager.Ins.CloseUI<UICanvas_Finish>();
            UIManager.Ins.OpenUI<UICanvas_Soon>();
            return;
        }
        LevelManager.Ins.OnInit();
        UIManager.Ins.CloseUI<UICanvas_Finish>();
        UIManager.Ins.OpenUI<UICanvas_Gameplay>();
    }
}
