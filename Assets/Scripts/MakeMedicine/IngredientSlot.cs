using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientSlot : MonoBehaviour
{
    private GameObject bar;
    private GameObject backgroundBar;
    private GameObject ingrePrefab;
    Kettle kettle;
    public int score;

    GameObject storage;
    List<GameObject> leftStorage = new List<GameObject>();
    List<GameObject> middleStorage = new List<GameObject>();
    List<GameObject> rightStorage = new List<GameObject>();

    private void Start()
    {
        bar = GameObject.Find("ItemBar(Panel)");
        backgroundBar = GameObject.Find("BackGroundItemBar(Panel)");
        ingrePrefab = Resources.Load<GameObject>("MakingRoom/Prefab/Ingredient(Prefab)");  // ������ �̹���
        kettle = GameObject.FindObjectOfType<Kettle>().GetComponent<Kettle>();

        storage = GameObject.Find("Storage");

        for (int i = 0; i < storage.transform.GetChild(0).childCount; i++)
        {
            leftStorage.Add(storage.transform.GetChild(0).GetChild(i).gameObject);
        }

        for (int i = 0; i < storage.transform.GetChild(1).childCount; i++)
        {
            middleStorage.Add(storage.transform.GetChild(1).GetChild(i).gameObject);
        }

        for (int i = 0; i < storage.transform.GetChild(2).childCount; i++)
        {
            rightStorage.Add(storage.transform.GetChild(2).GetChild(i).gameObject);
        }
    }

    //��ٱ��Ͽ� ��� �߰��ϴ� �Լ�
    public void AddingSlotBar(Ingredient image)
    {
        if (bar.transform.childCount < 5)  // ��ٱ��Ͽ� ��ᰡ 5�� �����϶���
        {
            image.ingredientImage.alphaHitTestMinimumThreshold = 0.01f;

            GameObject instance = Instantiate(ingrePrefab);
            instance.name = image.name;
            Image prefabImage = instance.transform.GetChild(0).GetComponent<Image>();
            prefabImage.sprite = image.ingredientImage.sprite;  // Ŭ���� ��� �̹��� ��ȯ

            instance.transform.SetParent(bar.transform);  // ��ٱ��� UI�� ���̰�
           
            return;
        }
    }

    // ��ٱ��Ͽ� ��� �����ϴ� �Լ�
    public void DeleteSlotBar(string name)
    {
        for (int i = 0; i < leftStorage.Count; i++)
        {
            if (leftStorage[i].name == name)
            {
                storage.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
                return;
            }
        }
        for (int i = 0; i < middleStorage.Count; i++)
        {
            if (middleStorage[i].name == name)
            {
                storage.transform.GetChild(1).GetChild(i).gameObject.SetActive(true);
                return;
            }
        }
        for (int i = 0; i < rightStorage.Count; i++)
        {
            if (rightStorage[i].name == name)
            {
                storage.transform.GetChild(2).GetChild(i).gameObject.SetActive(true);
                return;
            }
        }

        Debug.Log("GetItemIndex ����");
        return;
    }

    // ��ٱ��Ͽ� ����ִ� ������ �ľ��ϴ� �Լ�
    public int GetBarChildCount()
    {
        return bar.transform.childCount;
    }
    
    public GameObject GetBarObj()
    {
        return bar;
    }
}
