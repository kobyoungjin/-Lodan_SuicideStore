using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDatabase : MonoBehaviour
{
    //TextAsset csvFile;
    List<DialogueData> dialogue = new List<DialogueData>();  //��� ���� ����Ʈ
    DialogueParser theParser;

    private void Awake()
    {
        theParser = GetComponent<DialogueParser>();
    }

    public void SaveData(TextAsset csvFile)
    {
        if (dialogue != null) dialogue.Clear();  // dialogue ����Ʈ�� �����Ͱ� ������ ����

        DialogueData[] dialogues = theParser.Parse(csvFile);
        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogue.Add(dialogues[i]);  // dialogue����Ʈ�� ���, �̸� ����
        }
    }

    public DialogueData[] GetDialogue() // ��� get�Լ�
    {
        return dialogue.ToArray();  // ����Ʈ�� dialogue[]���·�
    }
}


