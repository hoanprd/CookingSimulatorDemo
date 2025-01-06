using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider fillBar;

    public GameObject gameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        fillBar.value = fillBar.maxValue = GameManager.Instance.GetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.updateUI)
        {
            GameManager.Instance.updateUI = false;
            fillBar.value = GameManager.Instance.GetHealth();

            if (GameManager.Instance.IsGameOver)
            {
                gameOverPanel.SetActive(true);
            }
        }
    }
}
