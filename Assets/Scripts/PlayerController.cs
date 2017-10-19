﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float jumpForce = 800f;
    public float moveForce = 5000f;
    public float maxSpeed = 5f;

    bool isGrounded = false;

    Rigidbody2D rb;

	void Awake () {
        rb = GetComponent<Rigidbody2D>();
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
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

}
