using UnityEngine;

public class Projectile : MonoBehaviour
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
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
    }

}
