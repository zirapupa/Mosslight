using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private SpriteRenderer sprite;

    public InputAction MoveAction;
    public InputAction JumpAction;

    public float MoveSpeed = 5f;
    public float JumpForce = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        MoveAction.Enable();
        JumpAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        bool isGrounded = Math.Abs(rb2d.linearVelocityY) < 0.1f;
        bool jump = JumpAction.WasPressedThisFrame();

        if (jump && isGrounded)
        {
            rb2d.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        Vector2 move = MoveAction.ReadValue<Vector2>();

        Vector2 velocity = rb2d.linearVelocity;
        rb2d.linearVelocity = new Vector2(move.x * MoveSpeed, rb2d.linearVelocityY);
        if (move.x > 0)
            sprite.flipX = true;
        else if (move.x < 0)
            sprite.flipX = false;
    }
}
