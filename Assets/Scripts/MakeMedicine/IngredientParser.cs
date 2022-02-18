using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientParser : MonoBehaviour
{
    List<string> type = new List<string>();

    public IngredientData[] Parse(TextAsset csvData) // �ļ�
    {
        List<IngredientData> IngredientList = new List<IngredientData>(); //��� ����Ʈ ����

        string[] data = csvData.text.Split(new char[] { '\n' });  // ���� ������ ��� ����

        for (int i = 0; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });  // ,���� ��� ����
            IngredientData ingredientData = new IngredientData(); // ��� ����Ʈ ����

            if (row[0] == "�з�")
                continue;

            type.Add(row[0]);  // type�� ����
            ingredientData.emotion = row[1];  // ���� ����
            ingredientData.name = row[2];  // ����̸� ����
            ingredientData.explain = row[3];  // ��ἳ�� ����

            IngredientList.Add(ingredientData);
        }
        return IngredientList.ToArray();   // ingredient ����Ʈ ���·� ��ȯ
    }

    public List<string> GetEmotionType()
    {
        return type;
    }

    internal string Parse(object storyIngredientData)
    {
        throw new NotImplementedException();
    }
}