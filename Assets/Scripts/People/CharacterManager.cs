using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    GameObject owner;   //������
    GameObject customer;  //�ظ���
    
    List<DialogueData> dialogue = new List<DialogueData>();
    List<Sprite> imageData = new List<Sprite>();
    ShowDialogue showDialogue;

    void Awake()
    {
        customer = GameObject.FindGameObjectWithTag("Customer");

        Color color = customer.GetComponent<Image>().color;
        color.a = 0.0f;
    }

    private void Start()
    {
        showDialogue = GameObject.FindObjectOfType<ShowDialogue>().GetComponent<ShowDialogue>();
        dialogue = showDialogue.GetCurrentDialogue();
        owner = GameObject.FindGameObjectWithTag("������");
    }

    // tag�� ĳ���� UI ����, �� ����
    public void ChangeColor(int i) 
    {
        if (dialogue[i].name == owner.tag)
        {
            owner.GetComponent<Image>().color = new Color(1, 1, 1, 1);  
            customer.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 1);  // �մ� ĳ���� ���ȭ 
        }
        else
        {
            owner.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 1);   // ������ ĳ���� ���ȭ 
            customer.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }

    public void ChooseCharacter(string stateName)
    {
        imageData = GameManager.Instance.GetCharacterData();
        Image customerImage = customer.GetComponent<Image>();
        for (int i = 0; i < imageData.Count; i++)
        {
            if(imageData[i].name == stateName)
            {
                customerImage.sprite = imageData[i];
                Color color = customerImage.color;
                color.a = 1.0f;
                customerImage.SetNativeSize();
                return;
            }
        }

        customerImage.sprite = null;
        Debug.Log("ĳ���� �̹����� �����ϴ�.");
        return;
    }
}
