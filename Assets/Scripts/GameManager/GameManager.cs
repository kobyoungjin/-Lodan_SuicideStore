using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : InheritSingleton<GameManager>
{
    private Texture2D cursorImg;

    AnimationManager animationManager;
    //SettingManager settingManager;

    DialogueDatabase dialogueDatabase;
    IngredientDatabase ingredientDatabase;
    PeopleDatabase peopleDatabase;
    AddFrameIllrust script;

    List<PeopleData> peopleData = new List<PeopleData>();
    List<Sprite> characterImageList = new List<Sprite>();
    List<Sprite> backGroundImage = new List<Sprite>();
    Dictionary<string, DialogueData[]> dialogueDicData = new Dictionary<string, DialogueData[]>();
    Dictionary<string, List<IngredientData>> ingredientDicData = new Dictionary<string, List<IngredientData>>();
    Dictionary<string, string> ingreToTypeDicData = new Dictionary<string, string>();
    Dictionary<string, string[]> answerDicData = new Dictionary<string, string[]>();


    protected override void Awake()
    {
        base.Awake();

        cursorImg = Resources.Load<Texture2D>("Image/Title/MainCursor");
        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.ForceSoftware);  // ���콺 Ŀ�� �̹��� ����

        var objs = FindObjectsOfType<GameManager>();
        if (objs.Length == 1)  // GameManagerŸ���� ������ 1���϶��� 
            DontDestroyOnLoad(this.gameObject);
        else  // �ƴϸ� ����
            Destroy(this.gameObject);

        return;
    }

    private void Start()
    {
        animationManager = GameObject.FindObjectOfType<AnimationManager>().GetComponent<AnimationManager>();
        //settingManager = GameObject.FindObjectOfType<SettingManager>().GetComponent<SettingManager>();
        dialogueDatabase = GetComponent<DialogueDatabase>();
        ingredientDatabase = GetComponent<IngredientDatabase>();
        peopleDatabase = GetComponent<PeopleDatabase>();
        
        LoadCharacterImageData();
        LoadBackGroundImage();
        LoadDialogueData();
        LoadIngreToTypeData();
        LoadMakingAnswer();
        LoadPeopleBookData();
        //LoadIngreData();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  // esc ������ Ÿ��Ʋ ������ (�ӽ�)
        {
            GameManager.Instance.LoadNextScene("Title", 1.0f);
        }
    }

    // ���� ������� fade in �ִϸ��̼��� ���ʰ� �������� �θ��� �Լ�
    public void LoadNextScene(string nextScene, float duration)
    {
        animationManager.SetFadeScene(nextScene, duration);
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ContinueGame()
    {
        Time.timeScale = 1;
    }

    // Ingredient �����͸� �����ϴ� �Լ�
    void LoadIngreData()
    {
        TextAsset textFile = Resources.Load<TextAsset>("MakingRoom/IngredientData/Ingredient");  // Ingredient �з�ǥ�� �����´�.
        ingredientDatabase.SaveData(textFile);

        List<string> typeData = ingredientDatabase.GetIngredientTypeList();
        List<IngredientData> ingredientData = ingredientDatabase.GetIngredientList();

        for (int i = 0; i < ingredientData.Count; i++)
        {
            if (ingredientDicData.ContainsKey(typeData[i]))     // ��Ǿ�� �̹� Ű���� �����Ѵٸ�
            {
                ingredientDicData[typeData[i]].Add(ingredientData[i]);  // �� Ű���� �̾ �߰�
                continue;
            }

            ingredientDicData[typeData[i]] = new List<IngredientData> { ingredientData[i] };  // Ű���� ���� x ��Ǿ�� ���ο� key��, value�� ����
        }
    }

    // Ingredient�� (��ܿ�)Ÿ���� ã�� �Լ�
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

    // Ingreient ������ �����ϴ� �Լ� (��ܿ� -> �̸�)
    void LoadIngreToTypeData()
    {
        TextAsset textFile = Resources.Load<TextAsset>("MakingRoom/IngredientData/Ingredient");  // Ingredient �з�ǥ�� �����´�.
        ingredientDatabase.SaveData(textFile);

        List<string> typeData = ingredientDatabase.GetIngredientTypeList();
        List<IngredientData> ingredientData = ingredientDatabase.GetIngredientList();

        for (int i = 0; i < ingredientData.Count; i++)
        {
            ingreToTypeDicData.Add(ingredientData[i].name, typeData[i]);
        }
    }
    
    // ����̸����� ��ܿ� ã��
    public string GetFindIngreToType(string name)
    {
        if (name == null)  // �̸��� ������ null�� ��ȯ
        {
            Debug.Log("�̸��� �����ϴ�.");
            return null;
        }

        for (int i = 0; i < ingreToTypeDicData.Count; i++)
        {
            if (ingreToTypeDicData.ContainsKey(name))  // ��ǳʸ��� �̸��� ������ key���� �´� value�� ��ȯ
            {
                return ingreToTypeDicData[name];
            }
        }

        Debug.Log("string�� �������� ���߽��ϴ�." + name);
        return null;
    }

    // Ingredient�� ��з� Ÿ�� ����Ʈ�� ��ȯ�ϴ� �Լ�
    public List<string> GetTypeList()
    {
        List<string> typeData = ingredientDatabase.GetIngredientTypeList();

        return typeData;
    }

    // Ingredient �����͸� ��ȯ�ϴ� �Լ�
    public List<IngredientData> GetIngreAllData()
    {
        List<IngredientData> ingredientData = ingredientDatabase.GetIngredientList();

        return ingredientData;
    }

    // MakingRoom ���ս� �� 
    void LoadMakingAnswer()
    {
        TextAsset textFile = Resources.Load<TextAsset>("MakingRoom/IngredientData/Answer");  // Ingredient ����ǥ�� �ҷ��´�.
        Debug.Log(textFile.name);
        ingredientDatabase.SaveData(textFile);
        
        List<Answer> answerData = ingredientDatabase.GetAnswerList();

        for (int i = 0; i < answerData.Count; i++)
        {
            answerDicData.Add(answerData[i].name, answerData[i].emotion);
        }
    }

    // ���丮�� ���� ���ս� �� ã�� �Լ�
    public string[] FindAnswer(string name)
    {
        Debug.Log(name);
        if (name == null)  // �̸��� ������ null�� ��ȯ
        {
            Debug.Log("�̸��� �����ϴ�.");
            return null;
        }

        for (int i = 0; i < answerDicData.Count; i++)
        {
            if (answerDicData.ContainsKey(name))  // ��ǳʸ��� �̸��� ������ key���� �´� value�� ��ȯ
            {
                return answerDicData[name];
            }
        }

        Debug.Log("string[]�� �������� ���߽��ϴ�." + name);
        return null;
    }

    // DiaLogue �����͸� �����ϴ� �Լ�
    void LoadDialogueData()
    {
        TextAsset[] textFiles = Resources.LoadAll<TextAsset>("Dialogue/Chapter1/Text");  // Resource/Dialogue ������ �ִ� ��� ���ϵ��� �����´�.
        TextAsset[] behind = Resources.LoadAll<TextAsset>("Dialogue/Behind/Text");
        for (int i = 0; i < textFiles.Length; i++)
        {
            dialogueDatabase.SaveData(textFiles[i]);
            dialogueDicData.Add(textFiles[i].name, dialogueDatabase.GetDialogue());
        }

        for (int i = 0; i < textFiles.Length; i++)
        {
            dialogueDatabase.SaveData(behind[i]);
            dialogueDicData.Add(behind[i].name, dialogueDatabase.GetDialogue());
        }
    }

    // DiaLogue�����͸� �������� �Լ�
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

    // �ι� ���� ������ �����ϴ� �Լ�
    void LoadPeopleBookData()
    {
        TextAsset textFile = Resources.Load<TextAsset>("WaitingRoom/PeopleBookData/People");  //  Resource/Dialogue ������ �ִ� ������ �����´�.

        peopleDatabase.SaveData(textFile);
        peopleData = peopleDatabase.GetPeopleData();
    }

    // Ÿ�Կ� ���� ����� �Ϻ��� �����Ǹ� ��ȯ�ϴ� �Լ�
    public string FindPeopleText(string storyName, string type)
    {
        if(type == "explain")
        {
            for (int i = 0; i < peopleData.Count; i++)
            {
                if (peopleData[i].name == storyName)
                    return peopleData[i].explain;
            }
        }
        else if(type == "PerfectRecipe")
        {
            for (int i = 0; i < peopleData.Count; i++)
            {
                if (peopleData[i].name == storyName)
                    return peopleData[i].PerfectRecipe;
            }
        }

        Debug.Log("FindPeopletext ����");
        return null;
    }

    // �ι� ��������Ʈ ������ ���������Լ�
    void LoadCharacterImageData()
    {
        Sprite[] sprite = Resources.LoadAll<Sprite>("Dialogue/Chapter1/Character");

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

    // ��� �̹��� �������� �Լ�
    void LoadBackGroundImage()
    {
        Sprite[] sprite = Resources.LoadAll<Sprite>("Dialogue/Behind/BackGround");

        for (int i = 0; i < sprite.Length; i++)
        {
            backGroundImage.Add(sprite[i]);
        }
    }

    // ��� �̹��� ��ȯ�ϴ� �Լ�
    public List<Sprite> GetBackGroundImageData()
    {
        return backGroundImage;
    }
}
