using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public UIDocument mainMenuUIDocument;
    public VisualTreeAsset popupDialogTemplate;

    private VisualElement root;

    void Start()
    {
        root = mainMenuUIDocument.rootVisualElement;

        // Connect button click events
        root.Q<Button>("QuestGames").clicked += () => LoadTargetScene("QuestViewScene");
        root.Q<Button>("ARMode").clicked += () => LoadTargetScene("Geolocation"); 
        //root.Q<Button>("ARMode").clicked += ShowNotImplementedPopup;
        root.Q<Button>("Tours").clicked += ShowNotImplementedPopup;
        root.Q<Button>("LeaderBoard").clicked += ShowNotImplementedPopup;
        root.Q<Button>("Settings").clicked += ShowNotImplementedPopup;
        root.Q<Button>("Help").clicked += ShowNotImplementedPopup;
    }

    private void LoadTargetScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void ShowNotImplementedPopup()
    {
        var popupDialog = popupDialogTemplate.CloneTree().Q("PopupDialog");
        root.Add(popupDialog);

        // Center the popup on the screen
        popupDialog.style.left = (root.layout.width - popupDialog.layout.width) / 2;
        popupDialog.style.top = (root.layout.height - popupDialog.layout.height) / 2;

        // Connect close button click event
        popupDialog.Q<Button>("CloseButton").clicked += () => {
            root.Remove(popupDialog);
        };
    }
}