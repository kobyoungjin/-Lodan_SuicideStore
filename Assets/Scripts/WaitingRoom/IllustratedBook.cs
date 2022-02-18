using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IllustratedBook : MonoBehaviour
{
    Button btn;
    GameObject detailBook;
    Transform canvas;

    void Start()
    {
        btn = GetComponent<Button>();
        detailBook = GameObject.Find("IllustratedBook(Canvas)").transform.GetChild(3).gameObject;
        canvas = GameObject.Find("IllustratedBook(Canvas)").transform;

        if (btn.name == "Left(Button)")  // ���� ȭ��ǥ
        {
            btn.onClick.AddListener(Left);
        }
        else if(btn.name == "Right(Button)")  // ������ ȭ��ǥ
        {
            btn.onClick.AddListener(Right);
        }
        else  // �ƴϸ� Ŭ���� ������ ������ ã��
            btn.onClick.AddListener(FindData);
    }
    
    void Left()
    {
        canvas.GetChild(2).gameObject.SetActive(false);
        canvas.GetChild(1).gameObject.SetActive(true);
    }

    void Right()
    {
        canvas.GetChild(2).gameObject.SetActive(true);
        canvas.GetChild(1).gameObject.SetActive(false);
    }

    // Ŭ���� �����͸� ã�� �Լ�
    void FindData()
    {
        string parentName = transform.parent.name;
        Debug.Log(parentName);
        List<Sprite> characterImageList = GameManager.Instance.GetCharacterData();

        detailBook.SetActive(true);
        SetBackInteract(false);
        SetArrowInteract(false);

        for (int i = 0; i < characterImageList.Count; i++)
        {
            if(characterImageList[i].name == parentName)  // ĳ���� �̹��� ����Ʈ���� parentName�� ������
            {
                Image image = detailBook.transform.Find("Character(Image)").GetComponent<Image>();
                image.sprite = characterImageList[i];

                TextMeshProUGUI name = detailBook.transform.Find("NameText(TMP)").GetComponent<TextMeshProUGUI>();
                name.text = characterImageList[i].name;

                //TextMeshProUGUI explain = detailBook.transform.Find("ExplainText(TMP)").GetComponent<TextMeshProUGUI>();
                //explain.text = characterImageList[i].name;
                return;
            }
        }
        Debug.Log("�ش� ��������Ʈ�� �����ϴ�");
    }

    // Back(Button)�� bool ��ȯ �Լ�
    public void SetBackInteract(bool isActive)
    {
        Button back = canvas.GetChild(4).GetComponent<Button>();
        back.interactable = isActive;
    }

    // ȭ��ǥ bool ��ȯ �Լ�
    public void SetArrowInteract(bool isActive)
    {
        Button arrow = transform.parent.transform.parent.GetChild(6).GetComponent<Button>();
        arrow.interactable = isActive;
    }
}
