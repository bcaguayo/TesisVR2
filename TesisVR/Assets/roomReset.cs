using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomReset : MonoBehaviour
{
    [SerializeField] private GameObject room;
    [SerializeField] private Texture red, yellow, green;
    [SerializeField] private GameObject box1, box2, box3, box4, box5,
                                    box6, box7, box8, box9, box10,
                                    box11, box12, box13, box14, box15,
                                    box16, box17, box18, box19, box20,
                                    box21, box22, box23, box24, box25;
    // Start is called before the first frame update
    private int score, rounds;
    private float timer;
    private Quaternion originalRotation;
    private int prevDegree = 0;
    private GameObject[] boxes;

    private HashSet<string> discovered = new HashSet<string>();

    void Start()
    {
        timer = 0;
        // Add box objects to array for Resets
        boxes = new GameObject[] {box1, box2, box3, box4, box5,
                                  box6, box7, box8, box9, box10,
                                  box11, box12, box13, box14, box15,
                                  box16, box17, box18, box19, box20,
                                  box21, box22, box23, box24, box25};
        // Store the original rotation of the room
        originalRotation = this.transform.rotation;
        // Set rounds to start at 0
        rounds = 0;
        // Set the boxes according to 0 degrees
        setBoxes(0);
    }

    // Update is called once per frame
    void Update()
    {
        // There's 5 prized boxes
        if (score >= 5) {
            rounds++;
            Reset();
            Debug.Log("Rounds: " + rounds + ", Timer: " + timer);
        }
        timer++;

        if (rounds >= 5) {
            Application.Quit();
        }
    }

    void Quit() {
        Application.Quit();
    }

    // Detects controller collision with Box
    public void Collision(string collider, GameObject box, bool pick) {        
        if (collider == "Controller (left)" || collider == "Controller (right)") { 
            // Get the box's renderer
            Renderer boxRenderer = box.GetComponent<Renderer>(); 
            // Not discovered yet
            if (!discovered.Contains(box.name)) {
                // If green (prize) box
                if(pick) {
                    boxRenderer.material.mainTexture = green;                        
                    score++;
                } 
                // If red (no prize) box
                else {
                    boxRenderer.material.mainTexture = red;
                }
                // add to discovered
                discovered.Add(box.name);
            }                
        }
    }

    // Once per Loop, resets the state of the room
    void Reset() {
        // If rounds are 10       
        if (rounds >= 5) Application.Quit();

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
            setBoxes(degree);
    }

    // Rotates the Room
    public void Rotate(float degree) {
        /* Quaternion currentRotation = new Quaternion (transform.rotation.x, transform.rotation.y, 
                                                     //transform.rotation.z, 0);
        // Resets to original Rotation
         currentRotation = originalRotation; */

        // Degree is with respect to 0
        // Rotates by the difference with the prev rotation
        int dif = (int) degree - prevDegree;
        // Debug.Log("Rotating: " + degree + ", prevRotation: " + prevDegree + ", dif: " + dif);
        room.transform.Rotate(0f, dif, 0f, Space.Self);
        prevDegree = (int) degree;
    }

    // Sets which Boxes are chosen based on degree of rotation
    public void setBoxes(int degree) {
        switch (degree) {
            // 0 degrees: 03, 06, 15, 17, 24
            case 0 :
                foreach(GameObject box in boxes) {
                    if (box.name == "box (3)" || box.name == "box (6)" ||
                        box.name == "box (15)"|| box.name == "box (17)" || box.name == "box (24)") {
                        BoxCollision b = box.GetComponent<BoxCollision>();
                        b.changePick(true);
                    }                    
                }
                break;
            // 90 degrees: 15, 04, 23, 07, 16
            case 90 :
                foreach(GameObject box in boxes) {
                    if (box.name == "box (4)" || box.name == "box (7)" ||
                        box.name == "box (15)"|| box.name == "box (16)" || box.name == "box (23)") {
                        BoxCollision b = box.GetComponent<BoxCollision>();
                        b.changePick(true);
                    }                    
                }
                break;
            // 180 degrees: 23, 20, 11, 09, 02
            case 180 :
                foreach(GameObject box in boxes) {
                    if (box.name == "box (2)" || box.name == "box (9)" ||
                        box.name == "box (11)"|| box.name == "box (20)" || box.name == "box (23)") {
                        BoxCollision b = box.GetComponent<BoxCollision>();
                        b.changePick(true);
                    }                    
                }
                break;
            // 270 degrees: 11, 22, 03, 19, 10
            case 270 :
                foreach(GameObject box in boxes) {
                    if (box.name == "box (3)" || box.name == "box (10)" ||
                        box.name == "box (11)"|| box.name == "box (19)" || box.name == "box (22)") {
                        BoxCollision b = box.GetComponent<BoxCollision>();
                        b.changePick(true);
                    }                    
                }
                break;
        }
    }
}
