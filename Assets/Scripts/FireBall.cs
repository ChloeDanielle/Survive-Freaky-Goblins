using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour
{
    public float speed = 5f;  // Speed of the fireball
    public int damage = 20;   // Damage dealt by the fireball

    void Start()
    {
        // Move the fireball in its current direction
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);  // Deal damage to the enemy
            Destroy(gameObject);       // Destroy the fireball after hitting an enemy
        }
    }
}
