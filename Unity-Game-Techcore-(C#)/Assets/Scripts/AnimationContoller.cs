using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationContoller : MonoBehaviour
{

    // Stores Animator For Character
    private Animator animator;
    public bool Direction = false;
    public float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<PlayerHealth>().isAlive && gameObject.GetComponent<PlayerController>().enabled == true)
        {
            // Checks if player has a weapon
            if (this.transform.childCount > 2)
            {
                animator.SetBool("hasWeapon", true);
            }
            else
            {
                animator.SetBool("hasWeapon", false);
            }

            // Checks if Player is Running and that they ARE NOT jumping
            if (Input.GetAxis("Horizontal") != 0 && !Input.GetKey(KeyCode.Space))
            {
                animator.SetBool("isRunning", true);

            }
            else
            {
                animator.SetBool("isRunning", false);
            }

            // Checks if Player is Jumping
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (this.transform.childCount > 2)
                {
                    animator.Play("Jump2");
                }
                else
                {
                    animator.Play("Jump1");
                }
            }
            // Gets the playerContoller scripts and checks if we are in the attack phase
            if (transform.GetComponent<PlayerController>().canAttack)
            {
                animator.Play("Idle1");
            }
        }
                
    }
}
