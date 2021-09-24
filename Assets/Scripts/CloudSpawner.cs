using System.Collections;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_Anchor;
    [SerializeField] private int m_NumClouds = 40;
    [SerializeField] private GameObject m_CloudPrefab;
    [SerializeField] private Vector3 m_CloudPosMin;
    [SerializeField] private Vector3 m_CloudPosMax;
    [SerializeField] private float m_CloudScaleMin;
    [SerializeField] private float m_CloudScaleMax;
    [SerializeField] private float m_CloudSpeed;
    private GameObject[] m_Clouds;
    private void Awake()
    {
        m_Clouds = new GameObject[m_NumClouds];
        GameObject cloud;
        for (int i = 0; i < m_NumClouds; i++)
        {
            cloud = Instantiate(m_CloudPrefab);

            Vector3 pos = Vector3.zero;
            pos.x = Random.Range(m_CloudPosMin.x, m_CloudPosMax.x);
            pos.y = Random.Range(m_CloudPosMin.y, m_CloudPosMax.y);

            float scaleU = Random.value;
            float scale = Mathf.Lerp(m_CloudScaleMin, m_CloudScaleMax, scaleU);
            pos.y = Mathf.Lerp(m_CloudPosMin.y, pos.y, scaleU);
            pos.z = 100 - 90 * scaleU;

            cloud.transform.position = pos;
            cloud.transform.localScale = Vector3.one * scale;
            cloud.transform.SetParent(m_Anchor.transform);
            m_Clouds[i] = cloud;
        }
    }
    private void Update()
    {
        foreach (var cloud in m_Clouds)
        {
            float scale = cloud.transform.localScale.x;
            Vector3 pos = cloud.transform.position;
            pos.x -= scale * Time.deltaTime * m_CloudSpeed;
            if (pos.x <= m_CloudPosMin.x) pos.x = m_CloudPosMax.x;
            cloud.transform.position = pos;
        }
    }
}
