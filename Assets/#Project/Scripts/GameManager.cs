using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int playedCards = 0;
    [SerializeField] SceneLoader sceneLoader;
    // Juste pour l'exemple. On pourra ajuster le nombre de cartes, idéalement on passerait cette information dans l'inspecteur.
    [SerializeField] private int MaxCardsToPlay = 10;

    // Ici, il nous faudra un lien avec le bouton START de la scène de démarrage. Il peut simplement appeler cette fonction.
    public void GameStarted()
    {
        sceneLoader.ChangeScene("TestGame");
    }

    public void CardPlayed()
    {
        playedCards++;
        if (playedCards >= MaxCardsToPlay)
        {
            GameEnded();
        }
    }

    private void GameEnded()
    {
        sceneLoader.ChangeScene("TestEnd");
    }

}