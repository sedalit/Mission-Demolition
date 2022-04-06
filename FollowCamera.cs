using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public static GameObject FollowingObject;

    private float m_CameraOffset;
    private float m_Easing = 0.05f;
    private Vector2 m_CameraLimitation = Vector2.zero;
    private void Awake()
    {
        m_CameraOffset = transform.position.z;
    }
    private void FixedUpdate()
    {
        Vector3 destination;
        if (FollowingObject == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            destination = FollowingObject.transform.position;
            if (FollowingObject.tag == "Projectile")
            {
                if (FollowingObject.GetComponent<Rigidbody>().IsSleeping()) 
                {
                    FollowingObject = null;
                    return;
                }
            }
        }
        destination.x = Mathf.Max(m_CameraLimitation.x, destination.x);
        destination.y = Mathf.Max(m_CameraLimitation.y, destination.y);
        destination = Vector3.Lerp(transform.position, destination, m_Easing);
        destination.z = m_CameraOffset;
        transform.position = destination;
        Camera.main.orthographicSize = destination.y + 10;
    }
}
