using UnityEngine;

public class Launcher : MonoBehaviour
{
    // Variables
    public Transform launchPoint;
    public GameObject[] projectiles;
    public float launchSpeed;

    // Trajectory Settings
    LineRenderer lineRenderer;
    public int trajectoryPoints = 100;
    public float timeBetweenPoints = 0.1f;

    // Position References
    Vector2 startPos;
    public Vector2 endPos;
    public Vector2 currentPos;
    public Vector2 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DrawTrajectory()
    {
        // LineRenderer
        Vector2 origin = launchPoint.position;
        Vector2 startVelocity = launchSpeed * direction;
        lineRenderer.positionCount = trajectoryPoints;
        float time = 0;

        for (int i = 0; i < trajectoryPoints; i++)
        {
            // s = u*t + 1/2*g*t*t
            var x = (startVelocity.x * time) + (Physics.gravity.x * 0.5f) * time * time;
            var y = (startVelocity.y * time) + (Physics.gravity.y * 0.5f) * time * time;
            Vector2 point = new Vector2(x, y);
            lineRenderer.SetPosition(i, origin + point);
            time += timeBetweenPoints;
        }
    }
}
