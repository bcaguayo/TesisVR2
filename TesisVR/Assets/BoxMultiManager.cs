using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BoxMultiManager : BoxManager
{
    // Room Asset, to allow rotation. Helper arrows. 
    // Two tutorial panels. One UI panel. One End instructions panel
    [SerializeField] private GameObject rig, arrows, 
                                        panel1, panel2, panel3, panel4;
    [SerializeField] private AudioSource tutorialTwo, tutorialThree, 
                                         tutorialFour, tutorialFive;

    // Boxes (for Reset)
    [SerializeField] GameObject[] boxes;
    [SerializeField] TextMeshProUGUI cdText;

    // Tutorial step
    private int part;

    // Maximum amount of green boxes, Total amount of boxes on scene
    private int maxScore, boxesTotal;   

    // Start is called before the first frame update
    void Start() {
        part = 1;
        maxScore = 5;
        boxesTotal = 25;

        // Tutorial Second Part
        ShowFirstPanel();
        PlayTutorial(1);
    }

    private float timer, waitLimit;
    private bool waiting, uiwaiting;
    // Update is called once per frame
    void Update() {        
        // Clock
        timer += Time.deltaTime;       

        if (score >= maxScore || discovered >= boxesTotal) {

            // Wait a bit | First Waiter 
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
                        // Play next tutorial
                        ShowSecondPanel();
                        PlayTutorial(2);
                        // Rotate the Room
                        rig.transform.Rotate(0f, 90f, 0f, Space.Self);
                        part = 2;                    
                    } else {
                        DisableBoxes();
                        // Play the UI tutorial
                        ShowUI();
                        PlayTutorial(3);
                        part = 3;
                        // Wait a bit more | Second Waiter 
                        uiwaiting = true;
                        waitLimit = timer + 20f;                
                    }
                }
            } else {
                if (uiwaiting) {
                    if (timer >= waitLimit) {      
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    } else if (timer >= waitLimit - 1) {
                        cdText.text = "1";   
                    } else if (timer >= waitLimit - 2) {
                        cdText.text = "2";   
                    } else if (timer >= waitLimit - 3) {
                        cdText.text = "3";
                    } else if (timer >= waitLimit - 8) {
                        // PlayTutorial(4);
                        ShowEndScreen();  
                    } else {
                        return;
                    }
                } else {
                    // First Waiter
                    waiting = true;
                    waitLimit = timer + 3f;
                }
            }                
        }
    }

    IEnumerator Waiter() {
        yield return new WaitForSeconds(4f);
    }

    public void HidePanels() {
        panel1.gameObject.SetActive(false);
        panel2.gameObject.SetActive(false);
        panel3.gameObject.SetActive(false);
        panel4.gameObject.SetActive(false);
    }

    public void ShowFirstPanel() {
        panel1.gameObject.SetActive(true);
    }

    public void ShowSecondPanel() {
        panel1.gameObject.SetActive(false);
        panel2.gameObject.SetActive(true);
    }

    public void ShowUI() {
        panel2.gameObject.SetActive(false);
        panel3.gameObject.SetActive(true);
    }

    public void ShowEndScreen() {
        panel3.gameObject.SetActive(false);
        panel4.gameObject.SetActive(true);
    }

    public void PlayTutorial(int part) {
        switch (part) {
            case 1 : tutorialTwo.Play(); break;
            case 2 : tutorialThree.Play(); break;
            case 3 : tutorialFour.Play(); break;
            case 4 : tutorialFive.Play(); break;
            default : return;
        }
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