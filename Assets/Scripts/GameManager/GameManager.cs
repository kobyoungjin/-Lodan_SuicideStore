using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : InheritSingleton<GameManager>
{
    public Texture2D cursorImg;
    
    //AnimationManager animationManager;
    //SettingManager settingManager;
    DialogueDatabase dialogueDatabase;
    
    Dictionary<string, DialogueData[]> dialogueDicData = new Dictionary<string, DialogueData[]>();

    protected override void Awake()
    {
        base.Awake();

        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.ForceSoftware);  // ���콺 Ŀ�� �̹��� ����
        
        DontDestroyOnLoad(this.gameObject);
        return;
    }

    private void Start()
    {
        //animationManager = GameObject.FindObjectOfType<AnimationManager>().GetComponent<AnimationManager>();
        //settingManager = GameObject.FindObjectOfType<SettingManager>().GetComponent<SettingManager>();
        dialogueDatabase = GetComponent<DialogueDatabase>();

        LoadDialogData();
    }


    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ContinueGame()
    {
        Time.timeScale = 1;
    }

    // ��� �����͸� �����ϴ� �Լ�
    void LoadIngreData()
    { 

    }

    // DiaLogue �����͸� �����ϴ� �Լ�
    void LoadDialogData()  
    {
        TextAsset[] textFiles = Resources.LoadAll<TextAsset>("Dialogue");  // Resource/Dialogue ������ �ִ� ��� ���ϵ��� �����´�.

        for (int i = 0; i < textFiles.Length; i++)
        {
            dialogueDatabase.SaveData(textFiles[i]);
            dialogueDicData.Add(textFiles[i].name, dialogueDatabase.GetDialogue());
        }
    }

    // �ι� ���� ������ �����ϴ� �Լ�
    void LoadBookData()
    {
        TextAsset textFile = Resources.Load<TextAsset>("IngredientData");  //  Resource/Dialogue ������ �ִ� ������ �����´�.
        
        dialogueDatabase.SaveData(textFile);
        dialogueDicData.Add(textFile.name, dialogueDatabase.GetDialogue());
    }
    
    // �̾߱⸦ �������� �Լ�
    public DialogueData[] GetStory(string name)
    {
        if (name == null)  // �̸��� ������ null�� ��ȯ
        {
            Debug.Log("�̸��� �����ϴ�.");
            return null;
        }
           
        for (int i = 0; i < dialogueDicData.Count; i++)
        {
            if (dialogueDicData.ContainsKey(name))  // ��ǳʸ��� �̸��� ������ key���� �´� value�� ��ȯ
            {
                return dialogueDicData[name];
            }
        }

        Debug.Log("name �Է��� �߸��߽��ϴ�");
        return null;
    }
}

