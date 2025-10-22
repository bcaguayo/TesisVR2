using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/*
This script uses a Parent object to pass the Methods in BoxInstance
and BoxToggle to their respective Buttons
*/
public class ButtonController : MonoBehaviour {

    /*
    public void SwitchMe(Toggle t) {
        BoxToggle.Listener(t);
    }
    */

    // Change the Configuration Size and Spawn new Toggles
    public void Size(int size) {
        // Change Config Values
        BoxInstance.Instance.SetCount(size);
        
        // Visuals: Checkmarks
        BoxToggle.SpawnToggles(size);
        // Preset  
        Preset();
    }

    // Premade arrangement given size
    public void Preset() {
        // Use Instance Assigning at BoxInstance
        BoxInstance.Instance.Preset();

        // Rearrange Toggles
        BoxToggle.ClearToggle(); 
        BoxToggle.ToggleAll(BoxInstance.Instance.GetConfig(), true);
    }

    // Give a Random Configuration
    public void Randomize() {
        // Use Instance Assigning at BoxInstance
        BoxInstance.Instance.RandomizeConfig();
        
        // Toggles
        BoxToggle.ClearToggle();      
        BoxToggle.ToggleAll(BoxInstance.Instance.GetConfig(), true);  
    }

    // Change the number of Rounds
    public void Rounds(float f) {
        BoxInstance.Instance.SetRounds((int) f);
    }

    // Change test subject Name (to excel)
    public void Name(string s) {
        BoxInstance.Instance.SetName(s);
    }

    // Load Next Scene
    public void Play() {
        BoxInstance.Instance.toFile();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Quit the Game
    public void Quit() {
        Application.Quit();
    }
}
