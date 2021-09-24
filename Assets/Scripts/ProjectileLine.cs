using UnityEngine;
using System.Collections.Generic;

public class ProjectileLine : MonoBehaviour
{
    public static ProjectileLine Instanse;

    public Vector3 LastPoint { get { if (m_Points == null) return (Vector3.zero); return (m_Points[m_Points.Count - 1]); } }

    [SerializeField] private float m_MinDist = 0.1f;
    private LineRenderer m_LineRenderer;
    private GameObject m_Projectile;
    public GameObject Projectile { get 
        { 
            return m_Projectile; 
        } 
        set 
        { 
            m_Projectile = value;
        if(m_Projectile != null) 
            { 
                m_LineRenderer.enabled = false; 
                m_Points = new List<Vector3>(); 
                AddPoint(); 
            }
        } }
    private List<Vector3> m_Points;

    private void Awake()
    {
        Instanse = this;
        m_Projectile = GameObject.FindGameObjectWithTag("Projectile");
        m_LineRenderer = GetComponent<LineRenderer>();
        m_LineRenderer.enabled = false;
        m_Points = new List<Vector3>();
    }
    public void Clear()
    {
        m_Projectile = null;
        m_LineRenderer.enabled = false;
        m_Points = new List<Vector3>();
    }
    public void AddPoint()
    {
        Vector3 point = m_Projectile.transform.position;
        if (m_Points.Count > 0 && (point - LastPoint).magnitude < m_MinDist)
        {
            return;
        }
        if (m_Points.Count == 0)
        {
            Vector3 launchPos = point - Slingshot.LaunchPos;
            m_Points.Add(point + launchPos);
            m_Points.Add(point);
            m_LineRenderer.positionCount = 2;
            m_LineRenderer.SetPosition(0, m_Points[0]);
            m_LineRenderer.SetPosition(1, m_Points[1]);
            m_LineRenderer.enabled = true;
        }
        else
        {
            m_Points.Add(point);
            m_LineRenderer.positionCount = m_Points.Count;
            m_LineRenderer.SetPosition(m_Points.Count - 1, LastPoint);
            m_LineRenderer.enabled = true;
        }
    }
    private void FixedUpdate()
    {
        if (m_Projectile == null)
        {
            if (FollowCamera.FollowingObject != null)
            {
                if (FollowCamera.FollowingObject.tag == "Projectile")
                m_Projectile = FollowCamera.FollowingObject;
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
        AddPoint();
        if (FollowCamera.FollowingObject == null) m_Projectile = null;
    }
}
