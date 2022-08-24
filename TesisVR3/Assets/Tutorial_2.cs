using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_2 : MonoBehaviour {

    [SerializeField] GameObject room;
    private static GameObject iroom;
    /*
    [SerializeField] private GameObject box1, box2, box3, box4, box5,
                                    box6, box7, box8, box9, box10,
                                    box11, box12, box13, box14, box15,
                                    box16, box17, box18, box19, box20,
                                    box21, box22, box23, box24, box25;
    private GameObject[] boxes;
    void Awake() {
        // Add box objects to array for Resets
        boxes = new GameObject[] {box1, box2, box3, box4, box5,
                                  box6, box7, box8, box9, box10,
                                  box11, box12, box13, box14, box15,
                                  box16, box17, box18, box19, box20,
                                  box21, box22, box23, box24, box25};
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        iroom = room;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Load() {
        // Rotate();
    }

    public void Rotate() {
        iroom.gameObject.transform.Rotate(0f, 90f, 0f);
        /*
        foreach (GameObject box in boxes) {
            MiniCollision b = box.GetComponent<MiniCollision>();
            b.Reset();
        }
        */
    }
}
