using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Letter: MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject AfterImage;

    GameObject canvas;
    DragDrop dd;
    RectTransform rt;
    
    bool isEnter = false;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        dd = GameObject.FindObjectOfType<DragDrop>().GetComponent<DragDrop>(); //Hierachy -> DragDrop ��ũ��Ʈ�� ������ �ִ� ������Ʈ�� ã�´�
        rt = canvas.transform.GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
    }

    //������ �ȿ� ���ڰ� ������ ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        isEnter = true;
    }

    //������ �ȿ� ���ڰ� ������ ��
    public void OnPointerExit(PointerEventData eventData)
    {
        isEnter = false;
    }

    public bool GetEnter()
    {
        return isEnter;
    }
}
