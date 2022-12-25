using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button nextLevelButton;

    private void Start()
    {
        retryButton.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        EventSystem.OnGameOver += OnGameEnd;
    }

    private void OnDisable()
    {
        EventSystem.OnGameOver -= OnGameEnd;
    }

    private void OnGameEnd(GameResult gameResult)
    {
        retryButton.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);

        if (gameResult == GameResult.Win)
        {
            nextLevelButton.gameObject.SetActive(true);
        }

        else
        {
            retryButton.gameObject.SetActive(true);
        }
        
    }
}
