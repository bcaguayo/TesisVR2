using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxManager : MonoBehaviour {
    // Audio Assets
    [SerializeField] private AudioSource correct, incorrect;
    // Static equivalent
    public static AudioSource scorrect, sincorrect;
    // Box Textures
    [SerializeField] private Texture red, yellow, green;
    // Static equivalent
    public static Texture sred, syellow, sgreen;  

    // Amount of green boxes discovered, Total boxes discovered
    public static int score, discovered;

    // Start is called before the first frame update
    void Awake() {
        scorrect = correct;
        sincorrect = incorrect;
        sred = red;
        syellow = yellow;
        sgreen = green;
    }

    public static void Score(bool picked) {
        discovered++;
        if (picked) {
            score++;
        }
    }
}
