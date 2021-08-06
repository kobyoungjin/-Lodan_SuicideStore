using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickTigger : MonoBehaviour
{
    private int storageIndex;
    public GameObject[] storages;
    private int clickNum = 1;
    private bool rightBtn;
    private bool isBack;
    
    GameObject mainStorage;
    GameObject storageUI;

    private void Start()
    {
        mainStorage = GameObject.FindGameObjectWithTag("MainStorage").gameObject;
        storageUI = GameObject.FindGameObjectWithTag("Storage").gameObject;

        storageUI.SetActive(false);  // ����� ��ưUI ��Ȱ��ȭ
    }
    
    public void Trigger()  // Ŭ���Ҷ����� ��� ����
    { 
        var data = FindObjectOfType<DatabaseManager>();  
        data.ShowText(clickNum);
       
        clickNum++;
    }

    //�ش� storageIndex ���� ���� ���� Ȱ��ȭ
    private void ShowShelf()
    {
        mainStorage.SetActive(false);  // mainStorage(���� �ִ� â) ��Ȱ��ȭ 
        storageUI.SetActive(true);   // storageIndex�� 0�̸� ���� UI Ȱ��ȭ
        switch (storageIndex)
        {
            case 0:
                storages[0].SetActive(true);  //storageIndex�� 1�̸� 1��° ���� Ȱ��ȭ
                break;
            case 1:
                storages[1].SetActive(true);  // storageIndex�� 2�̸� 2��° ���� Ȱ��ȭ
                break;
            case 2:
                storages[2].SetActive(true);  // �� ���̸� 3��° ���� Ȱ��ȭ
                break;
        }
    }
    
    // ��Ḧ �������� ���� UI��Ȱ��ȭ
    public void OnClickCloseShelf() 
    {
        storages[storageIndex].SetActive(false);        
        storageUI.SetActive(false);
        mainStorage.SetActive(true);
    }

    private void NextStorage()
    {
       if(storageIndex > 3)

        if (rightBtn)
        {
            storages[storageIndex - 1].SetActive(false);
            storages[storageIndex].SetActive(true);
        }
        else
        { 
            storages[storageIndex + 1].SetActive(false);
            storages[storageIndex].SetActive(true);
        }
    }

    public void IsLeftShelf()  // ���ʼ��� Ŭ���� storageIndex = 0 ���Է�
    {
        storageIndex = 0;
        ShowShelf();
    }
    public void IsMiddleShelf()  // ������� Ŭ���� storageIndex = 1 ���Է�
    {
        storageIndex = 1;
        ShowShelf();
    }
    public void IsRightShelf()  // �����ʼ��� Ŭ���� storageIndex = 2 ���Է�
    {
        storageIndex = 2;
        ShowShelf();
    }
    public void IsLeft()
    {
        storageIndex--;
        NextStorage();
    }
    public void IsRight()
    {
        storageIndex++;
        NextStorage();
    }

}
