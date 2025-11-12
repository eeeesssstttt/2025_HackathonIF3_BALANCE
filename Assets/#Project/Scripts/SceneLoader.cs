using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    // Temporaire, pour tester le changement de scènes, je mets un input:
    [SerializeField] private InputActionAsset actions;
    private int nextSceneIndex = 0;
    private int totalScenes = 2;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
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

    // OnEnable est aussi temporaire et permet de voir qu'on change de scène:
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

    public void OnClick(InputAction.CallbackContext context)
    {
        nextSceneIndex++;
        if (nextSceneIndex > totalScenes)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);

    }
}
