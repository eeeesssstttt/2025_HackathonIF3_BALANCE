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
    // The card knows its GameManager.
    [SerializeField] private GameManager gameManager;
    /* Card Manager must gather: InstantiateCards, SwipeCard, SecondCard */

    [SerializeField] private CardBehavior currentCard;
    [SerializeField] private CardBehavior nextCard;



    private CardList cardList;
    private int currentCardIndex = 0;
    private int totalCards = 0; // à definir


    //elements UI
    [SerializeField] private Transform cardCanvas;
    // [SerializeField] private CardBehavior cardPrefab;

    // json data
    [SerializeField] private TextAsset jsonFile;



    private void LoadData() // charge données json
    {
        cardList = JsonUtility.FromJson<CardList>(jsonFile.text);
        totalCards = cardList.cards.Count; // recup nombre total de cartes
    }

    public void PrepareFirstCards()
    {
        // CardBehavior currentCard = Instantiate(cardPrefab, cardParent, false); // instantie nouvelles carte à partir du prefabs
        // currentCard.GetComponent<Renderer>().sortingLayerName = "CurrentCard";
        currentCard.DisplayQuestion("current", "current", "current", "current"); // cardList.cards[currentCardIndex]
        currentCard.gameObject.SetActive(true);

        // CardBehavior nextCard = Instantiate(cardPrefab, cardParent, false); // instantie nouvelles carte à partir du prefabs
        // nextCard.GetComponent<Renderer>().sortingLayerName = "NextCard";
        nextCard.DisplayQuestion("next", "next", "next", "next"); // cardList.cards[currentCardIndex + 1]
        nextCard.gameObject.SetActive(true);

        cardCanvas.gameObject.SetActive(true);
    }

    // public void UpdateCurrentCard()
    // {

    // }

    // public void PrepareNextCard()
    // {

    // }


    public void CreateNextCard()
    {
        // if (currentCardIndex >= totalCards) // si toutes les cartes sont passé --> fin du jeu
        // {
        //     EndGame();
        //     return;
        // }

        // CardBehavior currentCard = Instantiate(cardPrefab, cardParent, false); // instantie nouvelles carte à partir du prefabs


        // SwipeCard swipe = currentCard.GetComponent<SwipeCard>(); // recup comportement de SwipeCard.cs
        // CardBehavior behavior = newCard.GetComponent<CardBehavior>();

        currentCard.cardMoved += OnCardSwiped; // quand la carte esr swipé on appel la fonction

        // var cardData = cardList.cards[currentCardIndex];

        // currentCard.DisplayQuestion(null, cardData.question, cardData.reponse1, cardData.reponse2); // affiche question et reponses sur la carte
    }

    private void OnCardSwiped() // fonction appelée quand la carte est swipé
    {
        // if (currentCardIndex % 2 == 0)
        // {
        //     totalScore += cardList.cards[currentCardIndex].score1;
        // }
        // else
        // {
        //     totalScore += cardList.cards[currentCardIndex].score2;
        // }
        // currentCardIndex++; // on fait +1 dans l'index
        // progressBar.value = currentCardIndex; // met à jour la bar de progression

        CreateNextCard(); // crée la carte d'apres
    }

    void Start()
    {
        LoadData();
        PrepareFirstCards();
        Debug.Log(cardList.cards.Count);
        Debug.Log(totalCards);
    }
}
