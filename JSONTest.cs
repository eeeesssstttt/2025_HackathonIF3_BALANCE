using UnityEngine;

public class JSONTest : MonoBehaviour
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

    public class Card
    {
        public string id;
        public string category;
        public string title;
        public string image;
        public string text;
        public Choice accept;
        public Choice reject;
    }

    public class Deck
    {
        public string id;
        public string title;
        public Card[] cards;
    }

      public TextAsset textJSON;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Deck jsonData = JsonUtility.FromJson<Deck>(textJSON.text);

        Debug.Log("Title: " + jsonData.title);

         foreach (Card item in jsonData.cards)
        {
            Debug.Log("Category: " + item.category + ", Title: " + item.title + "Description:" + item.text);

            Debug.Log("Result:" + item.accept.eco);
             Debug.Log("Result:" + item.reject.population);
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
