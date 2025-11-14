using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData
{
    public string question;
    public string reponse1;
    public string reponse2;
    public int score1;
    public int score2;
}

[System.Serializable]
public class CardList
{
    public List<CardData> cards;
}

public class CardManager : MonoBehaviour
{
    [SerializeField] private CardBehavior currentCard;
    [SerializeField] private CardBehavior nextCard;

    private CardList cardList;
    private int currentCardIndex = 0;

    private TextAsset jsonFile;

    public void SetJsonFile(TextAsset json)
    {
        jsonFile = json;
    }

    public void StartCards()
    {
        if (jsonFile == null)
        {
            Debug.LogError("Aucun JSON assigné à CardManager !");
            return;
        }

        LoadData();
        PrepareFirstCards();
    }

    private void LoadData()
    {
        cardList = JsonUtility.FromJson<CardList>(jsonFile.text);
        if (cardList.cards.Count == 0)
        {
            Debug.LogError("Aucune carte dans le JSON !");
        }
    }

    private void PrepareFirstCards()
    {
        // Affiche la première carte
        currentCard.DisplayQuestion(
            cardList.cards[0].question,
            cardList.cards[0].question,
            cardList.cards[0].reponse1,
            cardList.cards[0].reponse2
        );
        currentCard.gameObject.SetActive(true);

        // Affiche la carte suivante si elle existe
        if (cardList.cards.Count > 1)
        {
            nextCard.DisplayQuestion(
                cardList.cards[1].question,
                cardList.cards[1].question,
                cardList.cards[1].reponse1,
                cardList.cards[1].reponse2
            );
            nextCard.gameObject.SetActive(true);
        }

        // Abonne la carte actuelle à l’événement cardMoved
        currentCard.cardMoved += OnCardSwiped;
    }

    private void OnCardSwiped()
    {
        // Passe à la carte suivante
        currentCardIndex++;

        if (currentCardIndex >= cardList.cards.Count)
        {
            Debug.Log("Fin du jeu !");
            return;
        }

        currentCard.DisplayQuestion(
            cardList.cards[currentCardIndex].question,
            cardList.cards[currentCardIndex].question,
            cardList.cards[currentCardIndex].reponse1,
            cardList.cards[currentCardIndex].reponse2
        );
    }
}
