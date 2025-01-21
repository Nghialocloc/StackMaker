using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    public bool IsDestroyOnClose = false;
    public bool IsHandlingRabbitEars = false;
    public bool IsWidescreenProcessing = false;

    protected RectTransform m_RectTransform;
    private Animator m_Animator;

    private void Start()
    {
        OnInit();
    }

    //khoi tao gia tri canvas
    protected void OnInit()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_Animator = GetComponent<Animator>();

        // xu ly tai tho
        float ratio = (float)Screen.height / (float)Screen.width;
        if (IsHandlingRabbitEars)
        {
            if (ratio > 2.1f)
            {
                Vector2 leftBottom = m_RectTransform.offsetMin;
                Vector2 rightTop = m_RectTransform.offsetMax;
                rightTop.y = -100f;
                m_RectTransform.offsetMax = rightTop;
                leftBottom.y = 0f;
                m_RectTransform.offsetMin = leftBottom;
            }
        }

        if (IsWidescreenProcessing)
        {
            ratio = (float)Screen.width / (float)Screen.height;
            if (ratio < 2.1f)
            {
                //size tieu chuan
                float ratioDefault = 850 / 1920f;
                float ratioThis = ratio;

                float value = 1 - (ratioThis - ratioDefault);

                float with = m_RectTransform.rect.width * value;

                m_RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, with);
            }

        }

    }

    //mo canvas
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    //dong truc tiep, ngay lap tuc
    public virtual void CloseDirectly()
    {
        gameObject.SetActive(false);
        if (IsDestroyOnClose)
        {
            Destroy(gameObject);
        }
        
    }

    //dong canvas sau mot khoang thoi gian delay
    public virtual void Close(float delayTime)
    {
        Debug.Log("Close");
        Invoke(nameof(CloseDirectly), delayTime);
    }

}
