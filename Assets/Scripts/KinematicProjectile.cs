using UnityEngine;
using System.Collections;

public class KinematicProjectile : MonoBehaviour
{
    // Variables
    Rigidbody2D rb;
    SpriteRenderer sr;
    public float speed;

    // Position References
    Vector2 startPos;
    Vector2 currentPos;
    Vector2 direction;
    Vector2 endPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        startPos = rb.position;
        rb.bodyType = RigidbodyType2D.Kinematic;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Change Alpha Value of Sprite to 0.7 when you click on the projectile and change it back to 1 when you release the mouse button
    private void OnMouseDown()
    {
        // Change Alpha Value of Sprite to 0.7
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.7f);
    }

    private void OnMouseUp()
    {
        currentPos = rb.position;
        direction = startPos - currentPos;
        direction.Normalize();

        rb.AddForce(direction * speed, ForceMode2D.Impulse);

        // Change Alpha Value of Sprite to 1
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);

        LaunchProjectile();
    }

    private void OnMouseDrag()
    {
        // Get the mouse position in world space and set the projectile's position to the mouse position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePos.x, mousePos.y);

        // Based on the distance between the start position and the mouse position, calculate the speed of the projectile
        speed = Vector2.Distance(startPos, mousePos) * 3f; // Adjust the multiplier as needed for desired speed
    }

    // x = v * t * cos(theta) (where v is the speed, t is the time, and theta is the angle of the trajectory)
    private void LaunchProjectile()
    {
        float gravity = -9.8f;
        float mass = 1f;
        // Set the projectile's velocity to the direction multiplied by the speed
        rb.linearVelocity = direction * speed;
        // Add Gravity to the projectile so that it follows a parabolic trajectory
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        // Add particle effects and sound effects here

        // Check if we are moving. If we're not, then start coroutine.
        StartCoroutine(RestartDelay());
    }

    IEnumerator RestartDelay()
    {
        // Reset the projectile position after 5 seconds and set the body type back to Kinematic
        yield return new WaitForSeconds(2f);
        rb.position = startPos;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
    }
}
