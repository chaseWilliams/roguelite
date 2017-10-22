using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerOldSchool : MonoBehaviour
{

    public float jumpForce = 800f;
    public float moveSpeed = 5f;

	bool isGrounded = false;

	Rigidbody2D rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{

		float h_input = Input.GetAxisRaw("Horizontal");
		float v_input = Input.GetKeyDown(KeyCode.UpArrow) ? 1 : 0;

        // set horizontal velocity
        if (Mathf.Abs(h_input) > Mathf.Epsilon) {
            float x_velocity = Mathf.Sign(h_input) * moveSpeed;
            rb.velocity = new Vector2(x_velocity, rb.velocity.y);
        }

        // jump
		if (v_input == 1 && isGrounded)
		{
			rb.AddForce(new Vector2(0, jumpForce));
			isGrounded = false;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		isGrounded = true;
	}

}

