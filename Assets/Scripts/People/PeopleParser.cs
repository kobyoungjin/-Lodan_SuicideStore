using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleParser : MonoBehaviour
{
    public PeopleData[] Parse(TextAsset _CSVFileData) // �ļ�
    {
        List<PeopleData> peopleDataList = new List<PeopleData>(); //��� ����Ʈ ����

        string[] data = _CSVFileData.text.Split(new char[] { '\n' });  // ���� ������ ��� ����

        for (int i = 0; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });  // ,���� ��� ����

            PeopleData peopleData = new PeopleData(); // ��� ����Ʈ ����

            peopleData.name = row[0];
            peopleData.explain = row[1];

            peopleDataList.Add(peopleData);
        }
        return peopleDataList.ToArray();   // dialogue ����Ʈ ���� ���·� ��ȯ
    }

    internal string Parse(object _CSVFileData)
    {
        throw new NotImplementedException();
    }
}