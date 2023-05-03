using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GetStartedBtnClick : MonoBehaviour
{
    UIDocument document;
    Button btnClick;
    private void OnEnable()
    {
        document = GetComponent<UIDocument>();

        if (document == null )
        {
            Debug.LogError("No document found");
        }

        btnClick = document.rootVisualElement.Q("TestButton") as Button;

        if ( btnClick != null )
        {
            Debug.Log("Button Found");
        }

        btnClick.RegisterCallback<ClickEvent>(OnButtonClick);
    }

    public void OnButtonClick(ClickEvent evt)
    {
        Debug.Log("Button is clicked");
    }

}
