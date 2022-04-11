using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoxMultiManager : BoxManager
{
    // Room Asset, to allow rotation. Helper arrows. 
    // Two tutorial panels. One UI panel
    [SerializeField] private GameObject rig, arrows, panel1, panel2, panel3;

    // Boxes (for Reset)
    [SerializeField] GameObject[] boxes;

    // Tutorial step
    private int part;

    // Maximum amount of green boxes, Total amount of boxes on scene
    private int maxScore, boxesTotal;   

    // Start is called before the first frame update
    void Start() {
        part = 1;
        maxScore = 5;
        boxesTotal = 25;
        ShowFirstPanel();
    }

    private float timer, waitLimit;
    private bool waiting;
    // Update is called once per frame
    void Update() {        
        // Clock
        timer += Time.deltaTime;       

        if (score >= maxScore || discovered >= boxesTotal) {

            // Wait a bit
            if (waiting) {
                if (timer >= waitLimit) {
                    waitLimit = -1f;
                    waiting = false;

                    if (part == 1) {
                        // Reset the Room
                        ResetScene();
                        // Return to yellow boxes
                        ResetBoxes();
                        // Hide the Hint Arrows
                        HideArrows();
                        // Show next tutorial
                        ShowSecondPanel();
                        // Rotate the Room
                        rig.transform.Rotate(0f, 90f, 0f, Space.Self);
                        part = 2;
                    } else if (part == 2) {

                        // Show the UI tutorial
                        ShowUI();
                        // Wait a bit more
                    } else {
                        // Application.Quit();
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                }
            } else {
                waiting = true;
                waitLimit = timer + 3f;
                return;
            }           

            if (part == 1) {
                // Reset the Room
                ResetScene();
                // Return to yellow boxes
                ResetBoxes();
                // Hide the Hint Arrows
                HideArrows();
                // Show next tutorial
                ShowSecondPanel();
                // Rotate the Room
                rig.transform.Rotate(0f, 90f, 0f, Space.Self);
                part = 2;
            } else if (part == 2) {

                // Show the UI tutorial
                ShowUI();
                // Wait a bit more
            } else {
                Application.Quit();
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }                  
        }
    }

    IEnumerator Waiter() {
        yield return new WaitForSeconds(4f);
    }

    public void ShowFirstPanel() {
        panel1.gameObject.SetActive(true);        
        panel2.gameObject.SetActive(false);      
        panel3.gameObject.SetActive(false);
    }

    public void ShowSecondPanel() {
        panel1.gameObject.SetActive(false);
        panel2.gameObject.SetActive(true);
    }

    public void ShowUI() {
        panel1.gameObject.SetActive(false);
        panel2.gameObject.SetActive(false);
        panel3.gameObject.SetActive(true);
    }

    public void ResetScene() {
        score = 0;
        discovered = 0;
        maxScore = 5;
        boxesTotal = 25;
    }

    public void ResetBoxes() {
        foreach (GameObject box in boxes) {
            box.GetComponent<MiniCollision>().Reset();
        }
    }

    public void DisableBoxes() {
        foreach (GameObject box in boxes) {
            box.GetComponent<MiniCollision>().Discover();
        }
    }

    public void HideArrows() {
        arrows.gameObject.SetActive(false);
    }
}