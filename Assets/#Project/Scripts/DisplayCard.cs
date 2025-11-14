using System.Collections.Generic;
using UnityEngine;

// Gère l'affichage des cartes à partir du JSON
public class CardDisplayManager : MonoBehaviour
{
    [Header("Cards Setup")]
    [SerializeField] private CardBehavior currentCard;
    [SerializeField] private CardBehavior nextCard;

    [Header("JSON File")]
    [SerializeField] private TextAsset jsonFile;

    private CardList cardList;
    private int currentCardIndex = 0;
    private int totalCards = 0;

    private void Start()
    {
        LoadData();
        PrepareFirstCards();
    }

    // Charge les données JSON
    private void LoadData()
    {
        cardList = JsonUtility.FromJson<CardList>(jsonFile.text);
        totalCards = cardList.cards.Count;
    }

    // Prépare les deux premières cartes visibles
    private void PrepareFirstCards()
    {
        if (totalCards == 0) return;

        // Carte actuelle
        DisplayCard(currentCard, currentCardIndex);

        // Carte suivante (en boucle infinie)
        int nextIndex = (currentCardIndex + 1) % totalCards;
        DisplayCard(nextCard, nextIndex);
    }

    // Affiche une carte avec les données
    private void DisplayCard(CardBehavior card, int index)
    {
        var cardData = cardList.cards[index];
        card.DisplayQuestion(cardData.question, cardData.question, cardData.reponse1, cardData.reponse2);
        card.gameObject.SetActive(true);
    }

    // Appelée pour passer à la carte suivante
    public void ShowNextCard()
    {
        // Incrémente l’index et boucle si nécessaire
        currentCardIndex = (currentCardIndex + 1) % totalCards;

        // La carte actuelle devient la suivante
        CardBehavior temp = currentCard;
        currentCard = nextCard;
        nextCard = temp;

        // Affiche la prochaine carte dans "nextCard"
        int nextIndex = (currentCardIndex + 1) % totalCards;
        DisplayCard(nextCard, nextIndex);
    }
}
