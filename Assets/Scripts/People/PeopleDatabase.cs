using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleDatabase : MonoBehaviour
{
    //TextAsset csvFile;
    List<PeopleData> peopleDataList = new List<PeopleData>();  // �ι� ������ ����Ʈ
    PeopleParser theParser;

    private void Awake()
    {
        theParser = GetComponent<PeopleParser>();
    }

    public void SaveData(TextAsset csvFile)
    {
        if (peopleDataList != null) peopleDataList.Clear();  // dialogue ����Ʈ�� �����Ͱ� ������ ����

        PeopleData[] peopledataes = theParser.Parse(csvFile);
        for (int i = 0; i < peopledataes.Length; i++)
        {
            peopleDataList.Add(peopledataes[i]);  // dialogue����Ʈ�� ���, �̸� ����
        }
    }

    public PeopleData[] GetPeopleData() // ��� get�Լ�
    {
        return peopleDataList.ToArray();  // ����Ʈ�� dialogue[]���·�
    }
}
