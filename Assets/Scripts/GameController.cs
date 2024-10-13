using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject timerObject;        // Timer GameObject to be enabled
    public GameObject ringCounterObject;  // RingCounter GameObject to be enabled
    public TMP_Text resultText;           // The Text to display win/lose
    public GameObject resultPanel;        // Panel to show results
    public Image resultImage;             // Image to be displayed in result
    public Sprite winImage;               // Win Image
    public Sprite loseImage;              // Lose Image
    public Button retryButton;            // Retry button to reload the game on lose

    [SerializeField] private Timer timer;
    [SerializeField] private RingCounter ringCounter;

    private bool gameStarted = false;
    private bool gameFinished = false;

    // Objective completion event
    public delegate void ObjectiveCompleted();
    public static event ObjectiveCompleted OnObjectiveComplete;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timerObject.SetActive(false);
        ringCounterObject.SetActive(false);
        resultPanel.SetActive(false);

        // Ensure retry button is initially hidden and not interactive
        retryButton.gameObject.SetActive(false);
        retryButton.onClick.AddListener(ReloadGame); // Add listener for retry button
    }

    public void StartGame()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            StartCoroutine(StartCountdownWithDelay(1.5f));
        }
    }

    private IEnumerator StartCountdownWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        timerObject.SetActive(true);
        ringCounterObject.SetActive(true);
        timer.StartTimer();
    }

    public void TimeUp()
    {
        if (gameFinished) return;

        gameFinished = true;
        ShowLoseResult();
    }

    private void ShowLoseResult()
    {
        resultPanel.SetActive(true);
        resultText.text = "You Lose";
        resultImage.sprite = loseImage;

        // Show retry button only on losing
        retryButton.gameObject.SetActive(true);
    }

    private void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }

    public void ObjectiveAchieved()
    {
        if (!gameFinished)  // Make sure this runs only once
        {
            gameFinished = true;

            // Trigger the objective completion event
            if (OnObjectiveComplete != null)
            {
                OnObjectiveComplete.Invoke();
            }

            Debug.Log("Objective Achieved - Event Triggered");
        }
    }


    // Methods for handling the win screen and image
    public void ShowWinText()
    {
        resultText.text = "You Win";
        resultPanel.SetActive(true); // Ensure the panel shows up when the player wins
    }

    public void ShowWinImage()
    {
        resultImage.sprite = winImage;
        resultPanel.SetActive(true); // Ensure the panel shows up when the player wins
    }
}
