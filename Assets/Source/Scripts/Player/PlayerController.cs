using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour
{
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    [Space(10)]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float wallJumpHeight = 10f;
    public float wallSlideSpeed = 0.5f;
    public float wallJumpWindow = 0.1f;
    [Space(10)]
    public int initialJumps = 1;
    public int jumpsLeft;
    private int facingDirection = 1;
    [Space(10)]
    public bool canAirJump;
    public bool controlDisabled = false;
    [Space(10)]
    public Collider2D groundCheckCollider;
    public Collider2D wallLeftCollider; // Add this line to reference the left wall collider
    public Collider2D wallRightCollider; // Add this line to reference the right wall collider
    private Rigidbody2D rb;
    private new Collider2D collider;
    [Space(10)]
    private PlayerAnimationController playerAnimationController;

private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        jumpsLeft = initialJumps;
        playerAnimationController = GetComponent<PlayerAnimationController>();
    }

    private void Update()
    {
        if (!controlDisabled)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

            bool isOnGround = groundCheckCollider.IsTouchingLayers(groundLayer);
            bool isTouchingWall = wallLeftCollider.IsTouchingLayers(wallLayer) || wallRightCollider.IsTouchingLayers(wallLayer); // Check ifcolliding with a wall using the left or right wall colliders
            bool canMoveHorizontally = !isTouchingWall || verticalInput > 0 || (horizontalInput > 0 && transform.position.x > collider.bounds.center.x) || (horizontalInput < 0 && transform.position.x < collider.bounds.center.x);

        if (isOnGround)
            {
                jumpsLeft = initialJumps;
                rb.velocity = movement;
            }
            else
            {
                rb.velocity = new Vector2(movement.x, rb.velocity.y - 0.1f);
            }

            if (Input.GetButtonDown("Jump") && (jumpsLeft > 0 || (isTouchingWall) || (canAirJump)))
            {
                if (isOnGround)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    jumpsLeft--;
                }
                else if (isTouchingWall)
                {
                    rb.velocity = new Vector2(2f * facingDirection * -1, wallJumpHeight);
                    jumpsLeft = initialJumps;
                }
                else if (canAirJump)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    jumpsLeft = initialJumps;
                    canAirJump = false;
                }
            }

        if (horizontalInput != 0)
            {
                facingDirection = (horizontalInput > 0) ? 1 : -1;
            }

            playerAnimationController.UpdateAnimation(movement, Mathf.Abs(horizontalInput), isTouchingWall);
        }


    }

    public void ResetVelocity()
    {
        rb.velocity = Vector2.zero;
    }
}