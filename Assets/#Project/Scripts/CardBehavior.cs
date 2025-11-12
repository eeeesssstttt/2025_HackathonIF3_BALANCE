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
    [SerializeField] private Image background;
    [SerializeField] private Image illustration;
    [SerializeField] private TextMeshProUGUI question;
    [SerializeField] private int questionFontSize;
    [SerializeField] private TextMeshProUGUI response1;
    [SerializeField] private TextMeshProUGUI response2;
    [SerializeField] private int responseFontSize;
    [SerializeField] private Sprite testSprite;
    [SerializeField] private string testQuestion;
    [SerializeField] private string testResponse1;
    [SerializeField] private string testResponse2;

    // private void Start()
    // {
    //     DisplayQuestion(testSprite, testQuestion, testResponse1, testResponse2);
    // }

    public void Initialize()
    {
        background.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);

        illustration.gameObject.SetActive(true);

        question.gameObject.SetActive(true);
        question.fontSize = questionFontSize;
        response1.gameObject.SetActive(true);
        response2.gameObject.SetActive(true);
        response1.fontSize = responseFontSize;
        response2.fontSize = responseFontSize;
    }

    public void DisplayQuestion(Sprite illustrationSprite, string questionText, string response1Text, string response2Text)
    {
        illustration.sprite = illustrationSprite;

        question.text = questionText;
        response1.text = response1Text;
        response2.text = response2Text;

        background.gameObject.SetActive(true);
        canvas.gameObject.SetActive(true);
    }

    public void HideQuestion()
    {
        background.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
    }
}