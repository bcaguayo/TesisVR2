using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxMiniManager : BoxManager
{
    [SerializeField] private AudioSource tutorialOne;

    // Maximum amount of green boxes, Total amount of boxes on scene
    private int boxesTotal;

    // Start is called before the first frame update
    void Start() {
        boxesTotal = 2;
        tutorialOne.Play();
    }

    private float waitLimit, timer;
    private bool waiting = false;
    // Update is called once per frame
    void Update() {
        // Clock
        timer += Time.deltaTime;
        // Discoverd all boxes
        if (discovered >= boxesTotal) {
            // Wait for 3 seconds
            if (!waiting) {
                waiting = true;
                waitLimit = timer + 3f;
                return;
            } else {
                if (timer > waitLimit) {
                    waiting = false;
                    // Next Scene
                    ResetScene();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }            
        }
    }
    public void ResetScene() {
        score = 0;
        discovered = 0;
        boxesTotal = 2;
    }
}
