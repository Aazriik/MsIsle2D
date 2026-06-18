using UnityEngine;
using System.Collections;

public class ShoGunShards : MonoBehaviour
{
    Rigidbody2D rb;
    TrailRenderer trail;
    public float gravityScale = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        trail = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity += Vector2.down * gravityScale * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        // Disable the Trail component so that it stops rendering the trail of the projectile
        trail.enabled = false;
        // Add particle effects and sound effects here

        // Check if we are moving. If we're not, then start coroutine.
        StartCoroutine(DestroyDelay());
    }

    IEnumerator DestroyDelay()
    {
        // Reset the projectile position after 5 seconds and set the body type back to Kinematic
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
