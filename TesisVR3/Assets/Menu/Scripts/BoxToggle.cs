using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class BoxToggle : MonoBehaviour {

    // Non Static objects are assigned on the Editor
    public Toggle prefab;
    public GameObject boxManager;
    // Static Objects are used for static methods
    private static Toggle sPrefab;
    private static GameObject sManager;

    // Boxes are generated based on Size
    private static Toggle[] boxes;

    // Start is called before the first frame update
    void Start() {
        // Static vars
        sPrefab = prefab;
        sManager = boxManager;

        // Preset
        int noBoxes = BoxStandalone.Instance.GetCount(); // number of boxes
        SpawnToggles(noBoxes);
        // Config
        int[] config = BoxStandalone.Instance.GetConfig();
        ToggleAll(config, true);
    }

    // If the Object exists in boxes, Return its index.
    public static int GetIndex(Toggle g) {
        // Loop
        for (int i = 0; i < boxes.Length; i++) {
            // Compare Object
            if (boxes[i] == g) {
                // Return Order in Array with respect to indexing
                return (i + 1); 
            }
        }        
        return 0;
    }

    // Gives a Toggle the opposite value
    public static void Switch(Toggle t) {
        int index = GetIndex(t);
        if (t.isOn) {
            BoxStandalone.Instance.Add(index);
        } else {
            BoxStandalone.Instance.Remove(index);
        }
    }

    // Gives a specific box a specific value for Toggle
    // true for Green, false for Red
    public static void Toggle(int boxIndex, bool value) {
        // Box Index in Array is 1 less than in config
        Toggle t = boxes[boxIndex - 1];
        t.isOn = value;
    }

    // Makes all boxes green
    public static void ToggleAll(int[] config, bool value) {
        foreach (int i in config) {
            Toggle(i, value);
        }
    }

    // Makes all boxes red
    public static void ClearToggle() {
        foreach (Toggle t in boxes) {
            t.isOn = false;
        }
    }

    // 5x5 (-600, 450) 300d
    // 4x4 (-550, 350) 300d
    // 3x3 (-400, 250) 300d

    /* Functions:
    1. Spawn the boxes given size button
    2. Assign them an Index
    2. Pass OnValueChanged(bool) to SwitchMe(index, bool)
       based on this box's index
    3. Use Toggle to let other buttons interact with the boxes
    */
    public static void SpawnToggles(int count) {
        // Destroy previous Toggles
        foreach(Transform child in sManager.transform) {
            // DESTROY THE CHILDREN
            Destroy(child.gameObject);
        }
        
        // New boxes array
        boxes = new Toggle[count];        
        // 3 for 3x3, 4 for 4x4, 5 for 5x5
        int steps = (int) Mathf.Sqrt(count);
        
        int dif = 75;
        int xMin = -150;
        int yMin = 130;
        int index = 0;

        for (int i = 0; i < steps; i++) {
            for (int j = 0; j < steps; j++)
            {
                // Create Checkmark
                Toggle box = Instantiate(sPrefab, sManager.transform);
                int x = xMin + dif * i;
                int y = yMin - dif * j;
                // Move it (doesn't work another way)
                box.transform.Translate(x, y, 0);
                // Add to Array                
                boxes[i * steps + j] = box;
                // boxes[index] = box;
                index++;
            }
        }
    }
}
