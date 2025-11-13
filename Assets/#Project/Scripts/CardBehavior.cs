using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardBehavior : MonoBehaviour
{
    // The card knows its GameManager.
    // [SerializeField] private GameManager gameManager;

    // The card has a canvas to display text.
    [SerializeField] private Canvas canvas;

    // The card knows its contents : a question, the corresponding image and both possible answers.
    // [SerializeField] private Image background;
    [SerializeField] private Image image;

    [SerializeField] private TextMeshProUGUI titre;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI accept;
    [SerializeField] private TextMeshProUGUI reject;

    // private void Start()
    // {
    //     DisplayQuestion(testSprite, testQuestion, testResponse1, testResponse2);
    // }

    public void Initialize()
    {
        canvas.gameObject.SetActive(false);
        // background.gameObject.SetActive(true);
        image.gameObject.SetActive(true);
        titre.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        accept.gameObject.SetActive(true);
        reject.gameObject.SetActive(true);
    }

    // public void DisplayQuestion(Sprite illustrationSprite, string questionText, string response1Text, string response2Text)
    public void DisplayQuestion(string questionText, string textText, string response1Text, string response2Text)
    {
        // illustration.sprite = illustrationSprite;

        titre.text = questionText;
        text.text = textText;
        accept.text = response1Text;
        reject.text = response2Text;

        // background.gameObject.SetActive(true);
        canvas.gameObject.SetActive(true);
    }

    public void HideQuestion()
    {
        // background.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
    }
}