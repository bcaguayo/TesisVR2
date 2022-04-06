using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Organizer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI Rounds, Timer, Errors;
    [SerializeField] GameObject EndPanel;    

    void Awake()
    {
        Screenwriter.Start(Rounds, Timer, Errors, EndPanel);
        Screenwriter.Reset();
        Screenwriter.HidePanel();        
        //Typewriter.Reset();
    }

    void Start() {
        Typewriter.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        Screenwriter.AddTime(Time.deltaTime);
        Screenwriter.Loop();
    }

    public static void Write(float roundTimer, float roundDistance, int roundErrors) {
        Typewriter.Write(roundTimer, roundDistance, roundErrors);
    }
    
    public static void Round() {
        Screenwriter.Round();
    }

    public static void Error() {
        Screenwriter.Error();
    }

    public static void End() {
        Screenwriter.End();
    }

}
