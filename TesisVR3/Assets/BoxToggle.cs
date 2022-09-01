using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxToggle : MonoBehaviour {

    public GameObject prefab;

    // 5x5 (-600, 450) 300d
    // 4x4 (-550, 350) 300d
    // 3x3 (-400, 250) 300d

    /* Functions:
    1. Spawn the boxes given size button
    2. Assign them an Index
    2. Pass OnValueChanged(bool) to SwitchMe(index, bool)
       based on this box's index
    3. Use Toggle to let other buttons interact with the boxes
    */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
