using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

    public void SwitchMe(int index, bool value) {
        if (value) {
            BoxConfig.Instance.Add(index);
        } else {
            BoxConfig.Instance.Remove(index);
        }
    }

    public void Size(int size) {
        BoxConfig.Instance.SetCount(size);
    }

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
