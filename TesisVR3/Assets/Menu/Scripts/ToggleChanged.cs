using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleChanged : MonoBehaviour {

    public Toggle t;

    // Start is called before the first frame update
    void Start() {
        t.onValueChanged.AddListener(delegate {
            ToggleValueChanged(t);
        });
    }

    // Output the new state of the Toggle into Text
    void ToggleValueChanged(Toggle t) {
        BoxToggle.Switch(t);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
