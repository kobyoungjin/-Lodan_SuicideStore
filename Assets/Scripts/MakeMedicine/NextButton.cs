using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    GameObject harisonScene;
    GameObject makeMedicineScene;
    GameObject nextBtn;
    Button btn;
    DetermineBtn determine;
    Ingredient ingredient;

    void Start()
    {
        harisonScene = GameObject.Find("Harison Canvas").transform.GetChild(0).gameObject;
        makeMedicineScene = GameObject.Find("MakeRoom Canvas").transform.GetChild(0).gameObject;
       
        determine = GameObject.FindObjectOfType<DetermineBtn>().GetComponent<DetermineBtn>();
        btn = GetComponent<Button>();
        
        btn.onClick.AddListener(NextScene);
    }

    // �������� ��ȭ������ �Ѿ���ϴ� ��ư�Լ�
    public void NextScene()
    {
        if(gameObject.name == "RetryButton")  // ���࿡ Ȯ�ι�ư��� Retry��ư�̸� ��Ʈ���� ������Ʈ ��Ȱ��ȭ
        {
            determine.GetRetry(false);
            return;
        }
        nextBtn = GameObject.Find("FinishedPotion").transform.GetChild(2).gameObject;
        ingredient = GameObject.FindObjectOfType<Ingredient>().GetComponent<Ingredient>();

        harisonScene.gameObject.SetActive(true);
        makeMedicineScene.gameObject.SetActive(false);
        determine.SetActivePotion(false);
        ingredient.AllDelete();
    }
}
