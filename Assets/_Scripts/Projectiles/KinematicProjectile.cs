using UnityEngine;
using System.Collections;

public class KinematicProjectile : MonoBehaviour
{
    #region Variables
    Rigidbody2D rb;
    SpriteRenderer sr;
    TrailRenderer trail;
    Collider2D collider2D;

    public float speed;
    public float maxSpeed = 15f;
    public float speedMultiplier = 3f;
    public float maxDrag = 2f;
    private bool isLaunched = false;
    public float gravityScale = 5f;

    // Projectiles.
    public bool shoGon = false;
    public bool bigBertha = false;
    private bool shogonTemp = false;

    public GameObject shogunPrefab;
    public int shardCount = 2;

    // Position References
    Vector2 startPos;
    Vector2 currentPos;
    Vector2 direction;

    bool isDragging = false;
    #endregion

    #region Input Manager
    void OnEnable()
    {   InputManager.Instance.OnTouchBegin += OnTouchBegin;
        InputManager.Instance.OnTouchEnd += OnTouchEnd;
    }

    void OnDisable()
    {
        InputManager.Instance.OnTouchBegin -= OnTouchBegin;
        InputManager.Instance.OnTouchEnd -= OnTouchEnd;
    }
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        trail = GetComponent<TrailRenderer>();
        collider2D = GetComponent<Collider2D>();
        trail.enabled = false;
        startPos = rb.position;
        rb.bodyType = RigidbodyType2D.Kinematic;

    }

    // Update is called once per frame
    void Update()
    {
        if (isLaunched)
        {
            // Add Gravity to the projectile so that it follows a parabolic trajectory
            // rb.AddForce uses the Physics Engine, butt since we are using a Kinematic Rigidbody, we will manually apply gravity by modifying the rb.linearVelocity of the Rigidbody2D. We will add a downward force to the linear velocity to simulate gravity. Then we will multiply it by Time.deltaTime to make it frame rate independent. Finally, we will multiply it by gravityScale to adjust the strength of the gravity.
            rb.linearVelocity += Vector2.down * gravityScale * Time.deltaTime;
            // Set rotation of the projectile to the direction of the trajectory
            // We will use the Atan2 function to calculate the angle of the trajectory based on the linear velocity of the Rigidbody2D. The Atan2 function takes the y and x components of the linear velocity and returns the angle in radians. We will then convert it to degrees by multiplying it by Mathf.Rad2Deg. We will also check if the y component of the linear velocity is greater than 0, which means the projectile is moving upwards, and if so, we will add 180 degrees to the angle to make it face downwards.
            float angle = direction.y > 0 ? Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x)
                * Mathf.Rad2Deg : Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg + 180f;
            rb.SetRotation(angle);

            if (shoGon)
            {
                // Check if the projectile is falling, then call the Sotgun method.
                if (rb.linearVelocity.y < 0)
                {
                    Shotgun();
                    shoGon = false;
                    shogonTemp = true;
                }
            }
        }

        if (isDragging)
        {
            OnTouchDrag();
        }
    }

    // Change Alpha Value of Sprite to 0.7 when you click on the projectile and change it back to 1 when you release the mouse button
    private void OnTouchBegin() // OnTouchBegin. Call DRAG function in update.
    {
        // Grab Touch World Position.
        Vector3 touchPos = InputManager.Instance.GetTouchWorldPosition();
        // Raycast from camera to touch pos and see if you hit the colider, then accept dragging input.

        Collider2D hitCollider = Physics2D.OverlapPoint(touchPos);

        if (hitCollider == null)
            return;

        if (!isLaunched && (hitCollider = collider2D))
        {
            // Change Alpha Value of Sprite to 0.7
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.7f);
            //isDragging = true;
        }
    }

    private void OnTouchEnd() //onTouchEnd. Call LAUNCH function in update.
    {
        if (!isLaunched)
        {
            isDragging = false;
            currentPos = rb.position;
            direction = startPos - currentPos;
            direction.Normalize();

            // Change Alpha Value of Sprite to 1.
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
            // Launch Projectile.
            LaunchProjectile();
        }
        else
            return;
    }

    private void OnTouchDrag()
    {
        // Get the mouse position in world space and set the projectile's position to the mouse position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(InputManager.Instance.GetTouchScreenPosition());
        Vector2 desiredPos = mousePos;

        isDragging = true;

        float distance = Vector2.Distance(desiredPos, startPos);
        if (distance > maxDrag)
        {
            direction = desiredPos - startPos;
            direction.Normalize();
            desiredPos = startPos + (direction * maxDrag);
        }

        if (desiredPos.x > startPos.x)
        {
            desiredPos.x = startPos.x;
        }

        rb.position = desiredPos;

        // Based on the distance between the start position and the mouse position, calculate the speed of the projectile
        speed = Vector2.Distance(startPos, mousePos) * speedMultiplier; // Adjust the multiplier as needed for desired speed
        // Clamp speed to maxSpeed
        speed = Mathf.Clamp(speed, 0f, maxSpeed);
    }

    // x = v * t * cos(theta) (where v is the speed, t is the time, and theta is the angle of the trajectory)
    private void LaunchProjectile()
    {
        isLaunched = true;
        // Set the Trail component to be enabled so that it starts rendering the trail of the projectile
        trail.enabled = true;
        // Set the projectile's velocity to the direction multiplied by the speed
        rb.linearVelocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        speed = 0f;
        // Disable the Trail component so that it stops rendering the trail of the projectile.
        trail.enabled = false;
        // Add particle effects and sound effects here.
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
        isLaunched = false;
        if (shogonTemp)
        {
            shogonTemp = false;
            shoGon = true;
        }
    }

    private void Shotgun()
    {
        for (int i = 0; i < shardCount; i++)
        {
            GameObject shard = Instantiate(
                shogunPrefab,
                transform.position,
                Quaternion.identity
            );

            Rigidbody2D shardRb = shard.GetComponent<Rigidbody2D>();

            if (shardRb != null)
            {
                Vector2 randomDirection = new Vector2(Random.Range(1f, 2f), Random.Range(1f, -2f)).normalized;
                shardRb.linearVelocity = randomDirection * speed;
            }
        }
    }
}