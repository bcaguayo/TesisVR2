using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
This class is able to read and write a Json created
to save the Menu Configuration
*/
public class JsonManager : MonoBehaviour {

    // Nested Class saves the structure of config for the json
    [System.Serializable]
    public class Config {
        // Fields
        public int[] config;
        public int boxesCount, rounds;
        public string name;

        // Constructor
        public Config(int[] c, int b, int r, string n) {
            config = c;
            boxesCount = b;
            rounds = r;
            name = n;
        }
    }

    // Datapath
    private static string DATAPATH, PDATAPATH, FILENAME;
    // Singleton
    public static Config Instance;

    private void Awake() {
            // Datapath
        DATAPATH = Application.dataPath;
        // Filename is: "C:/Users/RMS VR/AppData/LocalLow/UACh/VRMenu/Config.json"
        PDATAPATH = "C:/Users/RMS VR/AppData/LocalLow/UACh/VRMenu";
        // Switch to persistent DP on Release
        FILENAME = Path.Combine(PDATAPATH, "Config.json");
        // Single static Instance
        Instance = new Config(new int[0], 25, 10, "");
    }

    // ---------------------------- Setters ---------------------------- 
    public static void SetArray(int[] arr) {
        Instance.config = new int[arr.Length];
        for (int i = 0; i < arr.Length; i++) {
            Instance.config[i] = arr[i];
        }
    }

    public static void SetCount(int c) {
        Instance.boxesCount = c;
    }

    public static void SetRounds(int r) {
        Instance.rounds = r;
    }

    public static void SetName(string n) {
        Instance.name = n;
    }

    // General Setter
    public static void SetValues(int[] arr, int c, int r, string n) {
        SetArray(arr);
        SetCount(c);
        SetRounds(r);
        SetName(n);
    }

    // ---------------------------- Getters ---------------------------- 
    public static int[] GetConfig() {
        /*
        int len = Instance.config.Length;
        arr = new int[len];
        for (int i = 0; i < len; i++) {
            arr[i] = Instance.config[i];
        }
        */
        return (int[]) Instance.config.Clone();
    }

    public static int GetCount() {
        return Instance.boxesCount;
    }

    public static int GetRounds() {
        return Instance.rounds;
    }

    public static string GetName() {
        return Instance.name;
    }

    // ---------------------------- IO ---------------------------- 
    // foolproof engineering ladies and gentlemen
    public static void Write(int[] arr, int c, int r, string n) {
        SetValues(arr, c, r, n);
        Write();
    }

    public static void Write() {
        string json = JsonUtility.ToJson(Instance);
        File.Delete(FILENAME);
        using(StreamWriter w = new StreamWriter(FILENAME, true)){
            w.WriteLine(json);            
            // Debug.Log("Printed to: " + FILENAME);
            // Debug.Log(json);
        }
    }

    // Read needs to be used before Getters
    public static void Read() {
        string json;
        using (StreamReader r = new StreamReader(FILENAME)) {
            json = r.ReadToEnd();
            //Debug.Log(json);
        }
        Instance = JsonUtility.FromJson<Config>(json);
    }
}