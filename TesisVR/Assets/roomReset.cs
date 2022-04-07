using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomReset : MonoBehaviour
{
    [SerializeField] private GameObject room;
    [SerializeField] private Texture red, yellow, green;
    [SerializeField] private AudioSource correct, incorrect;
    [SerializeField] private GameObject box1, box2, box3, box4, box5,
                                    box6, box7, box8, box9, box10,
                                    box11, box12, box13, box14, box15,
                                    box16, box17, box18, box19, box20,
                                    box21, box22, box23, box24, box25;
    [SerializeField] private GameObject playerTracker;

    /* Rounds measures how many loops completed
    roundScore measures the amount of green boxes touched
    roundErrors measures the amount of green boxes touched
    prevDegree is used to rotate the room
    */
    private int rounds, roundScore, roundErrors, prevDegree;
    /* roundTimer measures seconds passed
    roundDistance measures distance traveled in meters 
    prevPos stores the latest position
    */
    private float roundTimer, roundDistance;
    private Vector2 prevPos = new Vector2(-10.0f, -10.0f);

    // Boxes stores the GameObjects
    private GameObject[] boxes;

    /*  BoxRotation is a jagged array that contains the indexes of the picked boxes 
        for each 5-number configuration on: 0°, 90°, 180, 270°
        Access a Config with boxRotation[degree / 90][number space] */
    private int[][] boxRotation = new int[4][];


    // Discovered stores which boxes have been discovered
    private HashSet<string> discovered = new HashSet<string>();

    void Awake()
    {
        // Add box objects to array for Resets
        boxes = new GameObject[] {box1, box2, box3, box4, box5,
                                  box6, box7, box8, box9, box10,
                                  box11, box12, box13, box14, box15,
                                  box16, box17, box18, box19, box20,
                                  box21, box22, box23, box24, box25};
        
        // Set the box vectors according to 0 degrees
        BoxConfig.Init();
    }

    void Start() {
        // Choose 5 random boxes
        int[] randomBoxes = new int[5];
        int added = 0;

        while (added < 5) {
            int r = Random.Range(1, 25);
            bool repeat = false;
            foreach (int i in randomBoxes) {
                if (r == i) {
                    repeat = true;
                }
            }
            if (repeat) continue;
            else {
                randomBoxes[added] = r;
                added++;
            }
        }

        // Start at Round 1
        rounds = 1;

        // Box Configuration
        SetupBoxes(randomBoxes);
        SetBoxes(0);
    }

    // For spacing rounds
    private bool waiting;
    private float waitLimit = -1f;
    // Update is called once per frame

    void DisableBoxes() {
        foreach (GameObject box in boxes) {
            discovered.Add(box.name);
        }
    }

    void Update()
    {
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
        if (rounds > 10) {
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
                    waitLimit = roundTimer + 5f;
                } else {
                    waitLimit = roundTimer + 3.5f;
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

        // Clear the discovered array
        discovered = new HashSet<string>();

        // Reset every box
        foreach(GameObject box in boxes) {
            // 1. Turn yellow
            Renderer renderer = box.GetComponent<Renderer>();
            renderer.material.mainTexture = yellow;
            // 2. Set not chosen
            BoxCollision b = box.GetComponent<BoxCollision>();
            b.changePick(false);
        }        

        // Pick a rotation degree
        int degree = Random.Range(0, 4) * 90;
        // if no rotation, pick a new Random
        if (degree == prevDegree) {
            degree += Random.Range(1, 4) * 90;
        }
        // Crop over angle
        if (degree > 270) degree -= 360;
        // Rotate the Room
        RotateRoom(degree);
        // Pick the Boxes
        SetBoxes(degree);
    }

    // Rotates the Room
    void RotateRoom(int degree) {
        // Degree is with respect to 0
        // Rotates by the difference with the prev rotation
        int dif = degree - prevDegree;
        room.transform.Rotate(0f, dif, 0f, Space.Self);
        prevDegree = degree;
    }

    // Changes Box setup
    void pickBox(GameObject box) {
        box.GetComponent<BoxCollision>().changePick(true);
    }

    private void SetupBoxes(int[] indexSet) {
        // [0] : 0 degrees, [1] : 90 degrees.
        // [2] : 180 degrees, [3] : 270 degrees.
        boxRotation[0] = indexSet;
        boxRotation[1] = BoxConfig.rotateCounterClockwise(indexSet);
        boxRotation[2] = BoxConfig.flip(indexSet);
        boxRotation[3] = BoxConfig.rotateClockwise(indexSet);
    }

    // Sets which Boxes are chosen based on degree of rotation
    private void SetBoxes(int degree) {
        // Invalid degree
        if (degree != 0 && degree != 90 && degree != 180 && degree != 270) {
            throw new System.Exception("Wrong Rotation Degree: SetBoxes");
        }

        // Set list of selected boxes based on rotation
        int[] degreeList = new int[5];        
        switch(degree) {
            case 0 : 
                degreeList = boxRotation[0];
                break;
            case 90 :
                degreeList = boxRotation[1];
                break;
            case 180 :
                degreeList = boxRotation[2];
                break;
            case 270 :
                degreeList = boxRotation[3];
                break;
        }

        // Set box to marked
        foreach (int i in degreeList)
        {
            // Index in List is box number - 1
            pickBox(boxes[i - 1]);
        }    
    }

    // Detects controller collision with Box
    public void Collision(string collider, GameObject box, bool pick) {      
        if (collider == "Controller (left)" || collider == "Controller (right)") { 
            // Get the box's renderer
            Renderer boxRenderer = box.GetComponent<Renderer>(); 
            // Not discovered yet
            // string boxNumber = box.name.Substring(5, box.name.Length - 1);
            if (!discovered.Contains(box.name)) {
                // If green (prize) box
                if (pick) {                    
                    roundScore++;
                    Organizer.Success();
                    boxRenderer.material.mainTexture = green;   
                    correct.Play(); 
                } 
                // If red (no prize) box
                else {
                    roundErrors++;
                    Organizer.Error();
                    boxRenderer.material.mainTexture = red;
                    incorrect.Play();
                }
                // add to discovered
                discovered.Add(box.name);
            }                
        }
    }
}
