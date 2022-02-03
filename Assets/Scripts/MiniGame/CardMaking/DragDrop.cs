using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
    [SerializeField] private Canvas canvas;
    
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    Vector3 pos;
    Letter letter;

    private GameObject[] frame; //eventData.PointerDrag�� �ִ� ���� ���� frame �̹������� ���ϱ� ����
    private Sprite[] decoframe; //Letter Component�� �ִ� image source �ٲܷ���
    GameObject findletter;

    bool isCheckingFirst = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        letter = GameObject.FindObjectOfType<Letter>().GetComponent<Letter>();

        findletter = GameObject.Find("Letter");
        frame = GameObject.FindGameObjectsWithTag("DecoFrame"); //frame �迭�� ������ �̹����� �ֱ�.
        decoframe = Resources.LoadAll<Sprite>("MiniGame/CardMaking/DecoFrame"); //������ �̹��� sprite ����.
        Debug.Log(decoframe.Length);

        pos = this.gameObject.transform.position; //ó�� ��ġ
    }

    
    //��� �����̱� �����ϸ� ī��Ʈ��.
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    //�巡�� ing. �巡�� ���� �� ī��Ʈ ����
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");

        if ((this.gameObject.CompareTag("DecoItem")) || (this.gameObject.CompareTag("DecoFrame"))) 
        {
            transform.position = eventData.position;
        }
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;   
    }

    //������ �ۿ��� ��� �� ī��Ʈ. --> ���������� ��� ��(Letter ��ũ��Ʈ�� OnDrop)
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        if (eventData.pointerDrag.CompareTag("DecoItem")) 
        {
            Flower(eventData);
        }
        else if(eventData.pointerDrag.CompareTag("DecoFrame"))
        {
            BackFrame(eventData);
        }
        else if (eventData.pointerDrag.CompareTag("DecoSentence"))
        {
            Sentence(eventData);
        }

        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
    }


    //�巡������ �ʰ� Ŭ���� �� ��
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }


    public void SetChecking(bool check)
    {
        isCheckingFirst = check;
    }

    void Flower(PointerEventData flower)
    {
        bool isEnter = letter.GetEnter();

        // isEnter = ������ ��ġ���� ����, isCheckingFirst = ó�� ������ ������ Ȯ�� ����
        if (flower.pointerDrag != null && isEnter && !isCheckingFirst)  // ���� Drag�̺�Ʈ ���� gameObject�� ������
        {
            GameObject fbox = Instantiate(flower.pointerDrag.gameObject, pos, Quaternion.identity);
            fbox.name = this.name;
            fbox.transform.SetParent(canvas.transform.Find("DecoItem").transform);
            fbox.GetComponent<CanvasGroup>().alpha = 1.0f;
            fbox.GetComponent<CanvasGroup>().blocksRaycasts = true;

            isCheckingFirst = true;
        }
    }

    void Sentence(PointerEventData sentence)
    {
        bool isEnter = letter.GetEnter();
        transform.rotation = Quaternion.Euler(0,0,0);
        // isEnter = ������ ��ġ���� ����, isCheckingFirst = ó�� ������ ������ Ȯ�� ����
        if (sentence.pointerDrag != null && isEnter && !isCheckingFirst)  // ���� Drag�̺�Ʈ ���� gameObject�� ������
        {
            GameObject sbox = Instantiate(sentence.pointerDrag.gameObject, pos, sentence.pointerDrag.gameObject.transform.rotation);
            sbox.name = this.name;
            sbox.transform.SetParent(canvas.transform.Find("DecoItem").transform);
            sbox.GetComponent<CanvasGroup>().alpha = 1.0f;
            sbox.GetComponent<CanvasGroup>().blocksRaycasts = true;

            isCheckingFirst = true;
        }
    }

    void BackFrame(PointerEventData backframe)
    {
        Debug.Log(frame.Length);
        for (int i = 0; i < frame.Length; i++)
        {
            if (frame[i].gameObject == backframe.pointerDrag.gameObject) 
            {
                Debug.Log(decoframe[i].name);
                findletter.GetComponent<Image>().sprite = decoframe[i]; 

                break;
            }
        }
        backframe.pointerDrag.gameObject.transform.position = pos; //������ �ٽ� ���� �ڸ��� ���ư���.
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        bool isEnter = letter.GetEnter();
        if (eventData.pointerDrag.CompareTag("DecoItem") && eventData.pointerDrag !=isEnter && isCheckingFirst)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                Destroy(eventData.pointerDrag.gameObject);
            }
        }
        else if (eventData.pointerDrag.CompareTag("DecoSentence") && eventData.pointerDrag != isEnter && isCheckingFirst)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                Destroy(eventData.pointerDrag.gameObject);
            }
        }
    }
}
