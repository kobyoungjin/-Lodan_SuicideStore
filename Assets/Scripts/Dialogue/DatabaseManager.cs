using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;
    CharacterManager manager;

    [SerializeField] TextMeshProUGUI npcText;
    [SerializeField] TextMeshProUGUI npcName;
    
    [SerializeField] string csv_FileName;  

    List<Dialogue> dialogue = new List<Dialogue>();  //��� ���� ����Ʈ

    public static bool isFinish = false;  // ������ �������� �Ǻ�

    void Awake()
    {
        manager = GameObject.FindObjectOfType<CharacterManager>().GetComponent<CharacterManager>();

        if (instance == null)  // DatabaseManager�� �ν��Ͻ� ���°� �ƴϸ�
        {
            instance = this;
            DialogueParser theParser = GetComponent<DialogueParser>();
            Dialogue[] dialogues = theParser.Parse(csv_FileName);
            for (int i = 0; i < dialogues.Length; i++)
            {
                dialogue.Add(dialogues[i]);  // dialogue����Ʈ�� ���, �̸� ����
            }
            isFinish = true;
        }
    }

    public void ShowText(int i)  // �ؽ�Ʈ UI�� ��� ����
    {
        if (i > 36) //��縦 ������ ����ϸ�
        {
            End();  
            return;
        }
        npcName.text = dialogue[i].name;
        
        StopAllCoroutines();
        StartCoroutine(TypeNpcText(npcText.text = dialogue[i].context));
        

        manager.ChangeColor(i);  // ĳ���� ������
    }

    IEnumerator TypeNpcText(string npcText)  // �ѱ��ھ� ������ ����
    {
        this.npcText.text = string.Empty;

        foreach (var letter in npcText)
        {
            this.npcText.text += letter;   //�ѱ��ھ� ����
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void End()  // ��������� �����.
    {
        npcText.text = string.Empty;
    }

    public Dialogue[] GetDialogue() // ��� get�Լ�
    {
        return dialogue.ToArray();  // ����Ʈ�� dialogue[]���·�
    }
}


