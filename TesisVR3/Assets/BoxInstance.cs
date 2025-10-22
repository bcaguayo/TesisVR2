using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInstance : MonoBehaviour {

    // Singleton Instance for Box Config class
    public static BoxInstance Instance;
    /* [0] Array containins config
    Default {4, 7, 15, 18, 21} for 5x5
            {2, 8, 11, 13} for 4x4
            {1, 6, 8} for 3x3
    */
    private List<int> config;
    /* [1] configSize
    5 for 5x5 boxes with 5 Greens & 20 Reds
    4 for 4x4 boxes with 4 Greens & 12 Reds
    3 for 3x3 boxes with 3 Greens & 6 Reds
    */
    private int configSize;

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

    // ________________________ SETUP ________________________

    // Singleton for Data Persistence
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Print
        Debug.Log("BoxInstance Awake");

        // Default values
        if (Instance != null && boxesCount == 0)
        {
            // 5X5 Preset
            boxesCount = 25;
            configSize = 5;
            Preset();
            rounds = 10;
            subjectName = "";
        }
    }

    // ________________________ UTILS ________________________

    // DO ITERABLE instead
    
    // GET FROM CONFIG FILE
    // Setter for Config
    public void SetConfig(int[] arr)
    {
        config = new List<int>();
        foreach (int i in arr)
        {
            if (!config.Contains(i)) config.Add(i);

        }
        config.Sort();
    }

    // Setter for Config with List input
    public void SetConfig(List<int> list)
    {
        config = new List<int>();
        foreach (int i in list)
        {
            if (!config.Contains(i)) config.Add(i);
        }
        config.Sort();
    }
    
    // Adds the box with given index into Config
    public void Add(int box) {
        config.Add(box);
        config.Sort();      
    }

    // Removes the box with given index from Config
    public void Remove(int box)
    {
        config.RemoveAll(x => x == box);
    }

    // Getter for Config
    // Get rid of dupe checking
    public int[] GetConfig()
    {
        // Pass by Ref Safe
        // Sometimes, duplicate elements will remain
        int[] noDupes = Distinct(config).ToArray();
        return noDupes;
    }

    // Helper for Duplicates
    public List<int> Distinct(List<int> l)
    {
        l.Sort();
        List<int> distinct = new List<int>();
        foreach (int i in l)
        {
            if (!distinct.Contains(i)) distinct.Add(i);
        }
        return distinct;
    }

    // ________________________ GET / SET ________________________

    // Setter for BoxCount
    public void SetCount(int count) {
        boxesCount = count;
        configSize = (int) Mathf.Sqrt(count);
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
    public string GetName()
    {
        return subjectName;
    }

    // ________________________ BUTTONS ________________________

    //// Distinct presets for box configs
    /// Only works for perfectly square digits 
    public void Preset()
    {
        switch (boxesCount)
        {
            case 9:
                SetConfig(PRESET9);
                configSize = 3;
                break;
            case 16:
                SetConfig(PRESET16);
                configSize = 4;
                break;
            case 25: // Case 25
                SetConfig(PRESET25);
                configSize = 5;
                break;
        }
    }

    /* Based on RoomManager.ChooseRandom
    Creates a random config and assigns it locally.
    Dependent on configSize to decide how many numbers
    go in the config array.
    */
    public void RandomizeConfig()
    {
        // Clean
        List<int> randomConfig = new List<int>(configSize); 

        while (randomConfig.Count < configSize)
        {
            // Create random between 1 and the limit of boxes.
            int r = Random.Range(1, boxesCount + 1);

            // If seen, loop
            if (randomConfig.Contains(r)) continue;

            // Else, add it
            randomConfig.Add(r);
        }

        randomConfig.Sort();

        SetConfig(randomConfig);
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
