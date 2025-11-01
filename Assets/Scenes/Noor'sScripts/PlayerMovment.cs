using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Sprite Facing")]
    public bool spriteFacesRightByDefault = true;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private bool facingRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        // Freeze Z rotation so the player won't fall over
        rb.freezeRotation = true;

        // Initialize facing direction
        facingRight = spriteFacesRightByDefault;
        if (sr) sr.flipX = !facingRight;
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Move the player
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Flip the sprite when moving left/right
        if (moveInput > 0 && !facingRight)
            Flip();
        else if (moveInput < 0 && facingRight)
            Flip();

        // --- SIMPLE ANIMATION CONTROL ---
        if (Input.GetButtonDown("Jump"))
        {
            // Apply jump force
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

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

    // --- FLIP METHOD (from your advanced version) ---
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
