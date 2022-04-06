using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private GameObject m_CloudSphere;
    [SerializeField] private int m_NumSpheresMin = 6;
    [SerializeField] private int m_NumSpheresMax = 10;
    private Vector3 m_SphereOffsetScale = new Vector3(5, 2, 1);
    private Vector2 m_SphereScaleRangeX = new Vector2(4, 8);
    private Vector2 m_SphereScaleRangeY = new Vector2(3, 4);
    private Vector2 m_SphereScaleRangeZ = new Vector2(2, 4);
    private float m_ScaleMinY = 2f;
    private List<GameObject> m_Spheres;
    private void Start()
    {
        m_Spheres = new List<GameObject>();
        int num = Random.Range(m_NumSpheresMin, m_NumSpheresMax);
        for (int i = 0; i < num; i++)
        {
            GameObject sphere = Instantiate<GameObject>(m_CloudSphere);
            m_Spheres.Add(sphere);
            sphere.transform.SetParent(this.transform);
            sphere.transform.localPosition = GetSphereOffset();
            sphere.transform.localScale = GetSphereScale();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Restart();
    }
    private void Restart()
    {
        foreach (var sphere in m_Spheres)
        {
            Destroy(sphere);
        }
        Start();
    }
    private Vector3 GetSphereOffset()
    {
        Vector3 offset = Random.insideUnitSphere;
        offset.x *= m_SphereOffsetScale.x;
        offset.y *= m_SphereOffsetScale.y;
        offset.z *= m_SphereOffsetScale.z;
        return offset;
    }
    private Vector3 GetSphereScale()
    {
        Vector3 scale = Vector3.one;
        scale.x = Random.Range(m_SphereScaleRangeX.x, m_SphereScaleRangeX.y);
        scale.y = Random.Range(m_SphereScaleRangeY.x, m_SphereScaleRangeY.y);
        scale.z = Random.Range(m_SphereScaleRangeZ.x, m_SphereScaleRangeZ.y);
        scale.y *= 1 - (Mathf.Abs(GetSphereOffset().x / m_SphereOffsetScale.x));
        scale.y = Mathf.Max(scale.y, m_ScaleMinY);
        return scale;
    }

}
