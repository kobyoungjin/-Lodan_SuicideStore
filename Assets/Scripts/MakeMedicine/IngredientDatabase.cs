using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientDatabase : MonoBehaviour
{
    private static IngredientDatabase instance;

    [SerializeField] string storyIngredientData;  // ���⼭ ��� ������ �����ؾ��ҵ�?
    Dictionary<string, int> ingredientDic = new Dictionary<string, int>();

    public static bool isFinish = false;  // ������ �������� �Ǻ�

    void Awake()
    {
        if (instance == null)  // IngredientDatabase �ν��Ͻ� ���°� �ƴϸ�
        {
            instance = this;
            IngredientParsor theParser = GetComponent<IngredientParsor>();
            IngredientData[] ingredients = theParser.Parse(storyIngredientData);
            for (int i = 0; i < ingredients.Length; i++)
            {
                ingredientDic.Add(ingredients[i].key, ingredients[i].value);  // ��Ǿ�� key��, value�� ����
            }
            isFinish = true;
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
            Debug.Log("�߸��� ��� �̸��Դϴ�");  // �´� �̸��� ������ ����� �α׶���

        return value;
    }
}
