using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCollision : MonoBehaviour
{
    public bool pick;
    private bool discovered;
	private Renderer boxRenderer;

    void Awake() {
        // Get the box's renderer
        boxRenderer  = this.GetComponent<Renderer>();
    }

    void OnCollisionEnter(Collision collisionInfo) {
        // Collider name
        string collider = collisionInfo.collider.name;

        if (collider == "Controller (left)" || collider == "Controller (right)" 
            && discovered == false) { 
            if (pick) {
                BoxManager.scorrect.Play();
                boxRenderer.material.mainTexture = BoxManager.sgreen;
                
            } else {
                BoxManager.sincorrect.Play();
                boxRenderer.material.mainTexture = BoxManager.sred;
            }
            BoxManager.Score(pick);
            discovered = true;
        }
    }

    public void Reset() {
        boxRenderer.material.mainTexture = BoxManager.syellow;
        pick = false;
        discovered = false;
    }

    public void Pick() {
        pick = true;
    }
}
