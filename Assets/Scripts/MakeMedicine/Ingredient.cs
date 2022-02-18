using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour
{
    private IngredientSlot slotBar;
    private Button btn;
    public Image ingredientImage;
    IngreExplainBar explainBar;

    void Start()
    {
        slotBar = FindObjectOfType<IngredientSlot>().transform.GetComponentInParent<IngredientSlot>();
        explainBar = FindObjectOfType<IngreExplainBar>().transform.GetComponentInParent<IngreExplainBar>();
        ingredientImage = GetComponent<Image>();
        btn = GetComponent<Button>();
      
        if (btn == null)  // ��ư�� ������ ������Ʈ�� �ڽĿ��� ��ư ���۳�Ʈ�� ã�´�.
            btn = gameObject.transform.GetComponentInChildren<Button>();

        if(btn.gameObject.CompareTag("delete"))  // ��ưtag�� delete�� ��ٱ��Ͽ� ����� ������Ʈ�� �����ϴ� �Լ��� �ҷ��´�.
            btn.onClick.AddListener(DeleteIngredient);
        else  // ��ٱ��Ͽ� ��Ḧ ���ϴ� �Լ��� �ҷ��´�.
            btn.onClick.AddListener(AddIngredient);
    }

    // ��ٱ��Ͽ� ��Ḧ �߰��ϴ� �Լ�
    public void AddIngredient()
    {
        if (slotBar.GetBarChildCount() == 5)
            return;

        slotBar.AddingSlotBar(this);
        gameObject.SetActive(false);
    }

    // ��ٱ��Ͽ� ��Ḧ �����ϴ� �Լ�
    public void DeleteIngredient()
    {
        if (slotBar.GetBarChildCount() < 0)
            return;

        GameObject deleteItem = this.transform.parent.gameObject;
        slotBar.DeleteSlotBar(deleteItem.name);
        Destroy(deleteItem);
    }
}