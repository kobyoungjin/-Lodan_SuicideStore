using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngredientBook : MonoBehaviour
{
    Button btn1;
    Button btn2;
    int page = 1;

    List<string> type;
    List<IngredientData> ingreAllData;
    Sprite[] ingreSprites;

    private void Start()
    {
        btn1 = transform.GetChild(1).GetComponent<Button>();
        btn2 = transform.GetChild(2).GetComponent<Button>();

        type = GameManager.Instance.GetTypeList();
        ingreAllData = GameManager.Instance.GetIngreAllData();
        ingreSprites = Resources.LoadAll<Sprite>("MakingRoom/Material");

        Page(page);
        ChangeData(page);
            

        btn1.onClick.AddListener(Left);
        btn2.onClick.AddListener(Right);
    }
    
    // ingredientBook ȭ��ǥ ����
    void Left()
    {
        page -= 1;

        if (page < 1) page = 1;
        
        Page(page);
        ChangeData(page);
    }

    // ingredientBook ȭ��ǥ ������
    void Right()
    {
        page += 1;

        if (page > 5) page = 5;
        
        
        Page(page);
        ChangeData(page);
    }

    // ȭ��ǥ Ŭ���� ���� �����ͷ� ��ȯ
    void ChangeData(int page)
    {
        Transform range;
        int start = 4 * (page - 1);    
        for (int i = start; i < start + 4; i++)
        {
            range = transform.GetChild(0).transform.GetChild(i - start).transform;

            Sprite sprite = FindIngreSprite(ingreAllData[i].name);  
            range.GetChild(1).GetComponent<Image>().sprite = sprite;  // �̹��� ����
            range.GetChild(2).transform.GetComponentInChildren<TextMeshProUGUI>().text = ingreAllData[i].name;  // �̸� ����
            range.GetChild(3).GetComponent<TextMeshProUGUI>().text = type[i];  // ���� ��з� ����
            range.GetChild(4).GetComponent<TextMeshProUGUI>().text = ingreAllData[i].emotion;  // ���� ���� ����
            range.GetChild(5).GetComponent<TextMeshProUGUI>().text = ingreAllData[i].explain;  // ingredient ���� ����
        }
    }

    // ������ ã�� �Լ�
    void Page(int page)
    {
        TextMeshProUGUI bookPage = GameObject.Find("IngredientBook(Image)").transform.GetChild(3).GetComponent<TextMeshProUGUI>();

        if (btn1.gameObject.activeSelf == false) btn1.gameObject.SetActive(true);  // ������ ȭ��ǥ�� ���������� �ѱ�
        if (btn2.gameObject.activeSelf == false) btn2.gameObject.SetActive(true);  // ���� ȭ��ǥ�� ���������� �ѱ�

        if (page == 1) btn1.gameObject.SetActive(false);
        if (page == 5) btn2.gameObject.SetActive(false);

        bookPage.text = page.ToString();
    }

    // ingredient ��������Ʈ ã�� �Լ�
    Sprite FindIngreSprite(string name)
    {
        for (int i = 0; i < ingreSprites.Length; i++)
        {
            if(ingreSprites[i].name == name)
            {
                return ingreSprites[i];
            }
        }

        Debug.Log("Sprite null���� ��ȯ�մϴ�.");
        return null;
    }
}
