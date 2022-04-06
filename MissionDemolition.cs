using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public enum GameMode
{
    Idle, Playing, LevelEnd
}
public class MissionDemolition : MonoBehaviour
{
    public static MissionDemolition Instance;
    public static void ShotFired() { Instance.m_Shots++; }
    [SerializeField] private Text m_GUILevel;
    [SerializeField] private Text m_GUIShots;
    [SerializeField] private Text m_GUIButtonText;
    [SerializeField] private Transform m_CastlePos;
    [SerializeField] private GameObject[] m_Castles;
    [SerializeField] private Transform m_ViewBoth;

    private int m_Level;
    private int m_LevelMax;
    private int m_Shots;
    private GameObject m_CurrentCastle;
    private GameMode m_Mode = GameMode.Idle;
    private string m_ShowingText = "Показать рогатку";
    private void Start()
    {
        Instance = this;
        m_Level = 0;
        m_LevelMax = m_Castles.Length;
        StartLevel();
    }
    private void Update()
    {
        UpdateGUI();
        if ((m_Mode == GameMode.Playing) && Goal.IsOver)
        {
            m_Mode = GameMode.LevelEnd;
            SwitchView("Показать всё");
            Invoke("NextLevel", 2f);
        }
    }
    public void SwitchView(string view = "")
    {
        if (view == "") view = m_GUIButtonText.text;
        m_ShowingText = view;
        switch (m_ShowingText)
        {
            case "Показать рогатку":
                FollowCamera.FollowingObject = null;
                m_GUIButtonText.text = "Показать замок";
                break;
            case "Показать замок":
                FollowCamera.FollowingObject = m_CurrentCastle;
                m_GUIButtonText.text = "Показать всё";
                break;
            case "Показать всё":
                FollowCamera.FollowingObject = m_ViewBoth.gameObject;
                m_GUIButtonText.text = "Показать рогатку";
                break;
        }
    }
    public void StartLevel()
    {
        if (m_CurrentCastle != null) Destroy(m_CurrentCastle);
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (var projectile in projectiles)
        {
            Destroy(projectile);
        }
        m_CurrentCastle = Instantiate(m_Castles[m_Level]);
        m_CurrentCastle.transform.position = m_CastlePos.position;
        SwitchView("Показать всё");      
        Goal.IsOver = false;
        m_Shots = 0;
        UpdateGUI();
        m_Mode = GameMode.Playing;
    }
    private void UpdateGUI()
    {
        m_GUILevel.text = "Уровень: " + " " + (m_Level + 1);
        m_GUIShots.text = "Выстрелов сделано: " + " " + m_Shots;
    }
    private void NextLevel()
    {
        m_Level++;
        if (m_Level == m_LevelMax) m_Level = 0;
        StartLevel();
    }


}
