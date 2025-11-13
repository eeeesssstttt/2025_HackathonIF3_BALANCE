using UnityEngine;

public class testJSON : MonoBehaviour
{

    [System.Serializable]

    public class Choice
    {
        public int eco;
        public int pouvoir;
        public int population;
        public int sante;
    }

    [System.Serializable]

    public class Cards
    {
        public string id;
        public string category;
        public string title;
        public string image;
        public Choice accept;
        public Choice reject;
    }

    [System.Serializable]
    public class Deck
    {
        public string id;
        public string title;
        public Cards[] cards;
    }

    public class DeckCollection
    {
        public Deck[] decks;
    }

    public TextAsset textJSON;

    [SerializeField] CardBehavior cardBehavior;

    Deck decks;
    Cards cards;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DeckCollection jsonData = JsonUtility.FromJson<DeckCollection>(textJSON.text);

        // Looping through the outer array (decks)
        foreach (Deck deck in jsonData.decks)
        {
            Debug.Log("ID: " + deck.id + " Title: " + deck.title);

            // Looping through the next array (cards)
            // in deck.cards -> deck is from above array deck
            foreach (Cards card in deck.cards)
            {
                Debug.Log("ID: " + card.id + " Title: " + card.title);

                // Loops into accept or reject object
                Debug.Log("Point: " + card.accept.eco);
            }
        }


        // Accessing specific items -> Title of id 1 in First Deck (Regular Card)

        // Access first deck (decks in JSON)
        decks = jsonData.decks[0];

        // Access card at index 1 (id 1) (cards in JSON)
        cards = decks.cards[1];

        Debug.Log("Card title :" + cards.title);
    }

    // Update is called once per frame
    void Update()
    {
        cardBehavior.DisplayQuestion(cards.title, cards.title, cards.title);
    }
}
