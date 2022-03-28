using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kettle : MonoBehaviour
{
    Button btn;
    IngredientSlot ingredientSlot;
    FinishedDrug finishedDrug;
    GameObject bar;
    List<GameObject> barList = new List<GameObject>();  // ��ٱ��Ͽ� ����ִ� ������Ʈ �����ϴ� ����

    int remainIngre = 0;

    private void Start()
    {
        ingredientSlot = GameObject.FindObjectOfType<IngredientSlot>().GetComponent<IngredientSlot>();
        finishedDrug = GameObject.FindObjectOfType<FinishedDrug>().GetComponent<FinishedDrug>();
        bar = ingredientSlot.GetBarObj();

        btn = GetComponent<Button>();
        
        btn.onClick.AddListener(MakingMedicine);
    }

    private void Update()
    {
        Interact();
    }

    void MakingMedicine()
    {
        SceneNumber currentScene = SceneFlowManager.Instance.GetCurrentState();

        for (int i = 0; i < bar.transform.childCount; i++)
        {
            barList.Add(bar.transform.GetChild(i).gameObject);
        }

        List<string> types = new List<string>();
        for (int i = 0; i < barList.Count; i++)
        {
            string name = GameManager.Instance.GetFindIngreToType(barList[i].name);
            types.Add(name);
        }

        List<string> answer = new List<string>();
        answer.AddRange(GameManager.Instance.FindAnswer(currentScene.ToString()));

        int num = Result(types, answer);
        finishedDrug.SetDrugNameing(currentScene.ToString(), num);
    }

    int Result(List<string> types, List<string> answer)
    {
        List<string> playerToBar = types;  // ��ٱ��� ��� �迭
        List<string> temp = answer;


        for (int i = 0; i < playerToBar.Count; i++)
        {
            Debug.Log(playerToBar[i]);
        }
        Debug.Log("----------");
        for (int i = 0; i < temp.Count; i++)
        {
            Debug.Log(temp[i]);
        }

        
        for (int i = 0; i < temp.Count; i++)
        {
            for (int j = 0; j < playerToBar.Count; j++)
            {
                if (temp[i] == playerToBar[j])
                {
                    temp.RemoveAt(i);
                    playerToBar.RemoveAt(j);
                    remainIngre += 1;
                    break;
                }
            }
        }
        

        Debug.Log("------------");

        for (int i = 0; i < temp.Count; i++)
        {
            Debug.Log(temp[i]);
        }
        Debug.Log(remainIngre);

        return remainIngre;
    }

    void Interact()
    {
        int count = ingredientSlot.GetBarChildCount();

        if (count != 5)
            btn.interactable = false;
        else
            btn.interactable = true;
    }
}
