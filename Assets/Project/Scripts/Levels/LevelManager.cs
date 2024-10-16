using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [Header("Levels")]
    public Level[] levels;
    public Level currentLevel;
    public GameState status;
    public int index;

    [Header("Player")]
    [SerializeField] private Player player;
    public Player currentPlayer;

    [Header("Camera")]
    [SerializeField] private CameraFollow playerCamera;

    public void Start()
    {
        levels = Resources.LoadAll<Level>("Levels/");
        index = 0;
    }

    #region State

    //Khoi tao cac thong so bat dau game
    public void OnInit()
    {
        OnLoadLevel(index);
        if (currentPlayer != null)
        {
            Destroy(currentPlayer.gameObject);
        }
        currentPlayer = Instantiate(player);
        playerCamera.player = currentPlayer.transform;
        currentPlayer.transform.position = currentLevel.startPos.position;
    }

    public void OnReset()
    {
        index = 0;
        PlayerPrefs.SetInt("level", levels.Length-1);
        if (currentPlayer != null)
        {
            Destroy(currentPlayer.gameObject);
        }

        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

    }

    public void OnFinish()
    {
        index++;
        // Dam bao playpref khong bi luu vuot qua so luong man choi
        if(index < (levels.Length - 1))
        {
            PlayerPrefs.SetInt("level", index);
        }
        UIManager.Ins.CloseUI<UICanvas_Gameplay>();
        UIManager.Ins.OpenUI<UICanvas_Finish>();
    }

    //tao prefab level moi
    public void OnLoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levels[level]);
    }

    #endregion

}
