using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxManager : MonoBehaviour
{
    [SerializeField] private AudioSource correct, incorrect;
    [SerializeField] private Texture red, yellow, green;
    public int part;
    private int maxScore, boxesTotal;

    public static AudioSource scorrect, sincorrect;
    public static Texture sred, syellow, sgreen;

    // Amount of green boxes discovered
    private static int score, discovered;

    // Start is called before the first frame update
    void Awake()
    {
        scorrect = correct;
        sincorrect = incorrect;
        sred = red;
        syellow = yellow;
        sgreen = green;
        maxScore = 2;
        boxesTotal = 2;
        switch (part)
        {
            case 0 :
                
                break;
            default : 
                maxScore = 5;
                boxesTotal = 25;
                break;
        }
    }

    // Update is called once per frame
    void Update() {
        /*
        bool condition = discovered >= boxesTotal;
        Debug.Log(condition);
        */
        if (score >= maxScore || discovered >= boxesTotal) {
            ResetScene();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public static void Score(bool picked) {
        discovered++;
        if (picked) {
            score++;
        }
    }

    public void ResetScene() {
        score = 0;
        discovered = 0;
        switch (part) {
            case 0 :
                maxScore = 2;
                boxesTotal = 2;
                break;
            default : 
                maxScore = 5;
                boxesTotal = 25;
                break;
        }
    }


}
