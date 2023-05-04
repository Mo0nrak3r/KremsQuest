using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class ARModeController : MonoBehaviour
{
    public UIDocument questViewUIDocument;
    public VisualTreeAsset popupDialogTemplate;

    private VisualElement root;

    void Start()
    {
        root = questViewUIDocument.rootVisualElement;

        // Connect button click events
        root.Q<Button>("BackButton").clicked += () => LoadTargetScene("MainMenuScene");
        root.Q<Button>("Explorer").clicked += () => LoadTargetScene("Geolocation");
        root.Q<Button>("Landmark").clicked += () => LoadTargetScene("2DPicture");
        //root.Q<Button>("ARMode").clicked += ShowNotImplementedPopup;
        //root.Q<Button>("Tours").clicked += ShowNotImplementedPopup;
        //root.Q<Button>("LeaderBoard").clicked += () => LoadTargetScene("LeaderboardScene");
        //root.Q<Button>("Settings").clicked += ShowNotImplementedPopup;
        //root.Q<Button>("Help").clicked += ShowNotImplementedPopup;
    }

    private void LoadTargetScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

   /* private void ShowNotImplementedPopup()
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
    }*/
}