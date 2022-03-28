using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneNumber
{
    ����Ʈ = 0,
    �ظ��� = 1,
    �ַ�_���� = 2,
    ������ = 3,
    �긮�Ƴ� = 4,
    ���� = 5,
    ���� = 6,
    �̵� = 7,
    �� = 8,
    ������ = 9,
    ���� = 10,
    �뷻 = 11,
}

public enum Playing
{
    Dialogue,
    MiniGame,
}

public class SceneFlowManager : InheritSingleton<SceneFlowManager>
{
    SceneNumber currentFlow = SceneNumber.����;
    int saveNumer = 1;
    string setBehindName;

    protected override void Awake()
    {
        base.Awake();

        var objs = FindObjectsOfType<SceneFlowManager>();
        if (objs.Length == 1)
            DontDestroyOnLoad(this.gameObject);
        else
            Destroy(this.gameObject);

        return;
    }

    public SceneNumber GetCurrentState()
    {
        return currentFlow;
    }

    public void SetNextStory()
    {
        Debug.Log(currentFlow);
        
        currentFlow += 1;

        Debug.Log(currentFlow);
        saveNumer = 1;
    }

    public void SetSaveDialogueNum(int num)
    {
        saveNumer = num;
    }

    public int GetSaveDialogueNum()
    {
        return saveNumer;
    }

    public void SetBehindName(string name)
    {
        setBehindName = name + "Behind";
    }
    public string GetBehindName()
    {
        return setBehindName;
    }
}
