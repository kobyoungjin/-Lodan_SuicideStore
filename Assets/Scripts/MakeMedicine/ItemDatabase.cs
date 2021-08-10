using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;

    GameObject clickObject;
    private int slotCnt = 0;
    public Image[] itemSlotsUI;
    public Transform slotHolder;
    Image image;

    private void Awake()
    {
        instance = this;
        itemSlotsUI = slotHolder.GetComponentsInChildren<Image>();
    }

    List<Item> itemDB = new List<Item>();

    // ��ư Ŭ���� Ŭ���� ������Ʈ clickObject ��������
    public void ClickBtn()
    {
        clickObject = EventSystem.current.currentSelectedGameObject;
        ShowItemListUI();
    }

    //��ٱ��Ͽ� ������ ��Ḧ �����ִ� �Լ�
    private void ShowItemListUI()
    {
        if (slotCnt >= itemSlotsUI.Length) // ������ itemSlotsUI�� ���̸� ������ return
        {
            slotCnt = itemSlotsUI.Length;
            return;
        }
        
        itemSlotsUI[slotCnt].GetComponent<Image>().sprite 
                            = clickObject.GetComponent<Image>().sprite;  // Ŭ���� ��� ��ٱ��Ͽ� UI ��� 
        itemSlotsUI[slotCnt] = clickObject.GetComponent<Image>();   //Ŭ���� ��� ���� �Է�
        slotCnt++;
    }

    // x��ư Ŭ���� �ش� ���UI���� �� ��ĭ�� �δ�.
    public void DeleteItemList()
    {
        if (!(slotCnt < 0))
        {
           

            for (int i = slotCnt; i < itemSlotsUI.Length; i++)
            {
                itemSlotsUI[slotCnt] = itemSlotsUI[slotCnt + 1];
            }
        }
    }

    // ClickBtn���� clickObject �����Ѱ� ����Ʈ�� ����
    void AddListItem()
    {
        Item item = new Item();
        item.itemName = clickObject.name;
        item.itemImage = clickObject.GetComponent<Image>();

        itemDB.Add(item);
    }

    
}
