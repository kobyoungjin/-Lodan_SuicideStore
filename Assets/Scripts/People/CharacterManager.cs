using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    DialogueDatabase database;

    GameObject owner;   //������
    GameObject farmer;  //�ظ���
    
    List<DialogueData> dialogue = new List<DialogueData>();

    void Start()
    {
        database = GameObject.FindObjectOfType<DialogueDatabase>().GetComponent<DialogueDatabase>();

        DialogueData[] dialogues = database.GetDialogue();
        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogue.Add(dialogues[i]);  //dialogue ����Ʈ�� DatabaseManager���� ������ ��� �߰�
        }

        owner = GameObject.FindGameObjectWithTag("������");
        farmer = GameObject.FindGameObjectWithTag("�ظ���");
    }
    

    // tag�� ĳ���� UI ����, �� ����
    public void ChangeColor(int i) 
    {
        if (dialogue[i].name == owner.tag)
        {
            owner.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            farmer.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);  // �ظ��� ĳ���� ���ȭ 
        }
        else
        {
            owner.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);   // ������ ĳ���� ���ȭ 
            farmer.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }
}
