using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

    public void Preset() {
        BoxConfig.Instance.Preset();
    }

    public void Randomize() {
        BoxConfig.Instance.RandomizeConfig();
    }

    public void Play() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit() {
        Application.Quit();
    }
}
