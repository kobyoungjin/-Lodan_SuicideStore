using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ShowDialogue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI npcText;
    [SerializeField] TextMeshProUGUI npcName;
    private CharacterManager manager;
    private DialogueDatabase database;
    private Button btn;

    private int clickNum;  // ���° ������� �������� 
    private bool isRead;  // ��簡 �̹� ��Ÿ���� �ִ��� �Ǻ��ϴ� ����(true�� �ѱ��ھ� ������)

    List<DialogueData> dialogue = new List<DialogueData>();

    private void Start()
    {
        manager = GameObject.FindObjectOfType<CharacterManager>().GetComponent<CharacterManager>();
        database = GameObject.FindObjectOfType<DialogueDatabase>().GetComponent<DialogueDatabase>();
        btn = GetComponent<Button>();
        clickNum = 2;
        isRead = true;

        DialogueData[] dialogues = GameManager.Instance.GetStory("Farmer");
        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogue.Add(dialogues[i]);  //dialogue ����Ʈ�� DatabaseManager���� ������ ��� �߰�
        }

        ShowFirstDialogue();
        btn.onClick.AddListener(()=> ShowText(clickNum)); // Ŭ���� ��ȭ�� ���´�.
    }

    // Ŭ������ �ٷ� ù��� ����ϴ� �Լ�
    public void ShowFirstDialogue()
    {
        npcName.text = dialogue[1].name;

        StopAllCoroutines();
        StartCoroutine(TypeNpcText(npcText.text = dialogue[1].context));
        isRead = true;

        GameObject.FindGameObjectWithTag("������").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        GameObject.FindGameObjectWithTag("�ظ���").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
    }
    
    // ��縦 �����ִ� �Լ�
    public void ShowText(int i)  
    {
        if (i > 36) //��縦 ������ ����ϸ�
        {
            EndAndNextScene();
            return;
        }
        else if (i == 34)  // �๰���� �� ����̸�
        {
            clickNum = i + 1;

            SceneManager.LoadScene("MedicineScene");
            return;
        }

        npcName.text = dialogue[i].name;

        StopAllCoroutines();
        if (isRead == true)  // ó�� Ŭ���̸� ��� �ѱ��ھ� ������ ���
        {
            StartCoroutine(TypeNpcText(npcText.text = dialogue[i].context)); // �ؽ�Ʈ UI�� ��� ����
        }
        else  // ó��Ŭ���� �ƴ϶�� ��ü ��� ���
        {
            npcText.text = dialogue[i].context;
            isRead = true;  // �ٽ� �ѱ��ھ� ���ü� �ֵ��� bool�� �ٲ��ֱ�
            clickNum++;
        }
        
        manager.ChangeColor(i);  // ĳ���� ������
    }

    // �ѱ��ھ� ������ �����ϴ� �ڷ�ƾ �Լ�
    IEnumerator TypeNpcText(string npcText) 
    {
        isRead = false;  // ó�� Ŭ���ؼ� �Լ������� �ٽ� Ŭ�������� ��ü ��� ������ bool�� �ٲ��ֱ� 
        this.npcText.text = string.Empty;

        foreach (var letter in npcText)
        {
            this.npcText.text += letter;   //�ѱ��ھ� ����
            yield return new WaitForSeconds(0.1f);
        }
       
        if (this.npcText.text.Length == npcText.Length)  // ���� ��� ���̿� ��� ��ü ���̰� ������ 
        {
            clickNum++;
            isRead = true;  // �� ��簡 ������ ������絵 �ѱ��ھ� ������ bool�� ����
        }
    }

    // ��縦 ���ϸ� ��������� ����� �Լ�.
    private void EndAndNextScene()  
    {
        npcText.text = string.Empty;
        SceneManager.LoadScene("MedicineScene");
    }

    public void skip()
    {
        clickNum = 31;
        ShowText(clickNum);
    }
}
