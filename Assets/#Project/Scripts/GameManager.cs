using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    
public class GameManager : MonoBehaviour
{

    // "scene manager"
    [SerializeField] SceneLoader sceneLoader;

    //elements UI
    [SerializeField] private Slider progressBar;
    [SerializeField] private Transform cardParent;
    [SerializeField] private GameObject cardPrefab;

    // json data
    [SerializeField] private TextAsset jsonFile;

    // score
    [SerializeField] private int scoreMax = 20;
    [SerializeField] private int scoreMin = 0;

    private CardList cardList;
    private int currentCardIndex = 0;
    private int totalCards = 0;

    private int totalScore = 0;

    void Start()
    {
        LoadData();
        SetupProgressBar();
        CreateNextCard();
    }

    private void LoadData() // charge données json
    {
        cardList = JsonUtility.FromJson<CardList>(jsonFile.text);
        totalCards = cardList.cards.Count; // recup nombre total de cartes
    }

    private void SetupProgressBar() // instantie progress bar
    {
        progressBar.minValue = 0;
        progressBar.maxValue = 20; 
        progressBar.value = 0;
    }

    public void CreateNextCard()
    {
        if (currentCardIndex >= totalCards) // si toutes les cartes sont passé --> fin du jeu
        {
            sceneLoader.ChangeScene("End3");
            return;
        }

        GameObject newCard = Instantiate(cardPrefab, cardParent, false); // instantie nouvelles carte à partir du prefabs
        SwipeCard swipe = newCard.GetComponent<SwipeCard>(); // recup comportement de SwipeCard.cs
        CardBehavior behavior = newCard.GetComponent<CardBehavior>();

        swipe.cardMoved += OnCardSwiped; // quand la carte esr swipé on appel la fonction

        var cardData = cardList.cards[currentCardIndex];

        behavior.DisplayQuestion(null, cardData.question, cardData.reponse1, cardData.reponse2); // affiche question et reponses sur la carte
    }

    private void OnCardSwiped() // fonction appelée quand la carte est swipé
    {
        if (currentCardIndex %2 == 0)
        {
            totalScore += cardList.cards[currentCardIndex].score1;
        }
        else
        {
            totalScore += cardList.cards[currentCardIndex].score2;
        }
        currentCardIndex++; // on fait +1 dans l'index
        progressBar.value = currentCardIndex; // met à jour la bar de progression

         if (totalScore >= scoreMax)
        {
            sceneLoader.ChangeScene("End2"); // jauge max atteinte
            return;
        }
        else if (totalScore <= scoreMin)
        {
            sceneLoader.ChangeScene("End"); // jauge min atteinte
            return;
        }

        CreateNextCard(); // crée la carte d'apres
    }
    
    // private void EndGame() 
    // {
    //     if (totalScore <= 9)
    //     {
    //         sceneLoader.ChangeScene("End");
    //     }
    //     else if (totalScore <=20)
    //     {
    //         sceneLoader.ChangeScene("End2");
    //     }
    //     else
    //     {
    //         sceneLoader.ChangeScene("End3");
    //     }
    // }

    public void GameStarted() // lancement de la main scene
    {
        sceneLoader.ChangeScene("MainScene");
    }
}