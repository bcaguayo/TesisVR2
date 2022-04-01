using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Typewriter : MonoBehaviour
{
    // datapath
    private static string FILENAME;
    private static int ROUND;

    // Start is called before the first frame update
    void Start()
    {
        ROUND = 1;
        FILENAME = Application.dataPath + "/tesis.csv";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void Write(float time, float distance, int errors)
    {
        Debug.Log("Writing");
        TextWriter tw = File.AppendText(FILENAME);        
        tw.WriteLine(ROUND + ", " + time + ", " + distance + ", " + errors);
        ROUND++;
        tw.Close();
    }


    public static void Reset()
    {
        Debug.Log("Reset");
        ROUND = 1;
        if (!File.Exists(FILENAME)) {
            TextWriter tw = File.CreateText(FILENAME);
            tw.WriteLine("Round, Time, Distance, Errors");
            tw.Close();
        }
    }
}
