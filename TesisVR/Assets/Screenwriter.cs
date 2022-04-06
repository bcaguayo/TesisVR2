using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Screenwriter : MonoBehaviour
{
    public TextMeshProUGUI Rounds, Time, Errors;
    private static int rounds, timeMin, timeSec, roundErrors;

    // Start is called before the first frame update
    void Start() {
        Reset();
    }


    // Update is called once per frame
    void Update()
    {
        string min = "" + timeMin;
        Rounds.text = "Rondas: " + stringT(rounds);
        Time.text = stringT(timeMin) + ":" + stringT(timeSec);
        Errors.text = "Errores: " + stringT(roundErrors);
    }

    string stringT(int number) {
        string n = number.ToString();
        if (n.Length < 2) {
            n = "0" + n;
        }
        return n;
    }

    public static void Write(int round, float time, int err) {
        WriteRounds(round);
        WriteTime(time);
        WriteErrors(err);
    }
    public static void WriteRounds(int round) {
        rounds = round;
    }

    public static void WriteTime(float time) {
        timeMin = (int) time / 60;
        timeSec = (int) time % 60;
    }

    public static void WriteErrors(int err) {
        roundErrors = err;
    }

    public static void Round() {
        rounds++;
        roundErrors = 0;
    }

    public static void Error() {
        roundErrors++;
    }

    private void Reset() {
        rounds = 0;
        timeMin = 0;
        timeSec = 0;
        roundErrors = 0;
        Rounds.text = "Rondas: 00";
        Time.text = "00:00";
        Errors.text = "Errores: 00";
    }

}
