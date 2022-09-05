using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Organizer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI Rounds, Timer, Sucess, Errors;
    [SerializeField] GameObject EndPanel;    

    void Awake()
    {
        Screenwriter.Start(Rounds, Timer, Sucess, Errors, EndPanel);             
    }

    void Start() {
        Screenwriter.Reset();
        Screenwriter.HidePanel();   
        Typewriter.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        Screenwriter.AddTime(Time.deltaTime);
        Screenwriter.Loop();
    }

    public static void Set(int[] config, string name) {
        Typewriter.Set(config, name);
    }

    public static void Write(float roundTimer, float roundDistance, int roundErrors) {
        Typewriter.Write(roundTimer, roundDistance, roundErrors);
    }
    
    public static void Round() {
        Screenwriter.Round();
    }

    public static void Success() {
        Screenwriter.Success();
    }

    public static void Error() {
        Screenwriter.Error();
    }

    public static void End() {
        Screenwriter.End();
    }

}
