using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardBehavior : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image image;
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI titre;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI accept;
    [SerializeField] private TextMeshProUGUI reject;

    public Vector3 initialPosition;
    public Vector3 iinitialPosition;
    private bool isMovable;
    public event Action cardMoved;
    private float _distanceMoved;
    private bool _swipeLeft;

    private void Start()
    {
        initialPosition = transform.localPosition;
        iinitialPosition = transform.localPosition;
    }

    public void DisplayQuestion(string questionText, string textText, string response1Text, string response2Text)
    {
        titre.text = questionText;
        text.text = textText;
        accept.text = response1Text;
        reject.text = response2Text;
    }

    public void SetMobility(bool isMovable)
    {
        this.isMovable = isMovable;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isMovable)
        {
            
        transform.localPosition += new Vector3(eventData.delta.x, 0, 0);

        float rotationZ = Mathf.LerpAngle(0, 30, Mathf.Abs(transform.localPosition.x - initialPosition.x) / (Screen.width / 2));
        transform.localEulerAngles = new Vector3(0, 0, transform.localPosition.x > initialPosition.x ? -rotationZ : rotationZ);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isMovable)
        {
            
        initialPosition = transform.localPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isMovable)
        {
            
        _distanceMoved = Mathf.Abs(transform.localPosition.x - initialPosition.x);

        if (_distanceMoved < 0.4f * Screen.width)
        {
            transform.localPosition = initialPosition;
            transform.localEulerAngles = Vector3.zero;
        }
        else
        {
            _swipeLeft = transform.localPosition.x < initialPosition.x;

            if (isMovable)
            {
                cardMoved?.Invoke();
                StartCoroutine(SwipeAndReset());
            }
        }
        }
    }

    private IEnumerator SwipeAndReset()
    {
        float duration = 0.3f;
        float elapsed = 0f;

        Vector3 startPos = transform.localPosition;
        Vector3 endPos = new Vector3(_swipeLeft ? -Screen.width : Screen.width, transform.localPosition.y, 0);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        // transform.localPosition = initialPosition;
        transform.localEulerAngles = Vector3.zero;
    }
}