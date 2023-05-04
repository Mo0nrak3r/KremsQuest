using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class BackButtonHandler : MonoBehaviour
{
    public UIDocument leaderBoardUIDocument;
    private int previousSceneIndex;

    private VisualElement root;

    void Start()
    {
        root = leaderBoardUIDocument.rootVisualElement;
        previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;

        // Connect the UI back button click event
        root.Q<Button>("BackButton").clicked += () => LoadTargetScene("MainMenuScene");
    }
    


    void Update()
    {
        // Check for back button press on Android devices
        if (Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape))
        {
            LoadTargetScene("MainMenuScene");
        }
    }

    private void LoadTargetScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
