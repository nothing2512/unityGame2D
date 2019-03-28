/**
 * Player Movement
 * @author Robet Atiq Maulana Rifqi
 * MAR 2019
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /**
     * Declaring variable
     */
	 
    public Rigidbody2D rb;
    public Transform groundPoint;
    public LayerMask groundLayer;
    public Animator animator;

    public float speed = 2f;
    public float jumpForce = 6f;

    private bool onJump = false;
    private float mDir = 1;
    private bool onFly = false;
	
	private void start() {
        Constants.gameState = Constants.GameState.Play;
	}

    private void Update()
    {
		// cek state
		if (Constants.gameState == Constants.GameState.Play )
		{
			// call handle movement
			HandleMovement();

			// call handle jump
			HandleJump();

			// call handle animation
			HandleAnimation();

			// call handle air 
			HandleAir();

			// call handle fly
			HandleFly();
		} else if (Constants.gameState == Constants.GameState.Lose )
		{
            animator.Play("Character death");
		} else 
		{
			animator.Play("Character Idle");
		}

        if ( ! onFly && Constants.maxFly > Constants.flyPower )
        {
            Constants.flyPower += 0.05f;
        }
    }

    /**
     * handle fly function
     */
    private void HandleFly()
    {
        // Check space pressed and not jump
        if (Input.GetKey(KeyCode.Space) && !onJump && Constants.flyPower > 0)
        {
            // Set fly to true
            onFly = true;

            // flying player
            rb.AddForce(Vector3.up / 2, ForceMode2D.Impulse);

            // decrement max fly
            Constants.flyPower -= 1f;
        }
    }

    /**
     * handle movement function
     */
    private void HandleMovement()
    {
        // Get player movement direction
        float dir = Input.GetAxisRaw("Horizontal");

        // Set Main Direction
        mDir = dir == 0 ? mDir : dir;

        // handle player rotation
        HandleRotation(dir);

        // moving player
        rb.velocity = new Vector3(dir * speed, rb.velocity.y);
    }

    /**
     * handle player rotation function
     * 
     * @param float
     */
    private void HandleRotation(float dir)
    {
        float angle = dir > 0 ? 0f : 180f;

        // rotating enemy
        transform.rotation = dir != 0 ? Quaternion.AngleAxis(angle, Vector3.up) : transform.rotation;
    }

    /**
     * handle player jump function
     */
    private void HandleJump()
    {
        // check up arrow pressed and checking if player is inground
        if (Input.GetKeyDown(KeyCode.UpArrow) && GroundCheck())
        {
            // jumping player
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);

            // set onJump ( player on air )
            onJump = true;
        }
    }

    /**
     * check ground function
     * 
     * @return bool
     */
    private bool GroundCheck()
    {
        // returning status player is in ground or not
        return Physics2D.OverlapCircle(groundPoint.position, 0.5f, groundLayer);
    }

    /**
     * handle animation function
     */
    private void HandleAnimation()
    {
        // check if is not jump and not fly
        if (!onJump && !onFly)
        {
            // if player not move, play animation player idle
            if (Mathf.Abs(rb.velocity.x) <= 0 ) animator.Play("Character Idle");

            // if player move, play animation player move
            else animator.Play("Character Walk");
        }

        // check if player is jump
        else if ( onJump && !onFly )
        {
            // play animation player jump
            animator.Play("Character Jump");
        } else if( !onJump && onFly )
        {
            // play animation player fly
            animator.Play("Character Fly");
        }
    }

    /**
     * checking player collision function
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {

        string tag = collision.gameObject.tag;
        // check if player is collision with Ground
        if (tag.Equals("Ground"))
        {
            // set not jump
            onJump = false;

            // set not fly
            onFly = false;
        }

        // check if player is collision with finish object
        if ( tag.Equals("Finish"))
        {
            Constants.gameState = Constants.GameState.Win;
        }

        // check if player is collision with enemy object
        if ( tag.Equals("Enemy"))
        {

            // set player hitted
            Constants.playerHited = true;

            //Constants.gameState = Constants.GameState.Lose;
        }
    }

    /**
     * handle movement in air function
     */
    private void HandleAir()
    {
        // check if player jump or not
        if ( onJump )
        {
            // set player movement right / left in air
            rb.velocity = new Vector3(mDir * speed, rb.velocity.y);
        }
    }
}
