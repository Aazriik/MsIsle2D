using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Variables
    public QuadraticCurve curve;
    public float speed;

    private float sampleTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // sampleTime is set to 0f because we want to start at the beginning of the curve.
        sampleTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // sampleTime is the time variable that we will use to evaluate the curve. We will increment it by Time.deltaTime * speed to move the projectile along the curve at a consistent speed. This is called "sampleTime" because it is the TIME variable that SAMPLES the curve.
        sampleTime += Time.deltaTime * speed;
        // Clamp sampleTime to 1f to prevent it from going beyond the end of the curve.
        transform.position = curve.evaluate(sampleTime);
        // Prevent rotation of the projectile since this is a 2D game.
        // transform.RIGHT because that's the X-Axis. If we did FORWARD, that would be the Z-Axis, which is not what we want in a 2D game. The sprite would be facing sideways, so it would be invisible to the camera.
        transform.right = curve.evaluate(sampleTime + 0.001f) - transform.position;

        // When sampleTime reaches 1f, it means the projectile has reached the end of the curve.
        if (sampleTime >= 1f)
        {
            Debug.Log ("Projectile reached the end of the curve.");

        }
    }
}
