using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

    /*
    public void SwitchMe(Toggle t) {
        BoxToggle.Listener(t);
    }
    */

    public void Size(int size) {
        // Change Config Values
        BoxConfig.Instance.SetCount(size);
        
        // Visuals: Checkmarks
        BoxToggle.SpawnToggles(size);
        // Preset  
        Preset();
    }

    public void Preset() {
        // Use Instance Assigning at BoxConfig
        int[] config = BoxConfig.Instance.Preset();

        // Rearrange Toggles
        BoxToggle.ClearToggle(); 
        BoxToggle.ToggleAll(config, true);
    }

    public void Randomize() {
        // Use Instance Assigning at BoxConfig
        int[] config = BoxConfig.Instance.RandomizeConfig();
        
        // Toggles
        BoxToggle.ClearToggle();      
        BoxToggle.ToggleAll(config, true);  
    }

    public void Rounds(float f) {
        BoxConfig.Instance.SetRounds((int) f);
    }

    public void Name(string s) {
        BoxConfig.Instance.SetName(s);
    }

    public void Play() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RunTest() {
        // insert process
    }

    public void Quit() {
        Application.Quit();
    }
}
