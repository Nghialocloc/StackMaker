using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvas_Gameplay : UICanvas
{
    [SerializeField] private Text gemText;
    [SerializeField] private Text pointText;
    [SerializeField] private Text levelText;

    public override void Open()
    {
        base.Open();
        LevelManager.Ins.status = GameState.GamePlay;
        levelText.text = "Level " + (LevelManager.Ins.index + 1);
    }

    public void Update()
    {
        gemText.text = LevelManager.Ins.currentPlayer.collectedGem.ToString();
        pointText.text = LevelManager.Ins.currentPlayer.collectedPoint.ToString();
    }

}
