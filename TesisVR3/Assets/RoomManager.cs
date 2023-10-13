using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public Texture red, yellow, green;
    public AudioSource correct, incorrect;
    public GameObject playerTracker, rig, prefab;

    /* Rounds measures how many loops completed
    roundScore measures the amount of green boxes touched
    roundErrors measures the amount of green boxes touched
    prevDegree is used to rotate the room
    */
    private int roundScore, roundErrors, prevDegree;
    // Start at Round 1
    private int rounds = 1;

    /*  ITEMS PICKED FROM MENU
        roundLimit is Number of Rounds from Slider
        boxesCount is 3x3, 4x4, 5x5
        boxConfig contains the picked boxes
    */
    private int roundLimit;
    private int boxesCount;
    // config.Length = Number of boxes picked
    private int[] boxConfig;
    private string subjectName;

    /* roundTimer measures seconds passed
    roundDistance measures distance traveled in meters 
    prevPos stores the latest position
    */
    private float roundTimer, roundDistance;
    private Vector2 prevPos = new Vector2(-10.0f, -10.0f);

    // Boxes stores the GameObjects
    private GameObject[] boxes;    

    // Discovered stores which boxes have been discovered
    private HashSet<string> discovered = new HashSet<string>();

    void Start() {
        JsonManager.Read();
        // BOX SETUP
        if (JsonManager.Instance == null) {
            // Default Values
            // Debug.Log("Null Config");
            roundLimit = 10;
            boxesCount = 25;
            boxConfig = new int[]{4, 7, 15, 18, 21};
            subjectName = "";
        } else {
            // Debug.Log("Config: " + subjectName);
            // Values from Menu
            roundLimit = JsonManager.GetRounds();
            boxesCount = JsonManager.GetCount();
            // Get box configuration from the menu settings
            boxConfig = JsonManager.GetConfig();
            // Get name of subject to pass onto Excell
            subjectName = JsonManager.GetName();
        }           

        // Spawn Boxes
        boxes = new GameObject[boxesCount];
        spawnBoxes();

        // Set up Picked Boxes
        foreach (int i in boxConfig) {
            // Index in List is box number - 1
            boxes[i - 1].GetComponent<BoxCollision>().Pick();
        }

        // Pass to Excell
        Organizer.Set(boxConfig, subjectName);
    }


    // Coordinate Mayhem
    private void spawnBoxes() {
        /* Locations. Standard Y: 0.18
        
        5 X 5 
        X: 1, 0.25, -0.5, -1.25, -2 | 1 -2
        Z: -1, -1.75, -2.5, -3.25, -4 | -1 -4
        
        4 X 4
        X: 1, 0, -1, -2
        Z: -1, -2, -3, -4
        
        3 X 3
        X: 0.5, -0.5, -1.5
        z: -1.5, -2.5, -3.5
        */

        float startX, difX, stdY, startZ, difZ, steps;
        startX = difX = startZ = difZ = steps = 0f;
        stdY = 0.18f;

        switch (boxesCount) {
            case 9 : 
                startX = -1.5f;              
                startZ = -3.5f;
                difX = difZ = 1f;  
                steps = 3f;
                break;
                
            case 16 : 
                startX = -2f;              
                startZ = -4f;
                difX = difZ = 1f;  
                steps = 4f;
                break;

            default :  // CASE 25
                startX = -2f;                
                startZ = -4f;
                difX = difZ = 0.75f;
                steps = 5f;
                break;
        }

        int index = 0;
        float endX = startX + steps * difX;
        float endZ = startZ + steps * difZ;
        for (float x = startX; x < endX; x += difX) { // X Loop
            for (float z = startZ; z < endZ; z += difZ) { // Z Loop
                GameObject box = Instantiate(prefab, 
                                 new Vector3(x, stdY, z), Quaternion.identity);
                box.GetComponent<BoxCollision>().Manager = this;
                boxes[index] = box;
                index++;
            }            
        }         
    }

    // For spacing rounds and patch
    private bool controlEnable = false;
    private bool waiting;
    private float waitLimit = -1f;

    // Update is called once per frame
    void Update() {
        // Timer
        roundTimer += Time.deltaTime;

        // Distance
        Vector2 currPos = new Vector2(playerTracker.transform.position.x, 
                                      playerTracker.transform.position.z);
        if (!prevPos.Equals(new Vector2(-10.0f, -10.0f))) {
            roundDistance += Vector2.Distance(currPos, prevPos);
        }
        prevPos = currPos;

        // Round limit is reached
        if (rounds > roundLimit) {
            // Black end screen
            Organizer.End();
            DisableBoxes();

            if (waiting) {
                if (roundTimer >= waitLimit) {
                    /// Quit to Menu
                    // SceneManager.LoadScene(0);      
                    Application.Quit();
                }
            }
            // Not waiting, set to wait and the limit to 2
            // Using float Timer (ftimer) as base
            else
            {
                waiting = true;
                // Time between END screen and Quit
                waitLimit = roundTimer + 5f;
            }            
        }

        // Disable Boxes for two seconds
        if (rounds == 1 && !controlEnable) {
            DisableBoxes();
            if (waiting) {
                if (roundTimer >= waitLimit) {
                    ResetBoxes();      
                }
            }
            // Not waiting, set to wait and the limit to 3
            // Using float Timer (ftimer) as base
            else
            {
                waiting = true;
                waitLimit = roundTimer + 5f;
            } 
        }

        // When picked boxes are found
        if (roundScore >= boxConfig.Length) {
            // Wait 2 seconds before reset
            if (waiting) {
                if (roundTimer >= waitLimit) {
                    /// WAIT OVER, RESET          
                    waitLimit = -1;  // reset waiters
                    waiting = false;

                    Organizer.Write(roundTimer, roundDistance, roundErrors);                    
                    Reset();
                }
            }

            // Not waiting, set to wait and the limit to 2
            // Using float Timer (ftimer) as base
            else
            {
                waiting = true;
                if (rounds == 1) {
                    // More time for Round 1
                    waitLimit = roundTimer + 12f;
                } else if (rounds > 5) {
                    // Less time for later rounds
                    waitLimit = roundTimer + 3f;
                } else {
                    // Standard time (Rounds 2 to 5)
                    waitLimit = roundTimer + 7f;
                }
            }
        }
    }
    
    // Once per Loop, resets the state of the room
    void Reset() {
        // Per round values
        rounds++;
        Organizer.Round(); // Display one more round, reset errors
        roundScore = 0;
        roundErrors = 0;
        roundTimer = 0f;
        roundDistance = 0f;
        prevPos = new Vector2(-10.0f, -10.0f); 

        // Rotate Camera Rig
        ResetRoom();      

        // Reset Boxes to yellow
        ResetBoxes();
    }

    void ResetRoom() {
        // Pick a new degree by addying to prevDegree
        // + 90° / + 180° / + 270°
        int newDegree = prevDegree + Random.Range(1, 4) * 90;

        // Correct overflow
        if (newDegree > 270) newDegree -= 360;

        // Rotate the Room (Camera Rig) by the difference with the prev rotation
        // rotationDegree = newDegree - prevDegree;
        rig.transform.Rotate(0f, newDegree - prevDegree, 0f, Space.Self);

        // Update degree
        prevDegree = newDegree;
    }

    void ResetBoxes() {
        // Reset every box
        foreach(GameObject box in boxes) {
            box.GetComponent<BoxCollision>().Reset();
        }
    }

    void DisableBoxes() {
        // Set boxes to discovered so they won't change color
        foreach (GameObject box in boxes) {
            box.GetComponent<BoxCollision>().Discover();
        }
    }

    // Toggle Color and Sounds for Green
    public void Correct() {
        roundScore++;
        Organizer.Success(); 
        correct.Play(); 
    }    

    // Toggle Color and Sounds for Red
    public void Error(){
        roundErrors++;
        Organizer.Error();
        incorrect.Play();
    }

    // Helper for Texture
    public Texture getTexture(string color) {
        switch (color)
        {
            case "red" : return red;
            case "green" : return green;
            default : return yellow;
        }
    }   
}