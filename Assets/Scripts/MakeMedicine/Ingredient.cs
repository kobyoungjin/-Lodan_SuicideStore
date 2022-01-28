using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour
{
    private IngredientSlot slotBar;
    private Button btn;
    private IngredientDatabase data;
    public Image ingredientImage;

    void Start()
    {
        slotBar = FindObjectOfType<IngredientSlot>().transform.GetComponentInParent<IngredientSlot>();
        data = GameObject.FindObjectOfType<IngredientDatabase>().GetComponent<IngredientDatabase>();

        ingredientImage = GetComponent<Image>();
        btn = GetComponent<Button>();
      
        if (btn == null)  // ��ư�� ������ ������Ʈ�� �ڽĿ��� ��ư ���۳�Ʈ�� ã�´�.
            btn = gameObject.transform.parent.transform.GetChild(1).GetComponent<Button>();

        if(btn.gameObject.CompareTag("delete"))  // ��ưtag�� delete�� ��ٱ��Ͽ� ����� ������Ʈ�� �����ϴ� �Լ��� �ҷ��´�.
            btn.onClick.AddListener(DeleteIngredient);
        else  // ��ٱ��Ͽ� ��Ḧ ���ϴ� �Լ��� �ҷ��´�.
            btn.onClick.AddListener(AddIngredient);
    }

    // ��ٱ��Ͽ� ��Ḧ �߰��ϴ� �Լ�
    public void AddIngredient()
    {
        slotBar.AddingSlotBar(this);
    }

    // ��ٱ��Ͽ� ��Ḧ �����ϴ� �Լ�
    public void DeleteIngredient()
    {
        //int value = data.GetIngredientData(gameObject.GetComponent<Image>().sprite.name);
        //slotBar.score -= value;

        Destroy(this.transform.parent.gameObject);
    }
}