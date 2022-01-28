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

    List<Sprite> characterImageList = new List<Sprite>();
    Dictionary<string, DialogueData[]> dialogueDicData = new Dictionary<string, DialogueData[]>();
    Dictionary<string, List<IngredientData>> ingredientDicData = new Dictionary<string, List<IngredientData>>();

    protected override void Awake()
    {
        base.Awake();

        cursorImg = Resources.Load<Texture2D>("Image/Title/MainCursor");
        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.ForceSoftware);  // ���콺 Ŀ�� �̹��� ����

        DontDestroyOnLoad(this.gameObject);
        return;
    }

    private void Start()
    {
        animationManager = GameObject.FindObjectOfType<AnimationManager>().GetComponent<AnimationManager>();
        //settingManager = GameObject.FindObjectOfType<SettingManager>().GetComponent<SettingManager>();
        dialogueDatabase = GetComponent<DialogueDatabase>();
        ingredientDatabase = GetComponent<IngredientDatabase>();

        LoadCharacterImageData();
        LoadDialogueData();
        LoadIngreData();
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

    // DiaLogue �����͸� �����ϴ� �Լ�
    void LoadDialogueData()
    {
        TextAsset[] textFiles = Resources.LoadAll<TextAsset>("Dialogue/Text");  // Resource/Dialogue ������ �ִ� ��� ���ϵ��� �����´�.

        for (int i = 0; i < textFiles.Length; i++)
        {
            dialogueDatabase.SaveData(textFiles[i]);
            dialogueDicData.Add(textFiles[i].name, dialogueDatabase.GetDialogue());
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
    void LoadBookData()
    {
        TextAsset textFile = Resources.Load<TextAsset>("MakingRoom/IngredientData");  //  Resource/Dialogue ������ �ִ� ������ �����´�.

        dialogueDatabase.SaveData(textFile);
        dialogueDicData.Add(textFile.name, dialogueDatabase.GetDialogue());
    }

    // �ι� ��������Ʈ ������ ���������Լ�
    void LoadCharacterImageData()
    {
        Sprite[] sprite = Resources.LoadAll<Sprite>("Dialogue/Character");

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

    public List<string> GetTypeList()
    {
        List<string> typeData = ingredientDatabase.GetIngredientTypeList();

        return typeData;
    }

    public List<IngredientData> GetIngreAllData()
    {
        List<IngredientData> ingredientData = ingredientDatabase.GetIngredientList();

        return ingredientData;
    }
}




