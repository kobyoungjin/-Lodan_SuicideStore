using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;
    [SerializeField] string csv_FileName;  

    List<Dialogue> dialogue = new List<Dialogue>();  //��� ���� ����Ʈ

    public static bool isFinish = false;  // ������ �������� �Ǻ�

    void Awake()
    {
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

    public Dialogue[] GetDialogue() // ��� get�Լ�
    {
        return dialogue.ToArray();  // ����Ʈ�� dialogue[]���·�
    }
}


