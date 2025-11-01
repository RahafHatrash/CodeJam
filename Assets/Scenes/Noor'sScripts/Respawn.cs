using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 startPosition; // Player’s initial position
    private Rigidbody2D rb;        // Player's Rigidbody (for physics reset)
    [SerializeField] private float fallThreshold = -10f; // How far they can fall before respawning

    void Start()
    {
        // Store the player’s starting position and Rigidbody
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // If the player falls too low, respawn them
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        // Move the player back to the start
        transform.position = startPosition;

        // Stop any movement from before the fall
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}



