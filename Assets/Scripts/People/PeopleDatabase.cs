using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleDatabase : MonoBehaviour
{
    //TextAsset csvFile;
    List<PeopleData> peopleDataList = new List<PeopleData>();  // �ι� ������ ����Ʈ
    PeopleParser peopleParser;

    private void Awake()
    {
        peopleParser = GetComponent<PeopleParser>();
    }

    public void SaveData(TextAsset csvFile)
    {
        PeopleData[] peopledataes = peopleParser.Parse(csvFile);
        for (int i = 0; i < peopledataes.Length; i++)
        {
            peopleDataList.Add(peopledataes[i]);  // dialogue����Ʈ�� ���, �̸� ����
        }
    }

    public List<PeopleData> GetPeopleData() // ��� get�Լ�
    {
        return peopleDataList;  // ����Ʈ�� dialogue[]���·�
    }
}
