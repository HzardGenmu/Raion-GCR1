using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float dashSpeed;
    public float dashCooldown;
    public float dashTime;

    private Rigidbody2D rB; //reference to the Rigidbody2D
    //private Animator anim;

    private bool isJumping = false;
    public bool facingRight = true;
    private bool grounded;
    private bool isDashing;
    private bool canDash;
    private float moveDirection;
    [SerializeField] private int extraJump;
    private int jumpCounter;

    private float originalMoveSpeed; 
    private bool isBoosted = false;  

    // Awake is called after all objects are initiallized. Called in a random order.
    private void Awake()
    {
        rB = GetComponent<Rigidbody2D>(); // Will look for the component on this GameObject(What the script is attached to) of type rigidbody.
        //anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update 
    void Start()
    {
        canDash = true;
        originalMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        //Input
        ProcessInput(); //

        //Animate 
        Animation(); //switch character direction

        Jump();

        if (grounded)
            jumpCounter = extraJump;
    }

    //Better for handling update, can be called multiple times per update
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        //Move
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    //=====================================================================================================================

    private void Animation()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
       /* anim.SetBool("Run", moveDirection != 0); // animation for running
        anim.SetBool("Grounded", grounded);// animation for checking whether the character is touching object tagged "Ground"
        anim.SetBool("Dash", isDashing);*/
    }

    private void Move()
    {
        rB.velocity = new Vector2(moveDirection * moveSpeed, rB.velocity.y);
    }

    public void BoostSpeed(float multiplier, float duration)
    {
        if (!isBoosted)  // Prevent overlapping boosts
        {
            StartCoroutine(BoostCoroutine(multiplier, duration));
        }
    }

    // Coroutine to manage the speed boost
    private IEnumerator BoostCoroutine(float multiplier, float duration)
    {
        isBoosted = true;
        moveSpeed *= multiplier; // Double the speed
        yield return new WaitForSeconds(duration); // Wait for the duration
        moveSpeed = originalMoveSpeed; // Reset speed to original value
        isBoosted = false;
    }

    private void ProcessInput()
    {
        moveDirection = Input.GetAxisRaw("Horizontal"); //scale of (-1) to 1
        Dashing(); //To Dash
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);

    }

    private void Jump()
    {

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Lompat Gak?");
            if (grounded)
            {
                isJumping = true;
                grounded = false;
                //anim.SetTrigger("Jump");
                if (isJumping)
                {
                    rB.AddForce(new Vector2(0f, jumpForce)); // (x,y) with the jumpforce being added in the project
                }
            }
            else
            {
                if (jumpCounter > 0)
                {
                    rB.AddForce(new Vector2(0f, jumpForce));
                    jumpCounter--;
                }
            }
        }
    }

    private void Dashing()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            Debug.Log("Dash");
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {

        canDash = false;
        //anim.SetTrigger("Dash");
        isDashing = true;
        rB.velocity = new Vector2(moveDirection * dashSpeed, rB.velocity.y);

        yield return new WaitForSeconds(dashTime);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public bool canAttack()
    {
        return moveDirection == 0;
    }

}