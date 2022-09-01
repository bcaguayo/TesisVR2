using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    /*
    public void Preset() {
        // Change Config Value

        BoxConfig.Instance.Preset();        
        int[] preset = BoxConfig.Instance.GetConfig();

        // Toggles
        BoxToggle.ClearToggle(); 
        BoxToggle.ToggleAll(preset, true);
    }
    */

    public void Preset() {
        // Use Instance Assigning
        int[] config = BoxConfig.Instance.Preset();

        // Rearrange Toggles
        BoxToggle.ClearToggle(); 
        BoxToggle.ToggleAll(config, true);
    }

    public void Randomize() {
        int[] config = BoxConfig.Instance.RandomizeConfig();
        BoxToggle.ClearToggle();      
        BoxToggle.ToggleAll(config, true);  
    }

    public void Play() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit() {
        Application.Quit();
    }
}
