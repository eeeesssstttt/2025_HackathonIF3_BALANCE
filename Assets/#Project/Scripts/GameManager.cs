using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CardManager cardManager;
    [SerializeField] private TextAsset jsonFile;

    public void GameStarted()
    {
        cardManager.SetJsonFile(jsonFile);
        cardManager.StartCards();
    }
}
