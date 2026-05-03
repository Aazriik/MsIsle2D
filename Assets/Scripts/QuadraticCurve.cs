using UnityEngine;

public class QuadraticCurve : MonoBehaviour
{
    // Variables
    public Transform A;             // Start Point
    public Transform B;             // End Point
    public Transform Control;       // Curve Control Point (Parabolic Curve/Discriminant/Root of the curve)

    // Evaluate the curve at a given time t (0 <= t <= 1)
    public Vector3 evaluate(float t)
    {
        // The formula for a quadratic Bezier curve is: P(t) = (1-t)^2 * A + 2(1-t)t * Control + t^2 * B
        // We can break this down into two linear interpolations (Lerp):
        // First, we calculate the points on the lines A-B and then put Control in the center.
        // Then, we interpolate between those two points to get the final point on the curve at time t.
        
        Vector2 ab = Vector3.Lerp(A.position, B.position, t);
        Vector2 ac = Vector3.Lerp(A.position, Control.position, t);
        Vector2 cb = Vector3.Lerp(Control.position, B.position, t);
        return Vector3.Lerp(ac, cb, t);

        //Vector3 ac = Vector3.Lerp(A.position, Control.position, t);
        //Vector3 cb = Vector3.Lerp(Control.position, B.position, t);
        //return Vector3.Lerp(ac, cb, t);
    }

    private void OnDrawGizmos()
    {
        if (A == null || B == null || Control == null) {return;}

        for (int i = 0; i < 20; i++)
        {
            Gizmos.DrawWireSphere(evaluate(i / 20f), 0.1f);
            Gizmos.color = Color.red;
        }
    }
}
