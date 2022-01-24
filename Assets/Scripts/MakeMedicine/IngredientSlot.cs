using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientSlot : MonoBehaviour
{
    private GameObject bar;
    private GameObject backgroundBar;
    private GameObject ingrePrefab;
    private IngredientDatabase data;
    public int score;

    private void Start()
    {
        bar = GameObject.Find("ItemBar(Panel)");
        backgroundBar = GameObject.Find("BackGroundItemBar(Panel)");
        ingrePrefab = Resources.Load<GameObject>("SlotPrefabs/Ingredient(Prefab)");  // ������ �̹���
        data = GameObject.FindObjectOfType<IngredientDatabase>().GetComponent<IngredientDatabase>();
    }  

    //��ٱ��Ͽ� ��� �߰��ϴ� �Լ�
    public void AddingSlotBar(Ingredient image)
    {
        if (bar.transform.childCount < 5)  // ��ٱ��Ͽ� ��ᰡ 5�� �����϶���
        {
            image.ingredientImage.alphaHitTestMinimumThreshold = 0.01f;

            GameObject instance = Instantiate(ingrePrefab);
            Image prefabImage = instance.transform.GetChild(0).GetComponent<Image>();
            prefabImage.sprite = image.ingredientImage.sprite;  // Ŭ���� ��� �̹��� ��ȯ


            instance.transform.SetParent(bar.transform);  // ��ٱ��� UI�� ���̰�

            //int value = data.GetIngredientData(image.ingredientImage.gameObject.name.ToString());  // ��ٱ��Ͽ� �߰��Ǹ� �ش� ������ ��������.
            //score += value;
        }
    }
    

    // ���� ���� ��ȯ �Լ�
    public int GetScore()
    {
        return score;
    }
}
