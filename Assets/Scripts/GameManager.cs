using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }

    private static GameManager m_instance;

    public GameObject[] stars;

    private int score = 0;
    private int health = 3;
    private int level = 1;
    public bool isGameover { get; private set; }

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int newScore)
    {
        if (!isGameover)
        {
            score += newScore;
            UIManager.instance.UpdateScoreText(score);
        }
    }

    public void LoseHealth()
    {
        health--;
        Destroy(stars[health]);

        if (health == 0) EndGame();
    }

    public void LoadNextLevel()
    {
        level++;
        Debug.Log(level);
        //SceneManager.LoadScene(level);
    }

    public void EndGame()
    {
        isGameover = true;
        UIManager.instance.SetActiveGameoverUI(true);
    }
}
