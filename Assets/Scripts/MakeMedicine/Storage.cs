using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storage : MonoBehaviour
{
    public Button btn;
    private GameObject mainStorage;
    private GameObject storage;
    private GameObject storageUI;

    void Start()
    {
        mainStorage = GameObject.FindGameObjectWithTag("MainStorage");
        storage = GameObject.Find("Storage");
        storageUI = storage.transform.GetChild(3).gameObject;

        btn = gameObject.GetComponent<Button>();

        btn.onClick.AddListener(ShowShelf);
    }

    private void ShowShelf()
    {
        int currentShelf = btn.gameObject.transform.GetSiblingIndex();

        if (btn.transform.parent.gameObject.CompareTag("StorageUI"))
        {
            int nextShelf = btn.gameObject.transform.GetSiblingIndex();
            Debug.Log("��" + nextShelf);
            storage.transform.GetChild(nextShelf).gameObject.SetActive(false); // ���� ���� ��Ȱ��ȭ
            if (btn.name == "Back(ButtonMesh)")  // ��ư �̸��� back��ư�̸� ����UI�� �ݴ´�.
            {
                InitShelf();
                return;
            }

            if (btn.name == "RightButton")
                storage.transform.GetChild(nextShelf + 1).gameObject.SetActive(true); // ���� Ȱ��ȭ
            else
                storage.transform.GetChild(nextShelf - 1).gameObject.SetActive(true); // ���� Ȱ��ȭ
        }
    }

    // ���� �ʱ�ȭ �Լ�
    private void InitShelf()
    {
        for (int i = 0; i < mainStorage.transform.childCount; i++)
        {
            mainStorage.transform.GetChild(i).gameObject.SetActive(true);  // mainStorage(���� �ִ� â) Ȱ��ȭ 
        }

        for (int i = 0; i < storageUI.transform.childCount; i++)
        {
            storageUI.transform.GetChild(i).gameObject.SetActive(true);
        }

        for (int i = 0; i < storage.transform.childCount; i++)
        {
            storage.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
