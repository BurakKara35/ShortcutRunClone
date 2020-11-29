using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool isOn;
    public GameObject finalIslandGenerator;

    [HideInInspector] public int point = 0;

    private UIController uIController;

    private void Awake()
    {
        isOn = false;

        uIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
    }

    public void StartGame()
    {
        isOn = true;
        uIController.playButton.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        isOn = false;
        uIController.DisplayGameOverUI();
    }

    public void LevelCompleted()
    {
        uIController.DisplayLevelCompletedUI(point);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GenerateFinalIslands()
    {
        finalIslandGenerator.SetActive(true);
    }

    public void IncreasePoint()
    {
        point++;
        uIController.DisplayPoint(point);
    }

    public void MultiplyPoint(int multiplier)
    {
        point *= multiplier;
    }
}
