using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// FIX BOX ROTATION WITH BOX CONFIG


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

    // TODO Configurations
                                            /*
        int[,] configA = {{3, 6, 15, 17, 24}, {15, 04, 23, 07, 16}, 
                      {23, 20, 11, 09, 02}, {11, 22, 03, 19, 10}};
        int[,] configB = {{3, 6, 15, 17, 24}, {15, 04, 23, 07, 16},
                      {23, 20, 11, 09, 02}, {11, 22, 03, 19, 10}};
                      

    int[] numbers = {3, 6, 15, 17, 24};
    LinkedList<HashSet<int>>[] configA = new LinkedList<HashSet<int>>[4];
    new HashSet<int>().Add(3);

    
    // configA[0] = new HashSet<int>(numbers); 
    private HashSet<int>[] configB = new HashSet<int>[4];
    private HashSet<int>[] configC = new HashSet<int>[4];
    */

    /* Rounds measures how many loops completed
    roundScore measures the amount of green boxes touched
    roundErrors measures the amount of green boxes touched
    prevDegree is used to rotate the room
    
    // 
    */
    private int rounds, roundScore, roundErrors, prevDegree;
    /* roundTimer measures seconds passed
    roundDistance measures distance traveled in meters 
    
    */
    private float roundTimer, roundDistance;
    private Vector2 prevPos = new Vector2(-10.0f, -10.0f);

    // Boxes stores the GameObjects
    private GameObject[] boxes;

    // at 90, 180 and 270 degrees
    private int[] list0 = new int[5];
    private int[] list90 = new int[5];
    private int[] list180 = new int[5];
    private int[] list270 = new int[5];

    // WIP local[degree / 90][config numbers] 
    // instead of list0, list90... list[][]
    int[][] boxRotation = new int[4][];

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
        // Set the boxes according to 0 degrees
        BoxConfig.Init();

        // Choose 5 random boxes
        int[] randomBoxes = new int[5];
        int added = 0;

        while (added < 5) {
            int r = Random.Range(1, 25);
            foreach (int i in randomBoxes) {
                if (i == r) {
                    continue;
                }
            }
            randomBoxes[added] = r;
            added++;
        }

        // Box Configuration

        setupBoxesZ(randomBoxes);
        setBoxes(0);
        
        // ----------------- on excel
        Typewriter.Reset();
    }

    void Start() {}

    // For spacing rounds
    private bool waiting;
    private float waitLimit = -1f;
    // Update is called once per frame
    void Update()
    {
        // Clock
        roundTimer += Time.deltaTime;

        // Distance
        Vector2 currPos = new Vector2(playerTracker.transform.position.x, 
                                      playerTracker.transform.position.z);
        if (!prevPos.Equals(new Vector2(-10.0f, -10.0f))) {
            roundDistance += Vector2.Distance(currPos, prevPos);
        }
        prevPos = currPos;
        // ftimer = (int)(ftimer * 100) / 100;

        // Round limit is reached
        if (rounds >= 10) {
            // TODO
            Application.Quit();
        }

        // When 5 boxes are found
        if (roundScore >= 5) {
            // Wait 2 seconds before reset
            if (waiting) {
                if (roundTimer >= waitLimit) {
                    /// WAIT OVER, RESET          
                    waitLimit = -1;  // reset waiters
                    waiting = false;

                    Typewriter.Write(roundTimer, roundDistance, roundErrors);                    
                    Reset();
                }
            }

            // Not waiting, set to wait and the limit to 2
            // Using float Timer (ftimer) as base
            else
            {
                waiting = true;
                waitLimit = roundTimer + 2.5f;
            }
        }
    }
    
    // Once per Loop, resets the state of the room
    void Reset() {
        // Per round values
        rounds++;
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
        Rotate(degree);
        // Pick the Boxes
        setBoxes(degree);
    }

    // Rotates the Room
    void Rotate(int degree) {
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



    // Sets which Boxes are chosen based on degree of rotation
    private void setBoxes(int degree) {
        // Invalid degree
        if (degree != 0 && degree != 90 && degree != 180 && degree != 270) {
            throw new System.Exception("Wrong Rotation Degree: SetBoxes");
        }

        setBoxesZ(degree);        
    }    

    private void setupBoxesZ(int[] indexSet) {

        list0 = indexSet;

        // Setup rotated arrays with selected boxes' indexes
        for (int i = 0; i < list0.Length; i++)
        {
            list90[i] = BoxConfig.rotateClockwise(list0[i]);
            list180[i] = BoxConfig.flip(list0[i]);
            list270[i] = BoxConfig.rotateCounterClockwise(list0[i]);
        }

        // Debug
        /*
        Debug.Log("Config90: " + list90[0] + ", " + list90[1] + ", " 
                  + list90[2] + ", " + list90[3] + ", " + list90[4]);
        Debug.Log("Config180: " + list180[0] + ", " + list180[1] + ", " 
                  + list180[2] + ", " + list180[3] + ", " + list180[4]);
        Debug.Log("Config270: " + list270[0] + ", " + list270[1] + ", " 
                  + list270[2] + ", " + list270[3] + ", " + list270[4]);
        */
    }


    // Config Z
    private void setBoxesZ(int degree) {   
        int[] degreeList = new int[5];
        // Set list of selected boxes based on rotation
        switch(degree) {
            case 0 : 
                degreeList = list0;
                break;
            case 90 :
                degreeList = list90;

                break;
            case 180 :
                degreeList = list180;
                break;
            case 270 :
                degreeList = list270;
                break;
        }

        /*
        Debug.Log("Config: " + degreeList[0] + ", " + degreeList[1] + ", " 
                  + degreeList[2] + ", " + degreeList[3] + ", " + degreeList[4]);
        */

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
                    boxRenderer.material.mainTexture = green;   
                    correct.Play(); 
                } 
                // If red (no prize) box
                else {
                    roundErrors++;
                    boxRenderer.material.mainTexture = red;
                    incorrect.Play();
                }
                // add to discovered
                discovered.Add(box.name);
            }                
        }
    }
}
