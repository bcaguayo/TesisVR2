using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class Screenwriter : MonoBehaviour
{
    public static TMPro.TextMeshPro Rounds, Time, Errors;
    private static int rounds, timeMin, timeSec, roundErrors;

    // Start is called before the first frame update
    void Start() {
        Reset();
    }


    // Update is called once per frame
    void Update()
    {
        Rounds.text = "Rounds: " + rounds;
        Time.text = "" + timeMin + ":" + timeSec;
        Errors.text = "Errors: " + roundErrors;
    }

    public static void Write(int round, float time, int err) {
        WriteRounds(round);
        WriteTime(time);
        WriteErrors(err);
    }
    public static void WriteRounds(int round) {
        rounds = round;
    }

    public static void Round() {
        rounds++;
    }

    public static void WriteTime(float time) {
        timeMin = (int) time / 60;
        timeSec = (int) time % 60;
    }

    public static void WriteErrors(int err) {
        roundErrors = err;
    }

    private static void Reset() {
        rounds = 0;
        timeMin = 0;
        timeSec = 0;
        roundErrors = 0;
        Rounds.text = "Rounds: 00";
        Time.text = "00:00";
        Errors.text = "Errors: 00";
    }

}
