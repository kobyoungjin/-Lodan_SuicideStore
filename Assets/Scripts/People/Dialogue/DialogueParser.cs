using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    public DialogueData[] Parse(TextAsset _CSVFileData) // �ļ�
    {
        List<DialogueData> dialogueList = new List<DialogueData>(); //��� ����Ʈ ����

        string[] data = _CSVFileData.text.Split(new char[] {'\n'});  // ���� ������ ��� ����
        
        for(int i = 0; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });  // ,���� ��� ����

            DialogueData dialogue = new DialogueData(); // ��� ����Ʈ ����
            
            if (row[0] == "name") continue;

            dialogue.name = row[0]; 
            dialogue.context = row[1];

            dialogueList.Add(dialogue);
        }
        return dialogueList.ToArray();   // dialogue ����Ʈ ���� ���·� ��ȯ
    }

    internal string Parse(object _CSVFileData)
    {
        throw new NotImplementedException();
    }
}
