using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollision : MonoBehaviour
{
    [SerializeField] private GameObject box;
    [SerializeField] private bool chosen;
    public roomReset room;
	private Renderer boxRenderer;
    // public bool discovered;

    void OnCollisionEnter(Collision collisionInfo) {
        string collider = collisionInfo.collider.name;
        boxRenderer = box.GetComponent<Renderer>();
        room.Collision(collider, boxRenderer, chosen);    
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
