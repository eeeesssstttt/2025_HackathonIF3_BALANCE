using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable] // carte individuellement
public class CardData
{
    public string question;
    public string reponse1;
    public string reponse2;
    public Effect score1; // associé à la reponse 1 
    public Effect score2; // associé à la reponse 2

}

[System.Serializable] // l'ensemble des cartes
public class CardList
{
    public CardData[] cards;
}

[System.Serializable] 
public class Deck
{
    public string id;
    public CardData[] cards;
}

[System.Serializable] 
public class DeckList
{
    public Deck[] decks;
}
    
public class GameManager : MonoBehaviour
{

    // "scene manager"
    [SerializeField] SceneLoader sceneLoader;

    //elements UI
    [SerializeField] private Slider progressBar;
    [SerializeField] private Transform cardParent;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private ScoreManager scoreManager;

    // json data
    [SerializeField] private TextAsset jsonFile;

    private CardList cardList;
    private GameObject currentCard;
    private int currentCardIndex = 0;
    private int totalCards = 0; // à definir

    void Start()
    {
        LoadData();

        if (cardList == null || cardList.cards.Length == 0)
        {
            Debug.LogError("Aucune carte trouvée dans le JSON !");
            return;
        }

        totalCards = cardList.cards.Length;
        progressBar.minValue = 0;
        progressBar.maxValue = totalCards; // à définir
        progressBar.value = 0;

        CreateNextCard();
    }

    private void LoadData() // charge données json
    {
        cardList = JsonUtility.FromJson<CardList>(jsonFile.text);
        // totalCards = cardList.cards.Count; // recup nombre total de cartes
    }


    public void CreateNextCard()
    {
        if (currentCard != null)
        {
            Destroy(currentCard);
        }
        if (currentCardIndex >= totalCards) // si toutes les cartes sont passé --> fin du jeu
        {
            EndGame();
            return;
        }

        currentCard = Instantiate(cardPrefab, cardParent, false);// instantie nouvelles carte à partir du prefabs

        SwipeCard swipe = currentCard.GetComponent<SwipeCard>(); 
        CardBehavior behavior = currentCard.GetComponent<CardBehavior>();

        swipe.gameManager = this;

        behavior.DisplayQuestion(cardList.cards[currentCardIndex].question, cardList.cards[currentCardIndex].question, cardList.cards[currentCardIndex].reponse1,cardList.cards[currentCardIndex].reponse2 ); // affiche question et reponses sur la carte
    }

    public void OnCardSwiped(bool swipeLeft ) // fonction appelée quand la carte est swipé
    {
        var cardData = cardList.cards[currentCardIndex];

        if (swipeLeft)
        {
            scoreManager.SliderEffect(cardData.score1);
            Debug.Log("1 = swipe accept");
        }
        else
        {
            scoreManager.SliderEffect(cardData.score2);
            Debug.Log("0 = swipe reject");
        }

        currentCardIndex++;
        progressBar.value = currentCardIndex;

        CreateNextCard();
    }
    
    private void EndGame() 
    {
        Debug.Log ("end game");
        sceneLoader.ChangeScene("End_balance");

    }

    public void GameStarted()
    {
        sceneLoader.ChangeScene("MainScene");
    }
}