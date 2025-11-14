using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardChoice
{
    public int eco;
    public int pouvoir;
    public int population;
    public int sante;
}

[System.Serializable]
public class CardData
{
    public string id;
    public string category;
    public string title;
    public string image;
    public string text;
    public CardChoice accept;
    public CardChoice reject;
}

[System.Serializable]
public class Deck
{
    public string id;
    public string title;
    public List<CardData> cards;
}

[System.Serializable]
public class Root
{
    public List<Deck> decks;
}

public class CardManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private TextAsset jsonFile;
    private List<CardData> cardList;
    private int totalCards = 20;

    [SerializeField] private CardBehavior currentCard;
    [SerializeField] private CardBehavior nextCard;
    [SerializeField] private Transform cardCanvas;

    private int currentCardIndex = 0;

    void Start()
    {
        LoadData();
        PrepareFirstCards();
    }

    void Update()
    {
        ScaleNextCard();
        Debug.Log(currentCard.initialPosition);
    }

    private void LoadData()
    {
        Root root = JsonUtility.FromJson<Root>(jsonFile.text);

        cardList = root.decks[0].cards;// On prend uniquement le premier deck
    }

    private void PrepareFirstCards()
    {
        // currentCard.SaveInitialPosition();
        SetupCard(currentCard, currentCardIndex);
        currentCard.SetMobility(true);
        currentCard.gameObject.SetActive(true);
        currentCard.cardMoved += CardMovedFront;

        // nextCard.SaveInitialPosition();
        SetupCard(nextCard, GetNextIndex(currentCardIndex));
        nextCard.gameObject.SetActive(true);
        nextCard.transform.localScale = new Vector3(0.8f, 0.8f, 1f);

        cardCanvas.gameObject.SetActive(true);
    }

    private void SetupCard(CardBehavior card, int index)
    {
        var data = cardList[index];
        card.DisplayQuestion(data.title, data.text, "<- Accepter", "Rejecter ->");
    }

    private int GetNextIndex(int index)
    {
        return (index + 1) % totalCards;
    }

    private void ScaleNextCard()
    {
        float distanceMoved = currentCard.transform.localPosition.x;

        if (Mathf.Abs(distanceMoved) > 0)
        {
            float step = Mathf.SmoothStep(0.8f, 1f, Mathf.Abs(distanceMoved) / (Screen.width / 2));
            nextCard.transform.localScale = new Vector3(step, step, 1);
        }
    }

    private void ResetNextCardScale()
    {
        nextCard.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
        // Debug.Log("scaled next card back");
    }

    private void CardMovedFront()
    {
        // Swape current et next
        // var temp = currentCard;
        // currentCard = nextCard;
        // nextCard = temp;

        // Reset position et rotation de la carte qui derriere
        // nextCard.transform.localPosition = Vector3.zero;
        // nextCard.transform.localEulerAngles = Vector3.zero;
        // nextCard.transform.localScale = new Vector3(0.8f, 0.8f, 1f);

        currentCard.gameObject.SetActive(false);

        currentCard.transform.localEulerAngles = Vector3.zero;

        // Maj données de la carte derrière
        currentCardIndex = GetNextIndex(currentCardIndex);
        SetupCard(currentCard, GetNextIndex(currentCardIndex));

        // currentCard.ResetPosition();
        Debug.Log(currentCard.iinitialPosition);

        currentCard.gameObject.SetActive(true);

        if (currentCardIndex < totalCards)
        {
            SetupCard(currentCard, GetNextIndex(currentCardIndex + 1));

            ResetNextCardScale();
        }

        // Rendre la nouvelle carte devant interactive
        currentCard.SetMobility(true);
        currentCard.cardMoved += CardMovedFront;
    }
}