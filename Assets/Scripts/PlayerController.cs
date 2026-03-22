using System;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private SpriteRenderer sprite;

    public InputAction MoveAction;
    public InputAction JumpAction;

    public float speed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        MoveAction.Enable();
        JumpAction.Enable();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isGrounded = Math.Abs(rb2d.linearVelocityY) < 0.1f;
        bool jump = JumpAction.WasPressedThisFrame();

        if (jump && isGrounded)
        {
            rb2d.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        Vector2 move = MoveAction.ReadValue<Vector2>();

        Vector2 velocity = rb2d.linearVelocity;
        velocity.x = move.x * speed;
        rb2d.linearVelocity = velocity;

        if (move.x > 0)
        {
            sprite.flipX = false;
        }
        else if (move.x < 0)
        {
            sprite.flipX = true;
        }
    }
}
