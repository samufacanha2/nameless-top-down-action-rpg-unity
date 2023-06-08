using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public enum Direction
    {
        up = 0,
        right = 1,
        down = 2,
        left = 3
    }

    public float move_speed = 1f;
    public float collision_offset = 0.02f;
    public ContactFilter2D movement_filter;
    public bool is_attacking = false;
    public bool is_flipped = false;

    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Animator animator;
    SpriteRenderer spriteRenderer;

    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (is_attacking)
        {
            return;
        }

        TryMove(movementInput);
        GetFacingDirection(movementInput);
    }



    void TryMove(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            animator.Play("Player_Idle");
            return;
        }


        float movementOffset = move_speed * Time.fixedDeltaTime;

        Vector2 x_movement = new Vector2(direction.x, 0);
        Vector2 y_movement = new Vector2(0, direction.y);

        Vector2 new_movement = new Vector2(0, 0);



        int collisionCountX = rb.Cast(
            x_movement,
            movement_filter,
            castCollisions,
            movementOffset + collision_offset
            );

        int collisionCountY = rb.Cast(
            y_movement,
            movement_filter,
            castCollisions,
            movementOffset + collision_offset
            );

        if (collisionCountX == 0)
        {
            new_movement.x += direction.x;
            animator.SetFloat("Move_X", direction.x);
        }

        if (collisionCountY == 0)
        {
            new_movement.y += direction.y;
            animator.SetFloat("Move_Y", direction.y);
        }

        animator.Play("Player_Moving");

        rb.MovePosition(rb.position + new_movement * movementOffset);


    }

    void GetFacingDirection (Vector2 direction)
    {
        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
            is_flipped = true;
        }
        else if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
            is_flipped = false;

        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }


    void StartAttack()
    {
        is_attacking = true;
    }

    void OnFire()
    {

        animator.Play("Player_Attacking");

    }
}
