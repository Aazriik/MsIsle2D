using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            // Play POOF animation here

            // Disable Enemy GameObject
            gameObject.SetActive(false);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // Play POOF animation here

            // Disable Enemy GameObject
            gameObject.SetActive(false);
        }
    }
}
