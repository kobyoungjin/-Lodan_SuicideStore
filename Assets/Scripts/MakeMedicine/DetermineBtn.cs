using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetermineBtn : MonoBehaviour
{
    GameObject potion;
    GameObject potionName;
    GameObject instance;
    GameObject Retry;
    IngredientSlot ingredientSlot;
    Button btn;

    private void Start()
    {
        potion = GameObject.Find("MakeRoom").transform.Find("FinishedPotion").gameObject;
        potionName = potion.transform.GetChild(1).gameObject;
        Retry = GameObject.Find("MakeRoom").transform.Find("Retry").gameObject;
        ingredientSlot = GameObject.FindObjectOfType<IngredientSlot>().GetComponent<IngredientSlot>();
        btn = GetComponent<Button>();

        btn.onClick.AddListener(Satisfied);
    }

    // �๰ ������ �Լ�
    public void Satisfied()
    {
        int SatisfiedScore = ingredientSlot.GetScore();

        if (SatisfiedScore == 0)  // ���� score�� 0�̸� ��ٱ��Ͽ� ��ᰡ ���ٰ� ������
        {
            GetRetry(true);  // ��Ʈ���� �Լ� �ҷ���
        }
        else if(SatisfiedScore == 100)  // ���� score�� 100�̸� �Ϻ��� ���� ����.  
        {
            potion.transform.gameObject.SetActive(true);  // �ϼ� ���� Ȱ��ȭ

            GameObject perfectPotion = Resources.Load<GameObject>("SlotPrefabs/�Ϻ��� ����");  // ������ �̹���
            instance = Instantiate(perfectPotion);
            potion.transform.GetChild(0).GetComponent<Image>().sprite = instance.GetComponent<Image>().sprite;

            potionName.GetComponentInChildren<TextMeshProUGUI>().text = instance.GetComponent<Image>().sprite.name;
        }
        else if(SatisfiedScore > 60 || SatisfiedScore < 100)  // ����score�� 60~100���̸� �̹��� ���� ����
        {
            potion.transform.gameObject.SetActive(true);

            GameObject nomalPotion = Resources.Load<GameObject>("SlotPrefabs/�̹��� ����");  // ������ �̹���
            instance = Instantiate(nomalPotion);
            potion.transform.GetChild(0).GetComponent<Image>().sprite = instance.GetComponent<Image>().sprite;

            potionName.GetComponentInChildren<TextMeshProUGUI>().text = instance.GetComponent<Image>().sprite.name;
            
        }
        else  // ���ܴ̿� ������� ���� ����
        {

        }
    }

    // �ϼ��� ������Ʈ �̹��� �߿�� �Լ�
    public void SetActivePotion(bool isAct)  
    {
       potion.transform.gameObject.SetActive(isAct);
    }
    // ��Ʈ���� ������Ʈ ���� �Լ�
    public void GetRetry(bool isAct)
    {
        Retry.gameObject.SetActive(isAct);
    }
}
