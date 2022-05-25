using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool jumpKeyWasPressed;
    private bool shiftKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidBodyComponent;
    private int shiftSpeed=1;
    [SerializeField] private LayerMask playerMask;
    // Alternatively below line can be achieved in the following way
    // public Transform groundCheckTransform;
    
    [SerializeField] private Transform groundCheckTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            Debug.Log("Space was Pressed");
            jumpKeyWasPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) == true)
        {
            shiftKeyWasPressed = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            shiftKeyWasPressed = false;
        }
        horizontalInput = Input.GetAxis("Horizontal");
    }

    // FixedUpdate is called once after every PhysicsUpdate
    // Always apply physics in FixedUpdate method
    private void FixedUpdate()
    {
        rigidBodyComponent.velocity = new Vector3(horizontalInput * shiftSpeed, rigidBodyComponent.velocity.y, rigidBodyComponent.velocity.z);
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length==0)
        {
            return;
        }

        if (jumpKeyWasPressed)
        {
            rigidBodyComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
        if (shiftKeyWasPressed)
        {
            shiftSpeed = 2;
        }
        else
        {
            shiftSpeed = 1;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Destroy(other.gameObject);
        }
    }
}
