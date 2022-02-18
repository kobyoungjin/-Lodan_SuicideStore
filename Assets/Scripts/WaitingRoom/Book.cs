using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    Button btn;
    GameObject ingredientBook;
    Button illustratedBook;
    private void Start()
    {
        ingredientBook = GameObject.Find("WaitingRoomCanvas");
        illustratedBook = GameObject.Find("IllustratedBook(Button)").GetComponent<Button>();
        btn = GetComponent<Button>();

        if (btn.name == "IngredientBook(Button)")  // ingredient���� �̸��̸�
            btn.onClick.AddListener(Active);
        else if (btn.name == "Play(Button)")  // ���� play��ư ������ dialogue������
            btn.onClick.AddListener(() => GameManager.Instance.LoadNextScene("DialogueScene", 1.0f));
        else  // 
            btn.onClick.AddListener(() => GameManager.Instance.LoadNextScene("IllustratedBook", 1.0f));
    }

    // ingredient���� ���� �Լ�
    void Active()
    {
        ingredientBook.transform.GetChild(3).gameObject.SetActive(true);
        SetInteractable(false);
    }

    // �ι� ���� ��ư Ȱ��ȭ ��ȯ �Լ� 
    public void SetInteractable(bool interact)
    {
        illustratedBook.interactable = interact;
    }
}
