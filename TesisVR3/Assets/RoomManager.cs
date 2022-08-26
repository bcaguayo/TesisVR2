using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private Texture red, yellow, green;
    [SerializeField] private AudioSource correct, incorrect;
    [SerializeField] private GameObject box1, box2, box3, box4, box5,
                                    box6, box7, box8, box9, box10,
                                    box11, box12, box13, box14, box15,
                                    box16, box17, box18, box19, box20,
                                    box21, box22, box23, box24, box25;
    [SerializeField] private GameObject playerTracker, rig;

    /* Rounds measures how many loops completed
    roundScore measures the amount of green boxes touched
    roundErrors measures the amount of green boxes touched
    prevDegree is used to rotate the room
    */
    private int rounds, roundScore, roundErrors, prevDegree;

    /*  Items picked from Menu
        roundLimit is Number of Rounds from Slider
        boxesCount is 3x3, 4x4, 5x5
    */
    private int roundLimit;
    private int boxesCount;

    // BoxConfig contains the picked boxes
    // config.Length = Number of boxes picked
    private int[] boxConfig;

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

    void Awake()
    {
        // WIP: Spawn Boxes
        // Add box objects to array for Resets
        boxes = new GameObject[] {box1, box2, box3, box4, box5,
                                  box6, box7, box8, box9, box10,
                                  box11, box12, box13, box14, box15,
                                  box16, box17, box18, box19, box20,
                                  box21, box22, box23, box24, box25};
                                  // Add to list in order
        // Start at Round 1
        rounds = 1;
    }

    void Start() {
        // Set Defaults from Menu
        roundLimit = BoxConfig.Instance.GetRounds();
        boxesCount = BoxConfig.Instance.GetCount();
        // Get box configuration from the menu settings
        boxConfig = BoxConfig.Instance.GetConfig();       
        
        // Set up Box Configuration
        foreach (int i in boxConfig) {
            // Index in List is box number - 1
            boxes[i - 1].GetComponent<BoxCollision>().Pick();
        }
    }

    // For spacing rounds
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
                    /// WAIT OVER, QUIT          
                    Application.Quit(); // Quit
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

        // When 5 boxes are found
        if (roundScore >= 5) {
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
                    // Standard time (Rounds 2 to 4)
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

    public void Correct() {
        roundScore++;
        Organizer.Success(); 
        correct.Play(); 
    }    

    public void Error(){
        roundErrors++;
        Organizer.Error();
        incorrect.Play();
    }

    public Texture getTexture(string color) {
        switch (color)
        {
            case "red" : return red;
            case "green" : return green;
            default : return yellow;
        }
    }   
}