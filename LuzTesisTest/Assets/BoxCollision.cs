using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollision : MonoBehaviour
{
    [SerializeField] private GameObject box;
	[SerializeField] private Texture red, green;
    [SerializeField] private bool chosen;
	private Renderer boxRenderer;
    private bool discovered;
    private int score;

    void OnCollisionEnter(Collision collisionInfo) {
        if (!discovered) {
            if (collisionInfo.collider.name == "glove_left" || 
            collisionInfo.collider.name == "glove_right") {
                Debug.Log("Collision");
                boxRenderer = box.GetComponent<Renderer>();
                if(chosen) {
                    boxRenderer.material.mainTexture = green;
                    score++;
                    Debug.Log("Score: " + score);
                } else {
                    boxRenderer.material.mainTexture = red;
                }                
		    // gameObject.GetComponent<Button>().OnClick.AddListener(ChangeBoxTexture);
        }
        }
        if (score >= 5) {
            
        }        
    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
