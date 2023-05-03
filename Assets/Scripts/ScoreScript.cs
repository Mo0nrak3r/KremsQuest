using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public static int numberOfGrapes = 0;
    public Text grapeBalanceText;

    void Start()
    {
        grapeBalanceText.text = numberOfGrapes.ToString();
    }

    public void addGrapes(int ammount)
    {
        numberOfGrapes += ammount;
        grapeBalanceText.text = numberOfGrapes.ToString();
    }
}