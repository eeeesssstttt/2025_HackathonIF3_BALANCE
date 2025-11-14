using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }


    // ++++TEMPORAIRE++++ Variables temporaires pour tester le changement de scènes :
    [SerializeField] private InputActionAsset actions;
    private int nextSceneIndex = 0;
    private int totalScenes = 2;
    // ++++FIN++++


    // Dans Awake, on a le code pour faire un Singleton. 
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}