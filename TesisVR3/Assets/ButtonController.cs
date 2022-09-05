using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

    // Presets 
    private int[] PRESET25 = new int[]{4, 7, 15, 18, 21};
    private int[] PRESET16 = new int[]{2, 8, 11, 13};
    private int[] PRESET9 = new int[]{1, 6, 8};

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

    public void Quit() {
        Application.Quit();
    }
}
