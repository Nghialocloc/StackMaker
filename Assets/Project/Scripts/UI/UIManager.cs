using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : Singleton<UIManager>
{
    //list load ui resource
    [SerializeField] private UICanvas[] uiResources;

    //dung de lu thong tin prefab canvas truy cap cho nhanh
    private Dictionary<System.Type, UICanvas> uiCanvasPrefab = new Dictionary<System.Type, UICanvas>();

    //dict luu cac ui dang dung
    private Dictionary<System.Type, UICanvas> uiCanvas = new Dictionary<System.Type, UICanvas>();

    //canvas chua dung cac canvas con
    public Transform CanvasParentTF;

    // Khoi tao cac canvas UI ban dau vao list va goi UI cua main menu
    private void Start()
    {
        uiResources = Resources.LoadAll<UICanvas>("UI/");
        Ins.GetUI<UICanvas_MainMenu>();
    }

    #region Canvas

    //mo UI canvas
    public T OpenUI<T>() where T : UICanvas
    {
        UICanvas canvas = GetUI<T>();

        canvas.Open();

        return canvas as T;
    }

    //dong UI canvas ngay lap tuc
    public void CloseUI<T>() where T : UICanvas
    {
        if (IsOpened<T>())
        {
            GetUI<T>().CloseDirectly();
        }
    }   
    
    //dong ui canvas sau delay time
    public void CloseUI<T>(float delayTime) where T : UICanvas
    {
        if (IsOpened<T>())
        {
            GetUI<T>().Close(delayTime);
        }
    }

    //kiem tra UI dang duoc mo len hay khong
    public bool IsOpened<T>() where T : UICanvas
    {
        return IsLoaded<T>() && uiCanvas[typeof(T)].gameObject.activeInHierarchy;
    }

    //kiem tra UI da duoc khoi tao hay chua
    public bool IsLoaded<T>() where T : UICanvas
    {
        System.Type type = typeof(T);
        return uiCanvas.ContainsKey(type) && uiCanvas[type] != null;
    }

    //lay component cua UI hien tai
    public T GetUI<T>() where T : UICanvas
    {
        if (!IsLoaded<T>())
        {
            UICanvas canvas = Instantiate(GetUIPrefab<T>(), CanvasParentTF);
            uiCanvas[typeof(T)] = canvas;
        }

        return uiCanvas[typeof(T)] as T;
    }

    //Dong tat ca UI
    public void CloseAll()
    {
        foreach (var item in uiCanvas)
        {
            if (item.Value != null && item.Value.gameObject.activeInHierarchy)
            {
                item.Value.CloseDirectly();
            }
        }
    }

    //lay prefab tu Resources/UI 
    private T GetUIPrefab<T>() where T : UICanvas
    {
        if (!uiCanvasPrefab.ContainsKey(typeof(T)))
        {
            if (uiResources == null)
            {
                uiResources = Resources.LoadAll<UICanvas>("UI/");
            }

            for (int i = 0; i < uiResources.Length; i++)
            {
                if (uiResources[i] is T)
                {
                    uiCanvasPrefab[typeof(T)] = uiResources[i];
                    break;
                }
            }
        }

        return uiCanvasPrefab[typeof(T)] as T;
    }

    #endregion

}
