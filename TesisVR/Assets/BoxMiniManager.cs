using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxMiniManager : BoxManager
{
    // Maximum amount of green boxes, Total amount of boxes on scene
    private int maxScore, boxesTotal;  

    // Start is called before the first frame update
    void Start() {
        maxScore = 2;
        boxesTotal = 2;
    }

    // Update is called once per frame
    void Update() {
        if (score >= maxScore || discovered >= boxesTotal) {
            ResetScene();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void ResetScene() {
        score = 0;
        discovered = 0;
        maxScore = 2;
        boxesTotal = 2;
    }
}
