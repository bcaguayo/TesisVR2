using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class Screenwriter {
    static TextMeshProUGUI Rounds, Timer, Sucess, Errors;
    static GameObject EndPanel;
    private static int rounds, timeMin, roundSuccess, roundErrors;
    private static float timeSec;
    
    public static void Start(TextMeshProUGUI r, TextMeshProUGUI t, TextMeshProUGUI s
                           , TextMeshProUGUI e, GameObject p) {
        Rounds = r;
        Timer = t;
        Sucess = s;
        Errors = e;
        EndPanel = p;
    }

    // Hide End Panel on Start
    public static void HidePanel() {
        EndPanel.gameObject.SetActive(false);
    }

    // On Start: Initialize all variables
    public static void Reset() {
        rounds = 1;
        timeMin = 0;
        timeSec = 0;
        roundErrors = 0;
        roundSuccess = 0;
        Rounds.text = "Rondas: 00";
        Timer.text = "00:00";
        Sucess.text = "00 / ";
        Errors.text = "00";
    }   

    // On Update
    public static void Loop() {
        Rounds.text = "Rondas: " + stringT(rounds);
        Timer.text = stringT(timeMin) + ":" + stringT(timeSec);
        Sucess.text = stringT(roundSuccess) + " / ";
        Errors.text = stringT(roundErrors);
    }
    
    public static void ShowPanel() {
        EndPanel.gameObject.SetActive(true);
    }

    public static void End() {
        Rounds.gameObject.SetActive(false);
        Timer.gameObject.SetActive(false);
        Sucess.gameObject.SetActive(false);
        Errors.gameObject.SetActive(false);
        ShowPanel();
    }   
    
    public static void Round() {
        rounds++;
        roundErrors = 0;
        roundSuccess = 0;
    }

    public static void AddTime(float newTime) {
        float total = timeMin * 60 + timeSec + newTime;
        timeMin = (int) total / 60;
        timeSec = total % 60;
    }

    public static void Success() {
        roundSuccess++;
    }

    public static void Error() {
        roundErrors++;
    }

    // Helpers
    private static string stringT(int number) {
        string n = number.ToString();
        if (n.Length < 2) {
            n = "0" + n;
        }
        return n;
    }

    private static string stringT(float number) {
        int n = (int) (number * 100) / 100;
        return stringT(n);
    }
}
