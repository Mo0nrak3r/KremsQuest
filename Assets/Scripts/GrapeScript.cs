using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GrapeScript : MonoBehaviour
{
    public ScoreScript score;

    float minSize = 0.001f;
    float growthRate = -2.5f;
    float scale = 1f;
    bool collected = false;
    bool once = true;

    float deltaT;

    public ParticleSystem ps1;
    public ParticleSystem ps2;
    public GameObject mesh;

    void Start()
    {
        if (collected)
        {
            Destroy(gameObject);
            return;
        }
        var ob = GameObject.FindGameObjectWithTag("Score");
        score = ob.GetComponent<ScoreScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        deltaT = 0;
    }
    private void OnTriggerStay(Collider other)
    {
        deltaT += Time.deltaTime;
        if(deltaT > 0.5f)
            collected = true;
    }
    private void OnTriggerExit(Collider other)
    {
        deltaT = 0;
    }
    void Update()
    {
        if (collected)
        {
            transform.localScale = Vector3.one * scale;
            scale += growthRate * Time.deltaTime;
            if (scale < minSize)
            {
                if (once)
                {
                    score.addGrapes(1);
                    once = false;
                    Destroy(mesh);
                    ps1.Stop();
                    ps2.Play();
                }
                Destroy(gameObject, 2.0f);
            }
        }
    }
}
