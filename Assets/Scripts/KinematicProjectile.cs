/* This script is intended to be attached to a projectile GameObject.
 * Using a Rigidbody2D, the projectile will move using bodyType Kinematic, which means it will not be affected by physics forces
 * and will not collide with objects using physics. The projectile will be controlled by the player using OnMouseDrag, allowing the player to click and drag the projectile to set its position and direction drawing a tragectory. When the player clicks on the projectile, its alpha value will change to 0.7 to indicate that it is being dragged, and when the player releases the mouse button, the alpha value will return to 1. The script also calculates the direction of the projectile based on the starting position and the current position when the mouse button is released, which can be used to determine the trajectory of the projectile when it is launched. The Trajectory can be calculated using the direction vector and the speed variable, which can be set in the Unity Inspector. The projectile will then move in the direction of the calculated trajectory when launched. The Trajectory will then disappear when the projectile is launched, and the projectile will continue to move in the direction of the calculated trajectory until it collides with an object or goes off-screen. When the projectile collides with an object, it will switch to bodyType Dynamic, allowing it to be affected by physics forces and collide with other objects. Particle effects and sound effects will occur upon collision, aswell.
*/
using UnityEngine;

public class KinematicProjectile : MonoBehaviour
{
    // Variables
    Rigidbody2D rb;
    SpriteRenderer sr;
    
    public float speed;
    Vector2 startPos;
    Vector2 currentPos;
    Vector2 direction;
    Vector2 destinationPos;

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
        DrawTrajectory();

    }

    // Change Alpha Value of Sprite to 0.7 when you click on the projectile and change it back to 1 when you release the mouse button
    private void OnMouseDown()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.7f);
    }

    private void OnMouseUp()
    {
        currentPos = rb.position;
        direction = startPos - currentPos;
        direction.Normalize();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);

        LaunchProjectile();
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
    }

    // Launch the projectile in the direction of the calculated trajectory using the speed variable.
    // Since the projectile is Kinematic, we have to simulate gravity ourselves by calculating the parabolic trajectory of the projectile using the direction vector and the speed variable. The projectile will then move in the direction of the calculated trajectory until it collides with an object or goes off-screen. But it still needs to follow an arc, so we will use the formula for projectile motion to calculate the position of the projectile at each frame. The formula is:
    // x = v * t * cos(theta) (where v is the speed, t is the time, and theta is the angle of the trajectory) We are only using Kinematic Rigidbody, so we will not be using the physics engine to calculate the trajectory, but instead we will be calculating the position of the projectile at each frame using the formula for projectile motion. The projectile will then move in the direction of the calculated trajectory until it collides with an object or goes off-screen. When the projectile collides with an object, it will switch to bodyType Dynamic, allowing it to be affected by physics forces and collide with other objects. Particle effects and sound effects will occur upon collision, aswell.
    private void LaunchProjectile()
    {
        rb.linearVelocity = direction * speed;
    }

    private void DrawTrajectory()
    {
        // LineRenderer
        // We will use a LineRenderer to draw the trajectory of the projectile while it is being dragged. The LineRenderer will be updated in the Update() method to show the trajectory of the projectile based on the current position and the direction of the projectile. The LineRenderer will be disabled when the projectile is launched, and enabled when the projectile is being dragged.
        if (rb == null) return;
        LineRenderer lr = GetComponent<LineRenderer>();
        if (lr == null) return;
        lr.positionCount = 100;
        for (int i = 0; i < lr.positionCount; i++)
        {
            float t = i / (float)lr.positionCount;
            Vector2 pos = rb.position + direction * speed * t + 0.5f * Physics2D.gravity * t * t;
            lr.SetPosition(i, pos);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        // Add particle effects and sound effects here
    }
}
