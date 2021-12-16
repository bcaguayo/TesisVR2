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
    private int score;
    private GameObject[] boxes;

    void Start()
    {
        boxes = new GameObject[] {box1, box2, box3, box4, box5,
                                  box6, box7, box8, box9, box10,
                                  box11, box12, box13, box14, box15,
                                  box16, box17, box18, box19, box20,
                                  box21, box22, box23, box24, box25};

    }

    // Update is called once per frame
    void Update()
    {
        if (score >= 3) {
            foreach(GameObject box in boxes) {
                Renderer boxRenderer = box.GetComponent<Renderer>();
                boxRenderer.material.mainTexture = yellow;
            }
            score = 0;
            Debug.Log("Reset: " + score);
        }
    }
    public void Collision(string collider, Renderer boxRenderer, bool pick) {
        Debug.Log("Collision");
        if (collider == "glove_left" || collider == "glove_right") {               
                if(pick) {
                    boxRenderer.material.mainTexture = green;
                    // score++;
                    // Debug.Log("Score: " + score);
                } else {
                    boxRenderer.material.mainTexture = red;
                }
            }
    }

    public void Rotate(float degree) {
        room.transform.Rotate(0f, degree, 0f, Space.Self);
    }
}
