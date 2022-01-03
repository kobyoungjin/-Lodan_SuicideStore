using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{ 
    public Dialogue[] Parse(string _CSVFileName) // �ļ�
    {
        List<Dialogue> dialogueList = new List<Dialogue>(); //��� ����Ʈ ����
        TextAsset csvData = Resources.Load<TextAsset>("Dialogue/"+_CSVFileName);

        string[] data = csvData.text.Split(new char[] {'\n'});  // ���� ������ ��� ����
        
        for(int i=0; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });  // ,���� ��� ����

            Dialogue dialogue = new Dialogue(); // ��� ����Ʈ ����

            dialogue.name = row[0]; 
            dialogue.context = row[1];

            dialogueList.Add(dialogue);
        }
        return dialogueList.ToArray();   // dialogue ����Ʈ ���� ���·� ��ȯ
    }

    internal string Parse(object Farmer)
    {
        throw new NotImplementedException();
    }
}
