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
    /* Card Manager must gather: InstantiateCards, SwipeCard, SecondCard */


    private CardList cardList;
    private int currentCardIndex = 0;
    private int totalCards = 0; // à definir


    //elements UI
    [SerializeField] private Transform cardParent;
    [SerializeField] private GameObject cardPrefab;

    // json data
    [SerializeField] private TextAsset jsonFile;

    private void LoadData() // charge données json
    {
        cardList = JsonUtility.FromJson<CardList>(jsonFile.text);
        totalCards = cardList.cards.Count; // recup nombre total de cartes
    }


    public void CreateNextCard()
    {
        // if (currentCardIndex >= totalCards) // si toutes les cartes sont passé --> fin du jeu
        // {
        //     EndGame();
        //     return;
        // }

        GameObject newCard = Instantiate(cardPrefab, cardParent, false); // instantie nouvelles carte à partir du prefabs
        SwipeCard swipe = newCard.GetComponent<SwipeCard>(); // recup comportement de SwipeCard.cs
        CardBehavior behavior = newCard.GetComponent<CardBehavior>();

        swipe.cardMoved += OnCardSwiped; // quand la carte esr swipé on appel la fonction

        var cardData = cardList.cards[currentCardIndex];

        behavior.DisplayQuestion(null, cardData.question, cardData.reponse1, cardData.reponse2); // affiche question et reponses sur la carte
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
    }
}
