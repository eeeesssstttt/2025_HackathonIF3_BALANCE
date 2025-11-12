using UnityEngine;

public class InstantiateCards : MonoBehaviour
{
    public GameObject cardPrefab;

    void InstantiateCard()
    {
        GameObject newCard = Instantiate(cardPrefab, transform, false);
        newCard.transform.SetAsFirstSibling();
    }

    void Update()
    {
        if (transform.childCount < 2)
        {
            InstantiateCard();
        }
    }
}
