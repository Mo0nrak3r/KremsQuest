using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GrapeScript : MonoBehaviour
{
    public ScoreScript score;
    public float floatStrength;

    float minSize = 0.01f;
    float growthRate = -2.5f;
    float scale = 1f;
    bool collected = false;

    Vector3 floatY;
    float originalY;

    void Start()
    {
        var ob = GameObject.FindGameObjectWithTag("Score");
        score = ob.GetComponent<ScoreScript>();
        this.originalY = this.transform.position.y;
    }
    private void OnTriggerEnter(Collider other)
    {
        score.addGrapes(1);
        collected = true;
    }
    void Update()
    {
        floatY = transform.position;
        floatY.y = originalY + (Mathf.Sin(Time.time) * floatStrength);
        transform.position = floatY;

        if (collected)
        {
            transform.localScale = Vector3.one * scale;
            scale += growthRate * Time.deltaTime;
            if (scale < minSize)
                Destroy(gameObject);
        }
    }
}
