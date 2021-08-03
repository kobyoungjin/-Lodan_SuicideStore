using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;

    [SerializeField] Text npcText;
    [SerializeField] Text npcName;
    
    [SerializeField] string csv_FileName;

    List<Dialogue> dialogue = new List<Dialogue>();

    public static bool isFinish = false;  // ������ �������� �Ǻ�

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DialogueParser theParser = GetComponent<DialogueParser>();
            Dialogue[] dialogues = theParser.Parse(csv_FileName);
            for (int i = 0; i < dialogues.Length; i++)
            {
                dialogue.Add(dialogues[i]);  // dialogue����Ʈ�� ���, �̸� ����
            }
            isFinish = true;
        }
    }

    public void ShowText(int i)  // �ؽ�Ʈ UI�� ��� ����
    {
        npcName.text = dialogue[i].name;

        StopAllCoroutines();
        StartCoroutine(TypeNpcText(npcText.text = dialogue[i].context));

        changeColor(i);
    }

    IEnumerator TypeNpcText(string npcText)  // �ѱ��ھ� ������ ����
    {
        this.npcText.text = string.Empty;

        foreach (var letter in npcText)
        {
            this.npcText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public Dialogue[] getDialogue()
    {
        return dialogue.ToArray();
    }

    public void changeColor(int i) // tag�� ��ȭ UI ���� �� ����
    {
        GameObject owner = GameObject.FindGameObjectWithTag("������");
        owner.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        if (!(dialogue[i].name == owner.tag))
        {
            owner.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }

        GameObject farmer = GameObject.FindGameObjectWithTag("�ظ���");
        farmer.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        if (!(dialogue[i].name == farmer.tag))
        {
            farmer.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
    }

}
