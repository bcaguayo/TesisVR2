using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxToggle : MonoBehaviour {

    public Toggle prefab;
    private static Toggle sPrefab;

    public GameObject boxManager;
    private static GameObject sManager;

    private static Toggle[] boxes;

    // Start is called before the first frame update
    void Start() {
        // Static vars
        sPrefab = prefab;
        sManager = boxManager;

        // Preset
        int noBoxes = BoxConfig.Instance.GetCount();
        SpawnToggles(noBoxes);

        int[] config = BoxConfig.Instance.GetConfig();
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

    public static void Switch(Toggle t) {
        int index = GetIndex(t);
        if (t.isOn) {
            BoxConfig.Instance.Add(index);
        } else {
            BoxConfig.Instance.Remove(index);
        }
    }

    public static void Toggle(int boxIndex, bool value) {
        // Box Index in Array is 1 less than in config
        Toggle t = boxes[boxIndex - 1];
        t.isOn = value;
    }

    public static void ToggleAll(int[] config, bool value) {
        foreach (int i in config) {
            Toggle(i, value);
        }
    }

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
            Destroy(child.gameObject);
        }
        
        // New boxes array
        boxes = new Toggle[count];        
        // 3 for 3x3, 4 for 4x4, 5 for 5x5
        int steps = (int) Mathf.Sqrt(count);
        
        int index = 0;
        int dif = 140;
        int limit = dif * steps;
        for (int y = 0; y > -limit; y -= dif) { // Y Loop
            for (int x = 0; x < limit; x += dif) { // X Loop
                // Create Checkmark
                Toggle box = Instantiate(sPrefab, sManager.transform);
                // Move it (doesn't work another way)
                box.transform.Translate(x, y, 0);
                // Add to Array
                boxes[index] = box;
                index++;
            }
        }
    }
}
