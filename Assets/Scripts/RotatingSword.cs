using UnityEngine;
using System.Collections;

public class RotatingSword : MonoBehaviour
{
    public float rotationSpeed = 100f;   // Speed at which the sword rotates around the player
    public float lifetime = 5f;          // How long the sword lasts before disappearing
    public float cooldownTime = 5f;      // Cooldown time before the sword reappears
    public int damage = 10;              // Damage dealt to enemies on contact
    private bool isActive = false;       // Track if the sword is active

    void Start()
    {
        // Start the lifecycle of the sword when the game begins
        StartCoroutine(SwordLifecycle());
    }

    public void UpdateSwordScale(float newScale)
    {
        // Update the scale when leveling up
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    void Update()
    {
        if (isActive)
        {
            // Rotate the sword around the player
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    // Coroutine to handle the sword's lifecycle and cooldown
    public IEnumerator SwordLifecycle()
    {
        while (true)
        {
            // Activate the sword for its lifetime
            ActivateSword();
            yield return new WaitForSeconds(lifetime);

            // Deactivate the sword and wait for the cooldown
            DeactivateSword();
            yield return new WaitForSeconds(cooldownTime);
        }
    }

    // Activate the sword (show and enable collisions)
    private void ActivateSword()
    {
        isActive = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    // Deactivate the sword (hide and disable collisions)
    private void DeactivateSword()
    {
        isActive = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    // Use OnTriggerStay2D to deal continuous damage to enemies
    void OnTriggerStay2D(Collider2D hitInfo)
    {
        if (isActive)
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);  // Deal damage to enemies when in contact
            }
        }
    }
}
