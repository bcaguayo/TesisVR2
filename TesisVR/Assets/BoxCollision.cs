using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollision : MonoBehaviour
{
    [SerializeField] private GameObject box;
    [SerializeField] public bool pick;
    public roomReset room;
	private Renderer boxRenderer;
    // public bool discovered;

    void OnCollisionEnter(Collision collisionInfo) {
        string collider = collisionInfo.collider.name;
        // boxboxRenderer = GetComponent<Renderer>();
        room.Collision(collider, box, pick);    
    }

    public bool getPick() {
        bool p = pick;
        return p;
    }

    public void changePick(bool p) {
        pick = p;
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
