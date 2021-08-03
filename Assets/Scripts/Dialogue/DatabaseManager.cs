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
    Image image;
    Player player;

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

    public Dialogue[] getDialogue()  // dialogue ����Ʈ ��ȯ
    {
        return dialogue.ToArray();
    }
}
