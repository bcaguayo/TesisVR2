using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxConfig : MonoBehaviour {

    // Singleton Instance for Box Config class
    public static BoxConfig Instance;
    /* Array containins config
    Default {4, 7, 15, 18, 21} for 5x5
            {2, 8, 11, 13} for 4x4
            {1, 6, 8} for 3x3
    */
    private int[] config;
    // 25 for 5x5 boxes, 16 for 4x4 boxes, 9 for 3x3 boxes
    // Default 25
    private int boxesCount;
    // Number of Rounds. Default 10
    private int rounds;

    private void Awake() {

        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        // Default values
        if (Instance != null) {
            boxesCount = 25;
            // 5X5 Preset
            int[] preset = new int[]{4, 7, 15, 18, 21};
            config = preset;
            rounds = 10;
        }
    }

    // Setter for Config
    public void SetConfig(int[] arr) {
        config = new int[arr.Length];
        foreach (int i in arr) {
            config[i] = arr[i];
        }
    }

    // Getter for Config
    public int[] GetConfig() {
        return config;
    }

    // Setter for Rounds
    public void SetRounds(int roundLimit) {
        rounds = roundLimit;
    }

    // Getter for Rounds
    public int GetRounds() {
        return rounds;
    }

    // Setter for BoxCount
    public void SetCount(int count) {
        boxesCount = count;
    }

    // Getter for BoxCount
    public int GetCount() {
        return boxesCount;
    }

    /* Based on RoomManager.ChooseRandom
    Creates a random config and assigns it locally.
    Dependent on configSize to decide how many numbers
    go in the config array.
    */

    public void RandomizeConfig() {

        // Number of picked boxes is sqrt of total no boxes
        int pickedBoxes = (int) Mathf.Sqrt(boxesCount);

        // New Config
        int[] randomConfig = new int[pickedBoxes];

        // Add boxes till full array
        int added = 0;
        while (added <= pickedBoxes) {
            // Create random between 1 and the limit of boxes.
            int r = Random.Range(1, pickedBoxes + 1);
            // If seen, pick another
            foreach (int i in randomConfig) {
                if (r == i) continue;
            }
            // If not seen, add
            randomConfig[added] = r;
            added++;
        }

        // set to property
        config = randomConfig;
    }
}
