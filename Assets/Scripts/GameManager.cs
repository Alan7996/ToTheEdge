﻿using System.Collections;
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

    private int score = 0;
    public bool isGameover { get; private set; }

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //FindObjectOfType<PlayerHealth>().onDeath += EndGame;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) SceneManager.LoadScene(0);
    }

    public void AddScore(int newScore)
    {
        if (!isGameover)
        {
            score += newScore;
            //UIManager.instance.UpdateScoreText(score);
        }
    }

    public void EndGame()
    {
        isGameover = true;
        //UIManager.instance.SetActiveGameoverUI(true);
    }
}
