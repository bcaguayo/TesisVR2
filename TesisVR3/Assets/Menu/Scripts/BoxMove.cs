using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxMove : MonoBehaviour
{

    // Non Static objects are assigned on the Editor
    [SerializeField] public GameObject prefab;
    public GameObject boxManager;

    public int COUNT = 25;

    public bool enable;

    // List of boxes
    private static GameObject[] boxes;

    // Start is called before the first frame update
    void Awake()
    {
        if (enable)
        { 
            // New boxes array
            boxes = new GameObject[COUNT];
            // 3 for 3x3, 4 for 4x4, 5 for 5x5
            int steps = (int)Mathf.Sqrt(COUNT);

            int index = 0;
            int dif = 120;
            int xLimit = dif * steps + 300;
            int yLimit = dif * steps + 200;
            for (int y = -250; y > -yLimit; y -= dif) { // Y Loop
                for (int x = 360; x < xLimit; x += dif) { // X Loop
                    // Create Checkmark
                    GameObject box = Instantiate(prefab, boxManager.transform);
                    // Move it (doesn't work another way)
                    box.transform.Translate(x, y, 0);
                    // Add to Array
                    boxes[index] = box;
                    index++;
                }
            }
        }
        
    }
}
