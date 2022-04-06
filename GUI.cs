using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    [SerializeField] private GameObject m_PausePanel;
    private void Start()
    {
        m_PausePanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) PauseGame();
    }
    public void Restart()
    {
        MissionDemolition.Instance.StartLevel();
    }
    public void ContinueGame()
    {
        m_PausePanel.SetActive(false);
        var slignshot = FindObjectOfType<Slingshot>();
        slignshot.IsPaused = false;
    }
    public void Exit()
    {
        Application.Quit();
    }
    private void PauseGame()
    {
        m_PausePanel.SetActive(true);
        var slignshot = FindObjectOfType<Slingshot>();
        slignshot.IsPaused = true;

    }   
    

}
