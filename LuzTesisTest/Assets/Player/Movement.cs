    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #pragma warning disable 649

    [SerializeField] CharacterController controller;
    [SerializeField] float spd = 11f;
    [SerializeField] float g = -30f;
    [SerializeField] LayerMask gndMask;
    bool isGrounded;

    Vector2 horizontalInput;
    Vector3 vVelocity = Vector3.zero;

    public void ReceiveInput (Vector2 hInput) {
        horizontalInput = hInput;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, gndMask);
        if (isGrounded) {
            vVelocity.y = 0;
        }

        Vector3 hVelocity = (transform.right * horizontalInput.x 
            + transform.forward * horizontalInput.y) * spd;
        vVelocity.y += g * Time.deltaTime;
        controller.Move(hVelocity * Time.deltaTime);
        controller.Move(vVelocity * Time.deltaTime);
    }
}
