using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInstance : MonoBehaviour {

    // Singleton Instance for Box Config class
    public static BoxInstance Instance;
    // Array containins config
    private int[] config;
    /* configSize
    5 for 5x5 boxes with 5 Greens & 20 Reds
    4 for 4x4 boxes with 4 Greens & 12 Reds
    3 for 3x3 boxes with 3 Greens & 6 Reds
    */
    private int configSize;

    private void Awake() {

        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        if (Instance != null) {
            int[] preset = new int[]{4, 7, 15, 18, 21};
            SetConfig(preset);
        }
        configSize = 5;
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

    /* Based on RoomManager.ChooseRandom
    Creates a random config and assigns it locally.
    Dependent on configSize to decide how many numbers
    go in the config array.
    */

    public void RandomizeConfig() {
        // storing array
        int[] randomConfig = new int[configSize];
        // total number of boxes in the scene
        int noBoxes = configSize * configSize;

        int added = 0;
        while (added <= configSize) {
            int r = Random.Range(1, noBoxes + 1);
            foreach (int i in randomConfig) {
                if (r == i) continue;
            }
            randomConfig[added] = r;
            added++;
        }

        // set to property
        config = randomConfig;
    }
}
