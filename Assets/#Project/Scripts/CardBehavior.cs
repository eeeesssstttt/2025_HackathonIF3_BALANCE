using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// [RequireComponent(typeof(CanvasRenderer))]
public class CardBehavior : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // The card has a canvas to display text.
    [SerializeField] private Canvas canvas;


    // The card knows its contents : a question, the corresponding image and both possible answers.
    // [SerializeField] private Image background;
    [SerializeField] private Image image;
    [SerializeField] private Image background;

    [SerializeField] private TextMeshProUGUI titre;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI accept;
    [SerializeField] private TextMeshProUGUI reject;


    // CURRENTCARD (formerly SwipeCard) ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private Vector3 initialPosition; // Initial card position.


    public event Action cardMoved;
    private float _distanceMoved;
    private bool _swipeLeft;


    public void OnDrag(PointerEventData eventData)
    {
        transform.localPosition = new Vector2(transform.localPosition.x + eventData.delta.x, transform.localPosition.y);

        if (transform.localPosition.x - initialPosition.x > 0)
        {
            transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(0, -30, (initialPosition.x + transform.localPosition.x) / (Screen.width / 2)));
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(0, 30, (initialPosition.x - transform.localPosition.x) / (Screen.width / 2)));
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        initialPosition = transform.localPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _distanceMoved = Mathf.Abs(transform.localPosition.x - initialPosition.x);
        if (_distanceMoved < 0.4 * Screen.width)
        {
            transform.localPosition = initialPosition;
            transform.localEulerAngles = Vector3.zero;
        }
        else
        {
            if (transform.localPosition.x > initialPosition.x)
            {
                _swipeLeft = false;

            }
            else
            {
                _swipeLeft = true;
            }
            cardMoved?.Invoke();
            StartCoroutine(MovedCard());
        }
    }

    private IEnumerator MovedCard()
    {
        float time = 0;
        while (GetComponent<Image>().color != new Color(1, 1, 1, 0))
        {
            time += Time.deltaTime;
            if (_swipeLeft)
            {
                transform.localPosition = new Vector3(Mathf.SmoothStep(transform.localPosition.x,
                    transform.localPosition.x - Screen.width, time), transform.localPosition.y, 0);
            }
            else
            {
                transform.localPosition = new Vector3(Mathf.SmoothStep(transform.localPosition.x,
                    transform.localPosition.x + Screen.width, time), transform.localPosition.y, 0);
            }
            GetComponent<Image>().color = new Color(1, 1, 1, Mathf.SmoothStep(1, 0, 4 * time));
            yield return null;
        }
        Destroy(gameObject);
    }

    // NEXTCARD (formerly SecondCard) ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    // private SwipeCard _swipeEffect;
    private GameObject _firstCard;

    // void Start()
    // {
    //     // _swipeEffect = FindAnyObjectByType<SwipeCard>();
    //     // _firstCard = _swipeEffect.gameObject;
    //     // _swipeEffect.cardMoved += CardMovedFront;

    //     transform.localScale = new Vector3(3.5f, 5f, 1f);
    // }

    void Update()
    {
        float distanceMoved = _firstCard.transform.localPosition.x;

        if (Mathf.Abs(distanceMoved) > 0)
        {
            float step = Mathf.SmoothStep(0, 5, Mathf.Abs(distanceMoved) / (Screen.width / 2));
            float step2 = Mathf.SmoothStep(0, 3, Mathf.Abs(distanceMoved) / (Screen.width / 2));
            transform.localScale = new Vector3(step2, step, 1);
            // Debug.Log($"grandir {step}");
        }
    }

    void CardMovedFront()
    {
        // gameObject.AddComponent<SwipeCard>();
        // Destroy(this);
    }






    // private void Start()
    // {
    //     DisplayQuestion(testSprite, testQuestion, testResponse1, testResponse2);
    // }

    public void Start()
    {
        gameObject.SetActive(false);
        background.gameObject.SetActive(true);
        image.gameObject.SetActive(true);
        titre.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        accept.gameObject.SetActive(true);
        reject.gameObject.SetActive(true);

        initialPosition = transform.position;
    }

    // public void DisplayQuestion(Sprite illustrationSprite, string questionText, string response1Text, string response2Text)
    public void DisplayQuestion(string questionText, string textText, string response1Text, string response2Text)
    {
        // illustration.sprite = illustrationSprite;

        titre.text = questionText;
        text.text = textText;
        accept.text = response1Text;
        reject.text = response2Text;

        // background.gameObject.SetActive(true);
        // canvas.gameObject.SetActive(true);
    }

    public void HideQuestion()
    {
        // background.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
    }
}