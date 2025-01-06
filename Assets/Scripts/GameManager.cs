using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool updateUI;

    private int health = 3;
    public bool IsGameOver { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateHealth(int value)
    {
        updateUI = true;
        SetHealth(GetHealth() + value);
    }

    public void CheckLose()
    {
        if (GetHealth() <= 0)
        {
            IsGameOver = true;
        }
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public int GetHealth()
    {
        return health;
    }
}
