using UnityEngine;
using UnityEngine.EventSystems;

public class Slingshot : MonoBehaviour
{
    public static Slingshot Instance;
    public static Vector3 LaunchPos { get { if (Instance == null) return Vector3.zero; 
            return Instance.m_LaucnhPos; } }
    [SerializeField] private GameObject m_ProjectilePrefab;
    [SerializeField] private float m_Velocity = 8f;
    [SerializeField] private GameObject m_LaunchPoint;
    public GameObject LaunchPoint => m_LaunchPoint;
    private Vector3 m_LaucnhPos;
    private GameObject m_Projectile;
    private Rigidbody m_ProjectileRigid;
    private bool m_IsAimMode;
    private bool m_IsPaused = false;
    public bool IsPaused { get { return m_IsPaused; } set { m_IsPaused = value; } }
    private void Awake()
    {
        Instance = this;
        m_LaunchPoint.SetActive(false);
        m_LaucnhPos = m_LaunchPoint.transform.position;
    }
    private void Update()
    {
        if (!m_IsAimMode) return;
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 delta = mousePos3D - m_LaucnhPos;
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (delta.magnitude > maxMagnitude)
        {
            delta.Normalize();
            delta *= maxMagnitude;
        }
        Vector3 projectilePos = m_LaucnhPos + delta;
        m_Projectile.transform.position = projectilePos;
        if (Input.GetMouseButtonUp(0))
        {
            m_IsAimMode = false;
            m_ProjectileRigid.isKinematic = false;
            m_ProjectileRigid.velocity = -delta * m_Velocity;
            FollowCamera.FollowingObject = m_Projectile;
            m_Projectile = null;
            MissionDemolition.ShotFired();
            ProjectileLine.Instanse.Projectile = m_Projectile;
        }
        
    }
    private void OnMouseEnter()
    {
        m_LaunchPoint.SetActive(true);
    }
    private void OnMouseExit()
    {
        m_LaunchPoint.SetActive(false);
    }
    private void OnMouseDown()
    {
        if (m_IsPaused) return;
        m_IsAimMode = true;
        m_Projectile = Instantiate(m_ProjectilePrefab);
        m_Projectile.transform.position = m_LaucnhPos;
        m_ProjectileRigid = m_Projectile.GetComponent<Rigidbody>();
    }
}

