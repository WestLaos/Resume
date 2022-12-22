using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpTest : MonoBehaviour
{
    private Rigidbody playerRb;
    public int maxJumps = 2;
    public int jumps;
    public bool isOnGround = true;
    public bool canMove;
    public float jumpForce = 100;
    public float gravityModifier = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        canMove = true;
    }

    // Update is called once per frame
    //void Update()
    //{
    //}

    // Use FixedUpdate for physics engine related events
    void Update()
    {
        // Jump with space key
        // NOTE: Make sure that the player object has a RigidBody component with gravity enabled!
        if (Input.GetKeyDown(KeyCode.Space) && canMove)
        {
            Jump();
            //playerHealth.TakeDamage(10);
        }
    }

    // Method to control the double jump mechanic
    private void Jump()
    {
        if (jumps > 0)
        {
            Debug.Log("FORCE APPLIED");
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            jumps = jumps - 1;
        }
        if (jumps == 0)
        {
            return;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumps = maxJumps;
            isOnGround = true;
        }
    }
}
