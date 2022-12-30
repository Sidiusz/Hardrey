using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 velocity;
    private float speed;
    private int facingDirection = 1;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateAnimation(Vector2 velocity, float speed, bool attacking)
    {
        this.velocity = velocity;
        this.speed = speed;
        animator.SetBool("IsMoving", velocity.x != 0);
        if (velocity.x != 0)
        {
            facingDirection = (velocity.x > 0) ? 1 : -1;
        }
        animator.SetBool("IsFacingRight", facingDirection == 1);
        animator.SetFloat("MovementSpeed", speed);
        animator.SetFloat("MovementDirection", velocity.x);
        spriteRenderer.flipX = facingDirection == -1;
    }
}
