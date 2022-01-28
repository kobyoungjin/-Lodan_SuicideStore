using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShelfTigger : MonoBehaviour
{
    private GameObject mainStorage;
    private GameObject backGround;
    private GameObject storage;
    private GameObject storageUI;

    private int storageIndex;

    private void Start()
    {
        mainStorage = GameObject.FindGameObjectWithTag("MainStorage");
        backGround = GameObject.FindGameObjectWithTag("BackGround");
        storage = GameObject.Find("Storage");
        storageUI = storage.transform.GetChild(3).gameObject;
    }
    

    //�ش� storageIndex ���� ���� ���� Ȱ��ȭ
    public void ShowShelf(Shelf shelf)
    {
        //backGround.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);  // ��� �̹��� ������ �����ϵ��� �ణ �׸���ó�� ���󺯰�
        mainStorage.SetActive(false);  // mainStorage(���� �ִ� â) ��Ȱ��ȭ 
        storageUI.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            storageUI.transform.GetChild(i).gameObject.SetActive(true);
        }

        if (shelf.btn.CompareTag("1"))  // ��ư �±װ� 1�̸� storage1 Ȱ��ȭ 
        {
            storage.transform.GetChild(0).gameObject.SetActive(true);
            storageUI.transform.GetChild(1).gameObject.SetActive(false);
            storageIndex = 0;
        }
        else if (shelf.btn.CompareTag("2"))  // ��ư �±װ� 1�̸� storage2 Ȱ��ȭ 
        {
            storage.transform.GetChild(1).gameObject.SetActive(true);
            storageIndex = 1;
        }
        else if (shelf.btn.CompareTag("3"))  // ��ư �±װ� 1�̸� storage3 Ȱ��ȭ 
        {
            storage.transform.GetChild(2).gameObject.SetActive(true);
            storageUI.transform.GetChild(2).gameObject.SetActive(false);
            storageIndex = 2;
        }
    }

    // ��Ḧ �������� ���� UI��Ȱ��ȭ
    public void OnClickCloseShelf()
    {
        //backGround.GetComponent<Image>().color = new Color(1, 1, 1, 1);  // ���� ��� �������� ����
        storageUI.SetActive(false);  // ��ٱ��� UI ��Ȱ��ȭ
        mainStorage.SetActive(true);
        storage.transform.GetChild(storageIndex).gameObject.SetActive(false);  // ���� ���� ���� ��Ȱ��ȭ
    }

    // back, ����, ������ ��ư Ȱ��ȭ �Լ�
    public void StorageUI(Shelf shelf)
    {
        if (shelf.btn.name == "Back(ButtonMesh)")  // ��ư �̸��� back��ư�̸� ����UI�� �ݴ´�. 
            OnClickCloseShelf();
        else
            IsNext(shelf);
    }

    // storageIndex �����Լ�
    public void IsNext(Shelf shelf)
    {
        storage.transform.GetChild(storageIndex).gameObject.SetActive(false); // ���� ���� ��Ȱ��ȭ
        if (shelf.btn.name == "LeftButton")  // ��ư�̸��� LeftBtn��ư�̸� storageIndex ����
            storageIndex--;
        else                                 // ��ư�̸��� RightBtn��ư�̸� storageIndex ����
            storageIndex++;

        NextStorage();
    }

    // ���� �˻��� ���� ���� Ȱ��ȭ�ϴ� �Լ�
    private void NextStorage()
    {
        if (storageIndex > 2)  // storageIndex�� 2���� Ŀ���� �ٽ� 0���� �����.
            storageIndex = 2;
        else if (storageIndex < 0)  // storageIndex�� 0���� �۾����� 2�� �����.
            storageIndex = 0;

        storage.transform.GetChild(storageIndex).gameObject.SetActive(true); // ���� Ȱ��ȭ


        for (int i = 0; i < 3; i++)
        {
            storageUI.transform.GetChild(i).gameObject.SetActive(true);
        }

        if(storageIndex == 0)
            storageUI.transform.GetChild(1).gameObject.SetActive(false);
        else if(storageIndex == 2)
            storageUI.transform.GetChild(2).gameObject.SetActive(false);
    }
}
