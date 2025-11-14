using System.Collections.Generic;
using UnityEngine;

// Prepping classes to read from the JSON file.
[System.Serializable] // carte individuellement
public class CardData
{
    public string question;
    public string reponse1;
    public string reponse2;
    public int score1; // associé à la reponse 1 
    public int score2; // associé à la reponse 2
}

[System.Serializable] // l'ensemble des cartes
public class CardList
{
    public List<CardData> cards;
}




public class CardManager : MonoBehaviour
{
    // GameManager
    [SerializeField] private GameManager gameManager;

    // JSON data
    [SerializeField] private TextAsset jsonFile;
    private CardList cardList;

    // Cards and Canvas objects
    [SerializeField] private CardBehavior currentCard;
    [SerializeField] private CardBehavior nextCard;
    [SerializeField] private Transform cardCanvas;

    // Card index and total amount of cards.
    private int currentCardIndex = 0;
    private int totalCards = 0; // à definir

    void Start()
    {
        LoadData();
        PrepareFirstCards();
        // Debug.Log(cardList.cards.Count);
        // Debug.Log(totalCards);
    }

    void Update()
    {
        ScaleNextCard();
    }

    private void LoadData() // charge données json
    {
        cardList = JsonUtility.FromJson<CardList>(jsonFile.text);
        totalCards = cardList.cards.Count; // recup nombre total de cartes
    }

    public void PrepareFirstCards()
    {
        currentCard.DisplayQuestion("current", "current", "current", "current"); // cardList.cards[currentCardIndex]
        currentCard.SetMobility(true);
        currentCard.gameObject.SetActive(true);
        currentCard.cardMoved += CardMovedFront;

        nextCard.DisplayQuestion("next", "next", "next", "next"); // cardList.cards[currentCardIndex + 1]
        nextCard.gameObject.SetActive(true);
        nextCard.transform.localScale = new Vector3(0.8f, 0.8f, 1f);

        cardCanvas.gameObject.SetActive(true);
    }

    public void ScaleNextCard()
    {
        float distanceMoved = currentCard.transform.localPosition.x;

        if (Mathf.Abs(distanceMoved) > 0)
        {
            float step = Mathf.SmoothStep(0.8f, 1, Mathf.Abs(distanceMoved) / (Screen.width / 2));
            float step2 = Mathf.SmoothStep(0.8f, 1, Mathf.Abs(distanceMoved) / (Screen.width / 2));
            nextCard.transform.localScale = new Vector3(step2, step, 1);
        }
    }

    void CardMovedFront()
    {
        UpdateCurrentCard(); // La currentcard doit disparaître temporairement puis réapparaître à sa position initiale avec les infos de la carte de currentCardIndex + 1, si possible, currentCardIndex += 1 si possible
        UpdateNextCard(); // La nextcard doit récupérer les informations de la du nouveau currentCardIndex + 1, si possible.
    }


    public void UpdateCurrentCard()
    {
        // La currentcard doit disparaître temporairement puis réapparaître à sa position initiale avec les infos de la carte de currentCardIndex + 1, si possible
        // currentCardIndex += 1 si possible

        // ANCIEN CODE (CreateNextCard)
        // if (currentCardIndex >= totalCards) // si toutes les cartes sont passé --> fin du jeu
        // {
        //     EndGame();
        //     return;
        // }

        // var cardData = cardList.cards[currentCardIndex];

        // currentCard.DisplayQuestion(null, cardData.question, cardData.reponse1, cardData.reponse2); // affiche question et reponses sur la carte
    }

    public void UpdateNextCard()
    {
        // La nextcard doit récupérer les informations de la du nouveau currentCardIndex + 1, si possible.
    }

    // private void OnCardSwiped() // fonction appelée quand la carte est swipé
    // {
    //     // if (currentCardIndex % 2 == 0)
    //     // {
    //     //     totalScore += cardList.cards[currentCardIndex].score1;
    //     // }
    //     // else
    //     // {
    //     //     totalScore += cardList.cards[currentCardIndex].score2;
    //     // }
    //     // currentCardIndex++; // on fait +1 dans l'index
    //     // progressBar.value = currentCardIndex; // met à jour la bar de progression

    //     CreateNextCard(); // crée la carte d'apres
    // }

}
