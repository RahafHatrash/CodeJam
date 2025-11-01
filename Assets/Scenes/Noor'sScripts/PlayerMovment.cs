using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Sprite Facing")]
    public bool spriteFacesRightByDefault = true;

    [Header("Grounding")]
    // Set this to your Ground layer in the Inspector (defaults to Everything if you forget).
    public LayerMask groundLayers = ~0;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private Collider2D col;
    private bool facingRight;

    // cache input so physics can use it in FixedUpdate
    float moveInput;
    bool jumpRequested;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr  = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        // Freeze Z rotation so the player won't fall over
        rb.freezeRotation = true;

        // Initialize facing direction
        facingRight = spriteFacesRightByDefault;
        if (sr) sr.flipX = !facingRight;
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        // Flip the sprite when moving left/right
        if (moveInput > 0 && !facingRight)      Flip();
        else if (moveInput < 0 && facingRight)  Flip();

        // Queue jump, animations stay exactly like you had them
        if (Input.GetButtonDown("Jump"))
        {
            jumpRequested = true;

            // Jump animation only
            anim.SetBool("Jump", true);
            anim.SetBool("Walk", false);
            anim.SetBool("Idle", false);
        }
        else if (Mathf.Abs(moveInput) > 0.1f)
        {
            // Walking animation
            anim.SetBool("Walk", true);
            anim.SetBool("Jump", false);
            anim.SetBool("Idle", false);
        }
        else
        {
            // Idle animation
            anim.SetBool("Idle", true);
            anim.SetBool("Walk", false);
            anim.SetBool("Jump", false);
        }
    }

    void FixedUpdate()
    {
        // Move using physics timestep
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Apply jump only if grounded this frame
        if (jumpRequested && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // consume the queued input every physics step
        jumpRequested = false;
    }

    bool IsGrounded()
    {
        // True if this collider is touching any of the chosen ground layers
        return col != null && col.IsTouchingLayers(groundLayers);
    }

    // --- FLIP METHOD (unchanged) ---
    void Flip()
    {
        facingRight = !facingRight;

        if (sr != null)
        {
            // This logic lets you define which direction is the sprite's default
            sr.flipX = !spriteFacesRightByDefault ? facingRight : !facingRight;
        }
        else
        {
            Vector3 s = transform.localScale;
            s.x *= -1f;
            transform.localScale = s;
        }
    }
}
