using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider bar;
    public TextAsset jsonFile;
    private int currentIndex = 0;
    private int totalCards = 0;

    [System.Serializable]
    public class Card
    {
        public string reponse;
        public int score;
    }

    [System.Serializable]
    public class CardList
    {
        public Card[] cards;
    }

    private CardList data;

    void Start()
    {
        data = JsonUtility.FromJson<CardList>(jsonFile.text);
        totalCards = data.cards.Length;

        bar.minValue = 0;
        bar.maxValue = totalCards;
        bar.value = 0;

        SwipeCard swipe = FindAnyObjectByType<SwipeCard>();
        if (swipe != null)
            swipe.cardMoved += UpdateBar;
    }

    void UpdateBar()
    {
        currentIndex++;
        bar.value = currentIndex;
    }
}
