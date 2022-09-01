using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxConfig : MonoBehaviour {

    // Singleton Instance for Box Config class
    public static BoxConfig Instance;
    /* [1] Array containins config
    Default {4, 7, 15, 18, 21} for 5x5
            {2, 8, 11, 13} for 4x4
            {1, 6, 8} for 3x3
    */
    private List<int> config;

    // Presets 
    /*
    private int[] PRESET25 = new int[]{4, 7, 15, 18, 21};
    private int[] PRESET16 = new int[]{2, 8, 11, 13};
    private int[] PRESET9 = new int[]{1, 6, 8}; */

    private List<int> PRESET25 = new List<int>(new int[]{4, 7, 15, 18, 21});
    private List<int> PRESET16 = new List<int>(new int[]{2, 8, 11, 13});
    private List<int> PRESET9 = new List<int>(new int[]{1, 6, 8});


    // [2] 25 for 5x5 boxes (Default), 16 for 4x4 boxes, 9 for 3x3 boxes
    private int boxesCount;
    // [3] Number of Rounds. Default 10
    private int rounds;    

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);            
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Default values
        if (Instance != null) {
            // 5X5 Preset
            boxesCount = 25;            
            Preset();
            rounds = 10;
        }
    }

    // Setter for Config
    public void SetConfig(int[] arr) {
        config = new List<int>(arr);
    }

    public void Add(int box) {
        config.Add(box);
        config.Sort();
    }

    public void Remove(int box) {
        config.Remove(box);
    }

    // Getter for Config
    public int[] GetConfig() {
        return config.ToArray();
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
        Preset();
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

    public void Preset() {
        switch (boxesCount) {
            case 9 :
                config = PRESET9;
                break;
            case 16 :
                config = PRESET16;
                break;
            case 25 :
                config = PRESET25;
                break;
        }
    }

    public void RandomizeConfig() {

        // Number of picked boxes is sqrt of total no boxes
        int pickedBoxes = (int) Mathf.Sqrt(boxesCount);

        // New Config
        List<int> randomConfig = new List<int>(pickedBoxes);

        // Add boxes till full array
        while (randomConfig.Count <= pickedBoxes) {
            // Create random between 1 and the limit of boxes.
            int r = Random.Range(1, pickedBoxes + 1);
            // If seen, pick another
            if (randomConfig.Contains(r)) {
                continue;
            }
            // If not seen, add
            randomConfig.Add(r);
        }

        // Sort
        randomConfig.Sort();
        // Set to property
        config = randomConfig;        
    }
}
