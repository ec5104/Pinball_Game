using UnityEngine;
using UnityEngine.SceneManagement; // Required for reloading scenes
using System.Collections;
using TMPro;

public class GameRestart : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] public TMP_Text myTextComponent;
    
    // Call this method when the ball is lost (e.g., hits a bottom trigger)
    public void TriggerGameOver()
    {
        StartCoroutine(RestartDelayRoutine());
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        Time.timeScale = 0f; // Pauses the game physics/movement
    }

    // Hook this up to your UI Button OnClick event
    public void RestartGame()
    {
        Time.timeScale = 1f; // Resumes normal time speed
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        BallMove.life = 3;
        BallMove.score = 0;

    }
    
    private IEnumerator RestartDelayRoutine()
    {
        // 1. "Sleeps" this specific action for 1.0 seconds
        yield return new WaitForSeconds(5f);

        // 2. Gets the currently active scene name
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 3. Restarts the level
        SceneManager.LoadScene(currentSceneName);
    }

    public void ChangeScore(int score)
    {
        string printText = "Score: " + score;
        myTextComponent.text = printText.ToString();
    }
}