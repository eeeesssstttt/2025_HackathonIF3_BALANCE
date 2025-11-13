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


    // ++++TEMPORAIRE++++ OnEnable est aussi temporaire et permet de voir qu'on change de scène:
    void OnEnable()
    {
        actions.FindActionMap("Player").Enable();
        actions.FindActionMap("Player").FindAction("ChangeScene").performed += OnClick;
    }
    // void OnDisable()
    // {
    //     actions.FindActionMap("Player").Disable();
    //     actions.FindActionMap("Player").FindAction("ChangeScene").performed -= OnClick;
    //     Debug.Log("OnDisabled activated");
    // }
    // // OnClick est temporaire. Voir fonction ChangeScene() pour la version définitive.
    public void OnClick(InputAction.CallbackContext context)
    {
        nextSceneIndex++;
        if (nextSceneIndex > totalScenes)
        {
            // Ici aussi temporaire, relance la première scène (Attention ! Il faudra faire en sorte de ne pas rappeler le SceneLoader; à voir avec le GameInitializer).
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    // ++++FIN++++


    // Au lieu de OnClick, on aurait la fonction suivante, appelée par le gameManager :
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}