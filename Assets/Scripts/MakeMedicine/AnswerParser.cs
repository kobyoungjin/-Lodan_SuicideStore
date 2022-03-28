using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerParser : MonoBehaviour
{
    public Answer[] Parse(TextAsset csvData) // �ļ�
    {
        List<Answer> answerList = new List<Answer>(); //��� ����Ʈ ����

        string[] data = csvData.text.Split(new char[] { '\n' });  // ���� ������ ��� ����

        for (int i = 0; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });  // ,���� ��� ����
            Answer answer = new Answer(); // ��� ����Ʈ ����

            if (row[0] == "���丮")
                continue;

            List<string> temp = new List<string>();
            answer.name = row[0];
            for (int j = 0; j < 5; j++)
            {
                temp.Add(row[j + 1]);
            }
            answer.emotion = temp.ToArray();

            answerList.Add(answer);
        }
        return answerList.ToArray();   // array ���·� ��ȯ
    }
}
