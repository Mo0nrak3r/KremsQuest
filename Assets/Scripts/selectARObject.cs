using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlacementWithManySelectionController : MonoBehaviour
{

    [SerializeField]
    private Camera arCamera;

    private Vector2 touchPosition = default;

    public string sceneName;

    void Update()
    {
        // do not capture events unless the welcome panel is hidden

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            touchPosition = touch.position;

            if(touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if(Physics.Raycast(ray, out hitObject))
                {
                    SceneManager.LoadScene(sceneName);
                }
            }
        }
    }
}