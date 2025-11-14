using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeCard : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public event Action<bool> cardMoved;
    private Vector3 _initialPosition;
    private bool _swipeLeft;
    public GameManager gameManager;

    public void OnDrag(PointerEventData eventData)
    {
        transform.localPosition = new Vector2(transform.localPosition.x + eventData.delta.x, transform.localPosition.y);

        if (transform.localPosition.x - _initialPosition.x > 0)
        {
            transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(0, -30, (_initialPosition.x + transform.localPosition.x) / (Screen.width / 2)));
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(0, 30, (_initialPosition.x - transform.localPosition.x) / (Screen.width / 2)));
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _initialPosition = transform.localPosition;
    }

//_______________________________________________________________________________________________ detection swipe gauche/droite
    public void OnEndDrag(PointerEventData eventData)
    {
        float dir = eventData.delta.x; 

        if (dir < 0)
        {
            Debug.Log("SWIPE GAUCHE");
            if(gameManager != null) gameManager.OnCardSwiped(true);
        }
        else
        {
            Debug.Log("SWIPE DROITE");
            if(gameManager != null) gameManager.OnCardSwiped(false);
        }

        StartCoroutine(MovedCard());
    }
//_______________________________________________________________________________________________

    private IEnumerator MovedCard()
    {
        float time = 0;
        Image image = GetComponent<Image>();

        while (image.color.a > 0.01f)
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
            image.color = new Color(1, 1, 1, Mathf.SmoothStep(1, 0, 4 * time));
            yield return null;
        }

        // Destroy(gameObject);
    }
}
