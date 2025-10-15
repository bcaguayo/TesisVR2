using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This script manages the picks for the checkboxes on the BoxesPanel
So far there's three combinations, 3x3 (9 total), 4x4 (16), 5x5 (25)
The configuration is written as a Singleton so the object can be passed
through scenes ()
*/
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
    private List<int> PRESET25 = new List<int>(new int[]{4, 7, 15, 18, 21});
    private List<int> PRESET16 = new List<int>(new int[]{2, 8, 11, 13});
    private List<int> PRESET9 = new List<int>(new int[]{1, 6, 8});

    // [2] 25 for 5x5 boxes (Default), 16 for 4x4 boxes, 9 for 3x3 boxes
    private int boxesCount;
    // [3] Number of Rounds. Default 10
    private int rounds;
    // [4] Name of the test Subject
    private string subjectName;

    // Singleton for Data Persistence
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
            subjectName = "";
        }

        Debug.Log(Application.persistentDataPath);
    }

    // Setter for Config with array input
    public void SetConfig(int[] arr) {
        config = new List<int>(arr);
    }

    // Setter for Config with List input
    public void SetConfig(List<int> list) {
        config = new List<int>();
        foreach (int i in list) {
            config.Add(i);
        }
        config.Sort();
    }

    // Adds the box with given index into Config
    public void Add(int box) {
        config.Add(box);
        config.Sort();      
    }

    // Removes the box with given index from Config
    public void Remove(int box) {
        config.RemoveAll(x => x == box);
    }

    // Getter for Config
    public int[] GetConfig() {
        // Sometimes, duplicate elements will remain
        // DONT REMOVE
        int[] noDupes = Distinct(config).ToArray();
        return noDupes;
    }

    // Helper for Duplicates
    public List<int> Distinct(List<int> l) {
        l.Sort();
        List<int> distinct = new List<int>();
        foreach (int i in l) {
            if (!distinct.Contains(i)) distinct.Add(i);
        }
        return distinct;
    }    

    // Setter for BoxCount
    public void SetCount(int count) {
        boxesCount = count;
    }

    // Getter for BoxCount
    public int GetCount() {
        return boxesCount;
    }

    // Setter for Rounds
    public void SetRounds(int roundLimit) {
        rounds = roundLimit;
    }

    // Getter for Rounds
    public int GetRounds() {
        return rounds;
    }

    // Setter for Name
    public void SetName(string name) {
        subjectName = name;
    }

    // Getter for Rounds
    public string GetName() {
        return subjectName;
    }

    /* Based on RoomManager.ChooseRandom
    Creates a random config and assigns it locally.
    Dependent on configSize to decide how many numbers
    go in the config array.
    */
    //// Don't change the implementation of this method
    public int[] Preset() {
        switch (boxesCount) {
            case 9 : 
                SetConfig(PRESET9);
                break;
            case 16 :
                SetConfig(PRESET16);
                break;
            case 25 : // Case 25
                SetConfig(PRESET25);
                break;
        }
        return config.ToArray();
    }

    public int[] RandomizeConfig() {

        // Number of picked boxes is sqrt of total no boxes
        int pickedBoxes = (int) Mathf.Sqrt(boxesCount);

        // New Config
        List<int> randomConfig = new List<int>(pickedBoxes);

        // Add boxes till full array
        while (randomConfig.Count < pickedBoxes) {
            // Create random between 1 and the limit of boxes.
            int r = Random.Range(1, boxesCount + 1);
            // If seen, pick another
            if (randomConfig.Contains(r)) {
                continue; 
            } else {
                // If not seen, add
                randomConfig.Add(r);
            }
        }

        // Sort
        randomConfig.Sort();

        // Set to property
        SetConfig(randomConfig);
        return config.ToArray();   
    }

    // Helper method to print out Configurations
    private void Print() {
        Debug.Log("Config: ");
        foreach (int i in config) {
            Debug.Log(i + " ");
        }
    }

    /* Print to file using JsonManager:
        (in order)
        configArray, boxesCount, rounds, subjectName
        use GetConfig to erase duplicates
    */
    public void toFile() {
        JsonManager.Write(GetConfig(), boxesCount, rounds, subjectName);
    }
}
