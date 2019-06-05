using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }

            return m_instance;
        }
    }

    private static UIManager m_instance;
    
    public Text scoreText;
    public GameObject gameoverUI;
    public GameObject stageclearUI;
    public Text stageClearLevelText;
    public Text stageClearScoreText;
    public Text stageClearCherryText;
    public Text stageClearGemText;

    public void UpdateScoreText(int newScore)
    {
        scoreText.text = "SCORE : " + newScore;
    }
    
    public void SetActiveGameOverUI(bool active)
    {
        gameoverUI.SetActive(active);
    }

    public void SetActiveStageClearUI(bool active, int level)
    {
        stageclearUI.SetActive(active);
        stageClearLevelText.text = "LEVEL " + level.ToString();
        stageClearScoreText.text = PlayerPrefs.GetInt("Score").ToString();
        stageClearCherryText.text = PlayerPrefs.GetInt("Cherry").ToString();
        stageClearGemText.text = PlayerPrefs.GetInt("Gem").ToString();
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
