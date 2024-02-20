using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float maxSpeed;
    public float acceleration;
    public Rigidbody2D myRigidbody;
    public float jumpForce;
    public float secondaryJumpForce;
    public float secondaryJumpDelay;
    public bool secondaryJump;
    public bool isGrounded;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", Mathf.Abs(myRigidbody.velocity.x));
        if (Mathf.Abs(myRigidbody.velocity.magnitude) < maxSpeed && Mathf.Abs(Input.GetAxis("Horizontal")) >= 0)
        {
            myRigidbody.AddForce(new Vector2(acceleration * Input.GetAxis("Horizontal"),0), ForceMode2D.Force);
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            myRigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            StartCoroutine(SecondaryJump());
            
        }
        if (secondaryJump && Input.GetButton("Jump") && !isGrounded)
        {
            myRigidbody.AddForce(new Vector2(0, secondaryJumpForce), ForceMode2D.Force);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }

    IEnumerator SecondaryJump()
    {
        secondaryJump = true;
        yield return new WaitForSeconds(secondaryJumpDelay);
        secondaryJump = false;
        yield return null;
    }
}
