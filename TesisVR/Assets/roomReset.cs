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

    // Start is called before the first frame update
    // Rounds measures how many loops completed
    // Timer measures frames, fTimer measures seconds
    // Config stores the disposition of the boxes
    // PrevDegree is used to rotate the room
    private int score, rounds, timer, config, prevDegree;
    private float ftimer;

    // Boxes stores the GameObjects
    private GameObject[] boxes;

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
        /* config = Random.Range(1, 4);

        // For selecting random boxes
        int[] randomBoxes = new int[];
        int added = 0;

        // For 5 boxes
        while (added <= 5) {
            Random r = new Random();
            int rdm = r.Next(1, 25);
            if (!randomBoxes.Contains(rdm)) {
                randomBoxes[added] = rdm;
                added++;
            }
        }
        */

        // degree = 0, config = 5
        setBoxes(0, 5);
        
        // ----------------- on excel
        Typewriter.Reset();
    }

    void Start() {        
    }

    // Update is called once per frame
    void Update()
    {
        // or time++
        timer++;
        ftimer += Time.deltaTime;
        // ftimer = (int)(ftimer * 100) / 100;
        // When round limit is reached
        if (rounds >= 10) {
            // Quit app
            // TODO
            Application.Quit();
        }

        // When 5 boxes are found
        if (score >= 5)
        {

            // Wait 2 seconds before reset
            if (waiting)
            {
                if (roundTimer >= waitLimit)
                {
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
                waitLimit = roundTimer + 2.0f;
            }

            // StartCoroutine(WaitForBox());

        }
    }

    // Wait after last Box found
    IEnumerator WaitForBox() {
        yield return new WaitForSeconds(500);
    }

    // Detects controller collision with Box
    public void Collision(string collider, GameObject box, bool pick) {        
        if (collider == "Controller (left)" || collider == "Controller (right)") { 
            // Get the box's renderer
            Renderer boxRenderer = box.GetComponent<Renderer>(); 
            // Not discovered yet
            if (!discovered.Contains(box.name)) {
                // If green (prize) box
                if (pick) {
                    boxRenderer.material.mainTexture = green;   
                    correct.Play();                     
                    score++;
                } 
                // If red (no prize) box
                else {
                    boxRenderer.material.mainTexture = red;
                    incorrect.Play();
                }
                // add to discovered
                discovered.Add(box.name);
            }                
        }
    }

    // Once per Loop, resets the state of the room
    void Reset()
    {
        // Per round values
        rounds++;
        roundErrors = 0;
        roundTimer = 0f;
        roundDistance = 0f;
        prevPos = new Vector2(-10.0f, -10.0f);

        // for every box
        foreach(GameObject box in boxes) {
                // 1. Turn yellow
                Renderer renderer = box.GetComponent<Renderer>();
                renderer.material.mainTexture = yellow;
                // 2. Set not chosen
                BoxCollision b = box.GetComponent<BoxCollision>();
                b.changePick(false);
            }
            // Clear the discovered array
            discovered = new HashSet<string>();
            // Clear Score
            score = 0;

            // Pick a rotation degree
            int degree = Random.Range(0, 3) * 90;
            // if no rotation, pick a new Random
            if (degree == prevDegree) {
                degree += Random.Range(1, 3) * 90;
            }
            // Crop over angle
            if (degree > 270) degree -= 360;
            // Debug.Log("Random: " + degree);
            // Rotate the Room
            Rotate(degree);
            // Pick the Boxes
            setBoxes(degree, config);
    }

    // Rotates the Room
    void Rotate(float degree) {
        // Degree is with respect to 0
        // Rotates by the difference with the prev rotation
        int dif = (int) degree - prevDegree;
        room.transform.Rotate(0f, dif, 0f, Space.Self);
        prevDegree = (int) degree;
    }
    
    // ConfigMaster.WakeUp();
    // ConfigMaster.Add(configA);

    // Sets which Boxes are chosen based on degree of rotation
    private void setBoxes(int degree, int config) {
        // Invalid degree
        if (degree != 0 && degree != 90 && degree != 180 && degree != 270) {
            throw new System.Exception("Wrong Rotation Degree: SetBoxes");
        }

        // Invalid Config number
        if(config < 1 || config > 5) {
            throw new System.Exception("Out of Bounds Configuration: SetBoxes");
        }

        // Send to SetBoxes method based on random config
        switch (config) {
            case 1 : 
                setBoxesA(degree);
                // local = configA;
                break;
            case 2 : 
                setBoxesB(degree);
                // local = configB;
                break;
            case 3 :  
                setBoxesC(degree);
                // local = configC;
                break; 
            case 4 :
                // Symmetrical Configuration
                setBoxesS(degree);
                // local = configC;
                break;
            case 5 :  
                setBoxesZ(degree);
                // local = configC;
                break;               
        }

        /*
        foreach(GameObject box in boxes) {
            // BoxNumber : 1 to 25
            string boxNumber = box.name.Substring(5, box.name.Length - 1);
            /* Local[] from 0 to 3
            Local[0] for 0 degrees
            Local[1] for 90 degrees
            Local[2] for 180 degrees
            Local[3] for 270 degrees
            
            
            if (local[degree / 90].Contains(boxNumber)) {
                BoxCollision b = box.GetComponent<BoxCollision>();
                b.changePick(true);
            }       
        }
        */
    }

    void pickBox(GameObject box) {
        box.GetComponent<BoxCollision>().changePick(true);
    }

    
    private void setBoxesA(int degree) {
        foreach(GameObject box in boxes) {
            switch (degree) {
                // 0 degrees: 03, 06, 15, 17, 24
                case 0 :                    
                    if (box.name == "box (3)" || box.name == "box (6)" ||
                        box.name == "box (15)"|| box.name == "box (17)" || 
                        box.name == "box (24)") {
                        pickBox(box);
                    }      
                    break;
                // 90 degrees: 15, 04, 23, 07, 16
                case 90 :
                    if (box.name == "box (4)" || box.name == "box (7)" ||
                        box.name == "box (15)"|| box.name == "box (16)" || 
                        box.name == "box (23)") {
                        pickBox(box);
                    }    
                    break;
                // 180 degrees: 23, 20, 11, 09, 02
                case 180 :
                    if (box.name == "box (2)" || box.name == "box (9)" ||
                        box.name == "box (11)"|| box.name == "box (20)" || 
                        box.name == "box (23)") {
                        pickBox(box);
                    }          
                    break;
                // 270 degrees: 11, 22, 03, 19, 10
                case 270 :
                    if (box.name == "box (3)" || box.name == "box (10)" ||
                        box.name == "box (11)"|| box.name == "box (19)" || 
                        box.name == "box (22)") {
                        pickBox(box);
                    }   
                    break;
            }
        }
    }

    // Config 2
    private void setBoxesB(int degree) {
        foreach(GameObject box in boxes) {
            switch (degree) {
                // 0 degrees: 04, 06, 15, 18, 22
                case 0 :                    
                    if (box.name == "box (4)" || box.name == "box (6)" ||
                        box.name == "box (15)"|| box.name == "box (18)" || 
                        box.name == "box (24)") {
                        pickBox(box);
                    }                   
                    break;
                // 90 degrees: 03, 06, 14, 20, 22
                case 90 :
                    if (box.name == "box (3)" || box.name == "box (6)" ||
                        box.name == "box (14)"|| box.name == "box (20)" || 
                        box.name == "box (22)") {
                        pickBox(box);
                    }
                    break;
                // 180 degrees: 04, 08, 11, 20, 22
                case 180 :
                    if (box.name == "box (4)" || box.name == "box (8)" ||
                        box.name == "box (11)"|| box.name == "box (20)" || 
                        box.name == "box (22)") {
                        pickBox(box);
                    }                  
                    break;
                // 270 degrees: 04, 06, 12, 20, 23
                case 270 :
                    if (box.name == "box (4)" || box.name == "box (6)" ||
                        box.name == "box (12)"|| box.name == "box (20)" || 
                        box.name == "box (23)") {
                        pickBox(box);
                    }                
                    break;
            }
        }
        
    }

    // Config 3
    private void setBoxesC(int degree) {
        foreach(GameObject box in boxes) {
            switch (degree) {
            // 0 degrees: 01, 10, 14, 17, 23
                case 0 :                    
                    if (box.name == "box (1)" || box.name == "box (10)" ||
                        box.name == "box (14)"|| box.name == "box (17)" || 
                        box.name == "box (23)") {
                        pickBox(box);
                    }                    
                    
                    break;
                // 90 degrees: 02, 08, 15, 19, 21
                case 90 :
                    if (box.name == "box (2)" || box.name == "box (8)" ||
                        box.name == "box (15)"|| box.name == "box (19)" || 
                        box.name == "box (21)") {
                        pickBox(box);
                    }                    
                    
                    break;
                // 180 degrees: 03, 09, 12, 16, 25
                case 180 :
                    if (box.name == "box (3)" || box.name == "box (9)" ||
                        box.name == "box (12)"|| box.name == "box (16)" || 
                        box.name == "box (25)") {
                        pickBox(box);
                    }                    
                    
                    break;
                // 270 degrees: 05, 07, 11, 18, 24
                case 270 :
                    if (box.name == "box (5)" || box.name == "box (7)" ||
                        box.name == "box (11)"|| box.name == "box (18)" || 
                        box.name == "box (24)") {
                        pickBox(box);
                    }
                    break;
                }
        }        
    }

    // Config S
    private void setBoxesS() {
        foreach(GameObject box in boxes) {
            if (box.name == "box (2)" || box.name == "box (10)" ||
                box.name == "box (13)"|| box.name == "box (16)" || 
                box.name == "box (24)") {
                pickBox(box);
            }                    
        }
    }

    // Config Z    WIP : TRY
    private void setBoxesZ(int degree)
    {
        // Selected boxes at degree 0
        int[] boxList = new int[5] {1, 10, 14, 17, 23};
        // at 90, 180 and 270 degrees
        int[] list90 = new int[5];
        int[] list180 = new int[5];
        int[] list270 = new int[5];

        // Setup rotated arrays with selected boxes' indexes
        for (int i = 0; i < boxList.length; i++)
        {
            list90[i] = BoxConfig.rotateClockwise(boxList[i]);
            list180[i] = BoxConfig.flip(boxList[i]);
            list270[i] = BoxConfig.rotateCounterClockwise(boxList[i]);
        }

        // Set list of selected boxes based on rotation
        int[] degreeList;
        switch(degree) {
            case 0 : 
                degreeList = boxList;
                break;
            case 90 : break;
                degreeList = list90;
                break;
            case 180 : break;
                degreeList = list180;
                break;
            case 270 : break;
                degreeList = list270;
                break;
        }

        // Set box to marked
        foreach (int i in degreeList)
        {
            // Index in List is box number - 1
            pickBox(boxes[i - 1]);
        }
    }
}
