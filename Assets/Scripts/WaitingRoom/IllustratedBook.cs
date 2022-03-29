using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class IllustratedBook : MonoBehaviour
{
    Button btn;
    GameObject detailBook;
    Transform canvas;
    GameObject people1;
    GameObject people2;
    List<Sprite> characterImageList;
    List<string> addFrameCharacter;

    void Start()
    {
        btn = GetComponent<Button>();
        characterImageList = GameManager.Instance.GetCharacterData();
        addFrameCharacter = GameManager.Instance.GetAddFrameCharacter();
        detailBook = GameObject.Find("IllustratedBook(Canvas)").transform.GetChild(3).gameObject;
        canvas = GameObject.Find("IllustratedBook(Canvas)").transform;
        people1 = canvas.GetChild(1).gameObject;
        people2 = canvas.GetChild(2).gameObject;

        for (int i = 0; i < addFrameCharacter.Count; i++)
        {
            AddIllustrateCharacter(addFrameCharacter[i]);
        }

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
        people2.SetActive(false);
        people1.SetActive(true);
    }

    void Right()
    {
        people2.SetActive(true);
        people1.SetActive(false);
    }

    // Ŭ���� �����͸� ã�� �Լ�
    void FindData()
    {
        detailBook.SetActive(true);
        SetBackInteract(false);
        SetArrowInteract(false);

        Image image = detailBook.transform.Find("Character(Image)").GetComponent<Image>();
        TextMeshProUGUI name = detailBook.transform.Find("NameText(TMP)").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI explain = detailBook.transform.Find("ExplainText(TMP)").GetComponent<TextMeshProUGUI>();

        // ���� ������Ʈ �̹��� ��������Ʈ �̸��� empty�̹����� �������� �ҷ�����
        if (GetComponent<Image>().sprite.name == gameObject.name + "empty")
        {
            for (int i = 0; i < characterImageList.Count; i++)
            {
                if (characterImageList[i].name == this.gameObject.name)  // ĳ���� �̹��� ����Ʈ���� parentName�� ������
                {
                    image.sprite = characterImageList[i];
                    name.text = "";
                    explain.text = "";
                    image.color = new Color(0, 0, 0, 255);
                    detailBook.transform.GetChild(4).gameObject.SetActive(false);

                    return;
                }
            }
            Debug.Log("�ش� ��������Ʈ�� �����ϴ�");
            return;
        }
        else
        {
            for (int i = 0; i < characterImageList.Count; i++)
            {
                if (characterImageList[i].name == this.gameObject.name)  // ĳ���� �̹��� ����Ʈ���� parentName�� ������
                {
                    image.sprite = characterImageList[i];
                    name.text = characterImageList[i].name;
                    explain.text = GameManager.Instance.FindPeopleText(characterImageList[i].name, "explain");

                    image.color = new Color(255, 255, 255, 255);

                    detailBook.transform.GetChild(4).gameObject.SetActive(true);
                    return;
                }
            }
            Debug.Log("�ش� ��������Ʈ�� �����ϴ�");
            return;
        }
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
        Button arrow = transform.parent.GetChild(6).GetComponent<Button>();
        arrow.interactable = isActive;
    }

    // �̾߱Ⱑ �������� ���ڿ� �ι��� �߰��ϴ� �Լ�
    public void AddIllustrateCharacter(string characterName)
    {
        List<Sprite> characterImageList = GameManager.Instance.GetFrameCharacterData();
        Transform canvas = GameObject.Find("IllustratedBook(Canvas)").transform;
        GameObject people1 = canvas.GetChild(1).gameObject;
        GameObject people2 = canvas.GetChild(2).gameObject;

        if (characterName == null || characterName == "") return;

        Sprite illustImage = null;
        
        for (int i = 0; i < characterImageList.Count; i++)
        {
            if (characterImageList[i].name == characterName)
            {
                illustImage = characterImageList[i];
                break;
            }
        }

        for (int i = 0; i < people1.transform.childCount; i++)
        {
            if (people1.transform.GetChild(i).gameObject.name == characterName)
            {
                people1.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = illustImage;
                return;
            }
        }

        for (int i = 0; i < people2.transform.childCount; i++)
        {
            if (people2.transform.GetChild(i).gameObject.name == characterName)
            {
                people2.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = illustImage;
                return;
            }
        }

        Debug.Log("AddIllustrateCharacter�Լ��� characterName�� ã���� ����");
        return;
    }
}
