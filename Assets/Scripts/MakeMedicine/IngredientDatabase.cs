using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientDatabase : MonoBehaviour
{
    Dictionary<string, int> ingredientDic = new Dictionary<string, int>();
    IngredientParser theParser;

    void Awake()
    {
        theParser = GetComponent<IngredientParser>();
    }

    public void SaveData(TextAsset csvFile)
    {
        IngredientData[] ingredients = theParser.Parse(csvFile);
        for (int i = 0; i < ingredients.Length; i++)
        {
            ingredientDic.Add(ingredients[i].key, ingredients[i].value);  // ��Ǿ�� key��, value�� ����
        }
    }

    // ��Ǿ�� ����� key�� value���� �ִ� �Լ�
    public int GetIngredientData(string name)
    {
        int value = 0; 

        if (ingredientDic.ContainsKey(name))  // ��Ǿ�� �ش��̸��� ������
        {
            value = ingredientDic[name];  // �ش� �̸��� value�� ����
        }
        else
        {
            Debug.Log(name);
            Debug.Log("�߸��� ��� �̸��Դϴ�");  // �´� �̸��� ������ ����� �α׶���
        }
            

        return value;
    }
}
