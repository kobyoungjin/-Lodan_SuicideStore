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
    
    GameObject mainStorage;
    GameObject storageUI;
    GameObject backGround;

    private void Start()
    {
        mainStorage = GameObject.FindGameObjectWithTag("MainStorage").gameObject;
        storageUI = GameObject.FindGameObjectWithTag("Storage").gameObject;
        backGround = GameObject.FindGameObjectWithTag("BackGround").gameObject;

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
        backGround.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
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
    
    private void NextStorage()
    {
        if (storageIndex > 2)  // storageIndex�� 2�̻��� �Ǹ� �ٽ� 0���� �����.
        {
            storageIndex %= 3;
        }
        else if (storageIndex < 0)  // storageIndex�� 0���� �۾����� �ε����� 2�� �����.
        {
            storageIndex *= -2;
        }

        ShowShelf();
    }

    // ��Ḧ �������� ���� UI��Ȱ��ȭ
    public void OnClickCloseShelf()
    {
        backGround.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        storages[storageIndex].SetActive(false);
        storageUI.SetActive(false);
        mainStorage.SetActive(true);
    }

    //���ʼ��� Ŭ���� storageIndex = 0 ���Է�
    public void IsLeftShelf()  
    {
        storageIndex = 0;
        ShowShelf();
    }

    // ������� Ŭ���� storageIndex = 1 ���Է�
    public void IsMiddleShelf()  
    {
        storageIndex = 1;
        ShowShelf();
    }

    // �����ʼ��� Ŭ���� storageIndex = 2 ���Է�
    public void IsRightShelf()  
    {
        storageIndex = 2;
        ShowShelf();
    }

    // ��������
    public void IsLeft()
    {
        storages[storageIndex].SetActive(false); // ���� ���� ��Ȱ��ȭ
        --storageIndex;
        NextStorage();
    }

    // ����������
    public void IsRight()
    {
        storages[storageIndex].SetActive(false);  // ���� ���� ��Ȱ��ȭ
        ++storageIndex;
        NextStorage();
    }

}
