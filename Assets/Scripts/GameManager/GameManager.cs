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
    IngredientDatabase ingredientDatabase;
    SceneFlowManager sceneFlowManager;

    List<Sprite> characterImageList = new List<Sprite>();
    Dictionary<string, DialogueData[]> dialogueDicData = new Dictionary<string, DialogueData[]>();
    Dictionary<string, List<IngredientData>> ingredientDicData = new Dictionary<string, List<IngredientData>>();

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
        ingredientDatabase = GetComponent<IngredientDatabase>();
        sceneFlowManager = GetComponent<SceneFlowManager>();

        LoadCharacterImageData();
        LoadDialogueData();
        LoadIngreData();
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
        TextAsset textFile = Resources.Load<TextAsset>("IngredientData/Ingredient");  // Ingredient �з�ǥ�� �����´�.
        ingredientDatabase.SaveData(textFile);

        List<string> typeData = ingredientDatabase.GetIngredientTypeList();
        List<IngredientData> ingredientData = ingredientDatabase.GetIngredientList();

        for (int i = 0; i < ingredientData.Count; i++)
        {
            if (ingredientDicData.ContainsKey(typeData[i]))     // ��Ǿ 
            {
                ingredientDicData[typeData[i]].Add(ingredientData[i]);
                continue;
            }

            ingredientDicData[typeData[i]] = new List<IngredientData> { ingredientData[i] };  // ��Ǿ�� key��, value�� ����
        }
    }

    // DiaLogue �����͸� �����ϴ� �Լ�
    void LoadDialogueData()  
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

    // �ι� ��������Ʈ ������ ���������Լ�
    void LoadCharacterImageData()
    {
        Sprite[] sprite = Resources.LoadAll<Sprite>("Image/Character");

        for (int i = 0; i < sprite.Length; i++)
        {
            characterImageList.Add(sprite[i]);
        }
    }
    
    // �ι� ��������Ʈ ����Ʈ Getter�Լ�
    public List<Sprite> GetCharacterData()
    {
        return characterImageList;
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

    public List<IngredientData> GetFindTypeList(string type)
    {
        Debug.Log(type);
        if (type == null)  // �̸��� ������ null�� ��ȯ
        {
            Debug.Log("�̸��� �����ϴ�.");
            return null;
        }

        for (int i = 0; i < ingredientDicData.Count; i++)
        {
            if (ingredientDicData.ContainsKey(type))  // ��ǳʸ��� �̸��� ������ key���� �´� value�� ��ȯ
            {
                return ingredientDicData[type];
            }
        }

        Debug.Log("����Ʈ�� �������� ���߽��ϴ�.");
        return null;
    }
}

