using UnityEngine;
using System.IO;

public static class Typewriter
{
    // datapath
    private static string FILENAME = Application.dataPath + "/Results.csv";
    private static int ROUND = 1;

    public static void Write(float time, float distance, int errors)
    {
        // Debug.Log("Writing");
        TextWriter tw = File.AppendText(FILENAME);        
        tw.WriteLine(ROUND + "; " + time + "; " + distance + "; " + errors);
        ROUND++;
        tw.Close();
    }


    public static void Reset()
    {
        // Debug.Log("Reset");
        ROUND = 1;
        if (!File.Exists(FILENAME)) {
            TextWriter tw = File.CreateText(FILENAME);
            tw.WriteLine("Round; Time; Distance; Errors");
            tw.Close();
        }
    }
}
