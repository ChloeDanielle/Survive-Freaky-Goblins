using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int maxHealth = 100;
    public int currentHealth;
    public int attackDamage = 10;
    public float attackRange = 1.5f;
    public float attackCooldown = 1.5f;
    private Transform player;  
    public Animator animator;
    private float nextAttackTime = 0f;
    private bool isFacingRight = true;

    public int experiencePoints = 20; // Add experience points for player to gain on death

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        MoveTowardsPlayer();
        FlipTowardsPlayer();

        if (Time.time >= nextAttackTime && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            AttackPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        if (Vector2.Distance(transform.position, player.position) > attackRange)
        {
            Vector2 targetPosition = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void FlipTowardsPlayer()
    {
        if (player.position.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }
        else if (player.position.x < transform.position.x && isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void AttackPlayer()
    {
        animator.SetTrigger("Attack");
        nextAttackTime = Time.time + attackCooldown;

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        // Grant experience to player on death
        PlayerLevel playerLevel = player.GetComponent<PlayerLevel>();
        if (playerLevel != null)
        {
            playerLevel.GainExperience(experiencePoints);
        }

        // Update the player's kill count
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.AddKill();  // Increment the enemy kill count
        }

        Destroy(gameObject, 2f);  // Destroy the enemy after 2 seconds
    }
}
