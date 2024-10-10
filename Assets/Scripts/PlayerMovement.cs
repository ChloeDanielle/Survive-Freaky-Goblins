using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;                  // Movement speed
    public Animator animator;                     // Reference to the Animator
    public Rigidbody2D rb;                        // Reference to the Rigidbody2D
    public Transform attackPointRight;            // Attack point when facing right
    public Transform attackPointLeft;             // Attack point when facing left
    public float attackRange = 1f;                // Range of the attack
    public LayerMask enemyLayers;                 // Layers to detect enemies
    public int attackDamage = 20;                 // Damage dealt to enemies

    private Vector2 movement;
    private bool isFacingRight = true;

    public GameObject levelUpUI;  // Reference to the Level Up UI

    void Update()
    {
        // Get input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Set animator parameters
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // **Check if the Level Up UI is not active before attacking**
        if (Input.GetKeyDown(KeyCode.Space) && !levelUpUI.activeInHierarchy)
        {
            Attack();
        }

        // Flip player sprite based on movement direction
        if (movement.x < 0 && isFacingRight)
            Flip(false);  // Face left
        else if (movement.x > 0 && !isFacingRight)
            Flip(true);  // Face right
    }

    void FixedUpdate()
    {
        // Move the player
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Attack()
    {
        // Play attack animation
        animator.SetTrigger("Attack");

        // Use the correct attack point depending on the direction the player is facing
        Transform activeAttackPoint = isFacingRight ? attackPointRight : attackPointLeft;

        // Detect enemies in range of the attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(activeAttackPoint.position, attackRange, enemyLayers);

        // Damage each enemy
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void Flip(bool facingRight)
    {
        isFacingRight = facingRight;

        // Flip player sprite by changing scale
        Vector3 localScale = transform.localScale;
        localScale.x = facingRight ? 0.5f : -0.5f;  // Adjust the scale for left or right
        transform.localScale = localScale;
    }

    // Optional: Draw the attack range in the Scene view (for visualization)
    void OnDrawGizmosSelected()
    {
        if (attackPointRight == null || attackPointLeft == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPointRight.position, attackRange);  // Draw for right attack point
        Gizmos.DrawWireSphere(attackPointLeft.position, attackRange);   // Draw for left attack point
    }
}
