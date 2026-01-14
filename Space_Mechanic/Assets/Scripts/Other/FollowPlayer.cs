using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    // Camera bounds
    public float minX, maxX; // left and right bounds
    public float minY, maxY; // bottom and top bounds

    void LateUpdate()
    {
        if (target == null)
            return;

        // Desired position based on player + offset
        Vector3 desiredPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            transform.position.z
        );

        // Smoothly move the camera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Clamp the camera inside the bounds
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minX, maxX);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minY, maxY);

        transform.position = smoothedPosition;
    }
}


