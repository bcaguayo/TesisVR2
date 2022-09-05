using UnityEngine;
using System.IO;

public static class Typewriter
{
    // datapath
    private static string DATAPATH = Application.dataPath;
    private static string FILENAME = Path.Combine(DATAPATH, "Results.csv");
    private static int ROUND = 1;
    private static string printConfig;
    private static string subjectName;

    public static void Write(float time, float distance, int errors) {        
        TextWriter tw = new StreamWriter(FILENAME, true);
        if (ROUND == 1) {
            tw.WriteLine(ROUND + "; " + time + "; " + distance + "; " + errors
                        + "; " + printConfig + "; " + subjectName);
        } else {    
            tw.WriteLine(ROUND + "; " + time + "; " + distance + "; " + errors);
        }
        ROUND++;
        tw.Close();
    }

    public static void Set(int[] config, string name) {
        string sConfig = "[";
        for (int i = 0; i < config.Length; i++) {
            sConfig += config[i];
            if (i != config.Length - 1) sConfig += ", ";        
        }
        sConfig += "]";
        printConfig = sConfig;
        subjectName = name;
    }

    public static void Reset() {
        ROUND = 1;
        if (!File.Exists(FILENAME)) {
            TextWriter tw = File.CreateText(FILENAME);
            tw.WriteLine("Round; Time; Distance; Errors; Config; Name");
            tw.Close();
        }
    }
}
