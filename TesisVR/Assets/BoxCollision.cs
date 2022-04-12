using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollision : MonoBehaviour {
    private bool pick, discovered;
    public RoomManager Manager;
	private Renderer boxRenderer;

    // public bool discovered;

    void Awake() {
        boxRenderer = this.GetComponent<Renderer>(); 
    }

    void OnCollisionEnter(Collision collisionInfo) {
        string collider = collisionInfo.collider.name;
        if (!discovered) {
            if (collider == "Controller (left)" || collider == "Controller (right)") {  
                // Picked = green box          
                if (pick) {                    
                    boxRenderer.material.mainTexture = Manager.getTexture("green");
                    Manager.Correct();  
                } 
                // Red = No prize box
                else {
                    boxRenderer.material.mainTexture = Manager.getTexture("red"); 
                    Manager.Error();                   
                }
                // Add to discovered
                discovered = true;
            }            
        }        
    }

    public void Pick() {
        pick = true;
    }

    public void Discover() {
        discovered = true;
    }

    public void Reset() {
        // Turn yellow
        boxRenderer.material.mainTexture = Manager.getTexture("yellow"); 
        // not Discovered at round start
        discovered = false;
    }
}
