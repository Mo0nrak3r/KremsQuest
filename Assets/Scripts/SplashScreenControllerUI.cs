using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SplashScreenControllerUI : MonoBehaviour
{
    public float delayBeforeLoading = 9.0f;

    void Start()
    {
        StartCoroutine(LoadMainScene());
    }

    IEnumerator LoadMainScene()
    {
        yield return new WaitForSeconds(delayBeforeLoading);
        SceneManager.LoadScene("SampleScene");
    }
}
