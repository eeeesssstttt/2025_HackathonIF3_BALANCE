using UnityEngine;

public class JSONReader : MonoBehaviour
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
        public string text;
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
    DeckCollection jsonData;

    [SerializeField] CardBehavior cardBehavior;

    Deck decks;
    Cards cards;

    public class Card
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string Accept { get; set; }
        public string Reject { get; set; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jsonData = JsonUtility.FromJson<DeckCollection>(textJSON.text);

        // Looping through the outer array (decks)
        foreach (Deck deck in jsonData.decks)
        {
            Debug.Log("ID: " + deck.id + " Title: " + deck.title);

            // Looping through the next array (cards)
            // in deck.cards -> deck is from above array deck
            foreach (Cards card in deck.cards)
            {
                Debug.Log("ID: " + card.id + " Title: " + card.title + " Text: " + card.text);
                Debug.Log("   Text: " + card.text);

                // Loops into accept or reject object
                Debug.Log("Point: " + card.accept.eco);
            }
        }


        // Accessing specific items -> Title of id 1 in First Deck (Regular Card)

        // Access first deck (decks in JSON)
        decks = jsonData.decks[0];

        // Access card at index 1 (id 1) (cards in JSON)
        cards = decks.cards[0];

        Debug.Log("Card title :" + cards.text);
    }

    // Update is called once per frame
    // void Update()
    // {
    //     cardBehavior.DisplayQuestion(cards.title, cards.text, cards.id, cards.id);
    // }

    public void GetCard(int cardIndex)
    {
        Card card = new Card();
        card.Id = jsonData.decks[0].cards[cardIndex].id;
        card.Category = jsonData.decks[0].cards[cardIndex].category;
        card.Title = jsonData.decks[0].cards[cardIndex].title;
        card.Text = jsonData.decks[0].cards[cardIndex].text;
        card.Image = jsonData.decks[0].cards[cardIndex].image;
        // card.Accept = jsonData.decks[0].cards[cardIndex].accept;
        // card.Reject = jsonData.decks[0].cards[cardIndex].reject;
    }
}
