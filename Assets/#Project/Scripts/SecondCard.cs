using UnityEngine;

public class SecondCard : MonoBehaviour
{
    private SwipeCard _swipeEffect;
    private GameObject _firstCard;

    void Start()
    {
        _swipeEffect = FindAnyObjectByType<SwipeCard>();
        _firstCard = _swipeEffect.gameObject;
        _swipeEffect.cardMoved += CardMovedFront;

        transform.localScale = new Vector3(3.5f, 5f, 1f);
    }

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
        gameObject.AddComponent<SwipeCard>();
        Destroy(this);
    }
}
