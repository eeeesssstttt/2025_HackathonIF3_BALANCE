using UnityEngine;
using UnityEngine.UI;

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

    [System.Serializable]
    public class DeckCollection
    {
        public Deck[] decks;
    }

    public TextAsset textJSON;

    [SerializeField] CardBehavior cardBehavior;

   

    string CleanPath(string raw)
    {
        raw = raw.Replace("./", "");
        raw = raw.Replace(".jpg", "");
        raw = raw.Replace(".png", "");
        raw = raw.Replace(".jpeg", "");
        raw = raw.Replace(".webp", "");
        raw = raw.Replace(".avif", "");
        return raw;
    }

    Sprite LoadCardSprite(string rawPath)
    {
        string path = CleanPath(rawPath);
        Sprite sprite = Resources.Load<Sprite>(path);

        if (sprite == null)
        {
            Debug.LogError("Image not found at Resources/" + path);
        }

        return sprite;
    }

    Deck decks;
    Cards cards;
    Sprite sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DeckCollection jsonData = JsonUtility.FromJson<DeckCollection>(textJSON.text);

   

        // Looping through the outer array (decks)
        foreach (Deck deck in jsonData.decks)
        {
            // Debug.Log("ID: " + deck.id + " Title: " + deck.title);

            // Looping through the next array (cards)
            // in deck.cards -> deck is from above array deck
            foreach (Cards card in deck.cards)
            {
                // Debug.Log("ID: " + card.id + " Title: " + card.title + " Text: " + card.text);
                // Debug.Log("   Text: " + card.text);

                // Loops into accept or reject object
                // Debug.Log("Point: " + card.accept.eco);
            }
        }


        // Accessing specific items -> Title of id 1 in First Deck (Regular Card)

        // Access first deck (decks in JSON)
        decks = jsonData.decks[0];

        // Access card at index 1 (id 1) (cards in JSON)
        cards = decks.cards[1];

        Debug.Log("Card title :" + cards.text);

        sprite = LoadCardSprite(cards.image);

      
        
    }

    // Update is called once per frame
    void Update()
    {
        
        cardBehavior.DisplayQuestion(cards.text, sprite, cards.title, cards.id, cards.id);
    }
}
