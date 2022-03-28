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

    private int clickNum = SceneFlowManager.Instance.GetSaveDialogueNum();  // ���° ������� �������� 
    int medicineSceneNum;
    private bool isRead = true;  // ��簡 �̹� ��Ÿ���� �ִ��� �Ǻ��ϴ� ����(true�� �ѱ��ھ� ������)

    SceneNumber currentState;
    List<DialogueData> dialogue = new List<DialogueData>();

    private void Start()
    {
        manager = GameObject.FindObjectOfType<CharacterManager>().GetComponent<CharacterManager>();
        database = GameObject.FindObjectOfType<DialogueDatabase>().GetComponent<DialogueDatabase>();
        btn = GetComponent<Button>();

        Transform dialogueBar = GameObject.Find("Dialogue_Bar").transform;
        npcText = dialogueBar.GetChild(0).GetComponent<TextMeshProUGUI>();
        npcName = dialogueBar.GetChild(1).GetComponent<TextMeshProUGUI>();

        npcText.text = null;
        npcName.text = null;

        string currentStateName = null;
        if (SceneManager.GetActiveScene().name == "BehindDialogueScene")
        {
            currentStateName = SceneFlowManager.Instance.GetBehindName();
        }
        else
        {
            currentState = SceneFlowManager.Instance.GetCurrentState();
            currentStateName = currentState.ToString();
        }

        Debug.Log(currentStateName);
        SettingStory(currentStateName);

        //ShowFirstDialogue();
        btn.onClick.AddListener(() => ShowText(clickNum)); // Ŭ���� ��ȭ�� ���´�.
    }

    // Ŭ������ �ٷ� ù��� ����ϴ� �Լ�
    public void ShowFirstDialogue()
    {
        npcName.text = dialogue[0].name;

        StopAllCoroutines();
        StartCoroutine(TypeNpcText(npcText.text = dialogue[1].context));
        isRead = true;

        GameObject.FindGameObjectWithTag("������").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        GameObject.FindGameObjectWithTag("Customer").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
    }
    
    // ��縦 �����ִ� �Լ�
    public void ShowText(int i)  
    {
        if (i >= dialogue.Count) //��縦 ������ ����ϸ�
        {
            EndAndNextScene();
            return;
        }
        //else if (i == medicineSceneNum)  // �̸�ĭ�� ��ĭ�� ������
        //{
        //    Debug.Log(medicineSceneNum);
        //    clickNum = i + 1;
        //    SceneFlowManager.Instance.SetSaveDialogueNum(clickNum);
        //    //GameManager.Instance.LoadNextScene("MedicineScene", 1.0f);
        //    return;
        //}

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
            this.npcText.text += letter;   // �ѱ��ھ� ����
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
        GameManager.Instance.LoadNextScene("MedicineScene", 1.0f);
        clickNum = 0;
    }

    public List<DialogueData> GetCurrentDialogue()
    {
        return dialogue;
    }

    public void SetMedicineNum(int num)
    {
        medicineSceneNum = num;
    }

    public void skip()
    {
        clickNum = dialogue.Count-1;
        ShowText(clickNum);
    }

    // ���丮�� �ʿ���ĳ���Ϳ� ��� �������� �Լ�
    void SettingStory(string StateName)
    {
        dialogue.Clear();

        if (SceneManager.GetActiveScene().name == "BehindDialogueScene")
        {
            List<Sprite> backGroundImage = GameManager.Instance.GetBackGroundImageData();
            Image backGround = GameObject.Find("BackGround(Image)").GetComponent<Image>();
            for (int i = 0; i < backGroundImage.Count; i++)
            {
                if (backGroundImage[i].name == StateName)
                    backGround.sprite = backGroundImage[i];
            }
        }

        manager.ChooseCharacter(StateName);
        DialogueData[] dialogues = GameManager.Instance.GetStory(StateName);
        Debug.Log(dialogues.Length);
        for (int i = 0; i < dialogues.Length; i++)
        {
            if (dialogues[i].name == "")
            {
                medicineSceneNum = i;
                continue;
            }
            dialogue.Add(dialogues[i]);  //dialogue ����Ʈ�� DatabaseManager���� ������ ��� �߰�
        }
    }
    
}
