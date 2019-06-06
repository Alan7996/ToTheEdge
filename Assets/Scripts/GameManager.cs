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
    public GameObject doorOpenPrefab;

    private int score = 0;
    private int health = 3;
    private int level = 1;
    public bool isStageclear { get; private set; }
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
        stars[health].SetActive(false);

        if (health == 0) EndGame();
    }

    public void ResetHealth()
    {
        isGameover = false;
        health = 3;
        foreach (GameObject star in stars) {
            star.SetActive(true);
        }
    }

    public void Die()
    {
        for (int i = --health; i >= 0; i--)
        {
            stars[health].SetActive(false);
        }
        EndGame();
    }

    public void LoadNextLevel()
    {
        doorOpenPrefab.SetActive(true);
        isStageclear = true;

        PlayerPrefs.SetInt("Health", health);
        PlayerPrefs.SetInt("Score", score);
        UIManager.instance.Invoke("SetActiveStageClearUI", 1);

        level++;
        Debug.Log(level);
        //SceneManager.LoadScene(level);
    }

    public void EndGame()
    {
        isGameover = true;
        UIManager.instance.SetActiveGameOverUI();
    }
}
