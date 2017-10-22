using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float jumpForce = 800f;
    public float moveForce = 5000f;
    public float maxSpeed = 5f;
    public float wallSlideSpeed = -3f;

    bool isGrounded = false;
    bool isWallSliding = false;
    // 1 is right, -1 is left
    Vector2 wall_slide_jump_direction;


    Rigidbody2D rb;
    BoxCollider2D left_side;
    BoxCollider2D right_side;
    CircleCollider2D feet; 

	void Awake () {
        rb = GetComponent<Rigidbody2D>();
        left_side = transform.Find("LeftSide").GetComponent<BoxCollider2D>();
        right_side = transform.Find("RightSide").GetComponent<BoxCollider2D>();
        feet = transform.Find("Feet").GetComponent<CircleCollider2D>();
	}
	
	void Update () {

        float h_input = Input.GetAxisRaw("Horizontal");
        float v_input = Input.GetKeyDown(KeyCode.UpArrow) ? 1 : 0;

        rb.AddForce(new Vector2(h_input * Time.deltaTime * moveForce, 0));

        if (Mathf.Abs(rb.velocity.x) > maxSpeed) {
            float x_velocity = Mathf.Sign(rb.velocity.x) * maxSpeed;
            rb.velocity = new Vector2(x_velocity, rb.velocity.y);
        }

        if (v_input == 1 && isGrounded) {
            rb.AddForce(new Vector2(0, jumpForce));
            isGrounded = false;
            isWallSliding = false;
        }

        if (v_input == 1 && isWallSliding) {
            Debug.Log(Vector2.Angle(Vector2.right, wall_slide_jump_direction));
            rb.AddForce(wall_slide_jump_direction * jumpForce * 1.5f);
            isWallSliding = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("entering collision");
        if (collision.otherCollider == feet)
        {
            isGrounded = true;
        }

        rb.velocity = new Vector2(rb.velocity.x, wallSlideSpeed);


        if (collision.otherCollider == left_side)
        {
            isWallSliding = true;
            SetWallJumpDirection(70);
        }

        if (collision.otherCollider == right_side)
        {
            isWallSliding = true;
            SetWallJumpDirection(110);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.otherCollider == left_side || collision.otherCollider == right_side) {
            isWallSliding = false;
        }
    }

    void SetWallJumpDirection(float angle) {
        angle = angle * Mathf.PI / 180f;
        wall_slide_jump_direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        Debug.Log(wall_slide_jump_direction);
    }

}
