using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject timerObject;        
    public GameObject ringCounterObject; 
    public TMP_Text resultText;           
    public GameObject resultPanel;        
    public Image resultImage;             
    public Sprite winImage;               
    public Sprite loseImage;             
    public Button retryButton;            

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

       
        retryButton.gameObject.SetActive(false);
        retryButton.onClick.AddListener(ReloadGame); 
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
        retryButton.gameObject.SetActive(true);
    }

    private void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void ObjectiveAchieved()
    {
        if (!gameFinished) 
        {
            gameFinished = true;

            if (OnObjectiveComplete != null)
            {
                OnObjectiveComplete.Invoke();
            }

            Debug.Log("Objective Achieved - Event Triggered");
        }
    }


   
    public void ShowWinText()
    {
        resultText.text = "You Win";
        resultPanel.SetActive(true);
    }

    public void ShowWinImage()
    {
        resultImage.sprite = winImage;
        resultPanel.SetActive(true); 
    }
}
