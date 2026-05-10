using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TMP_Text orderText;
    public TMP_Text timerText;
    public TMP_Text scoreText;
    public GameObject resultPanel;
    public TMP_Text resultText;

    [Header("Round")]
    public float roundTimeSeconds = 90f;
    public int targetScore = 100;

    private float _timeRemaining;
    private int _score;
    private bool _roundEnded;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        _timeRemaining = roundTimeSeconds;
        _score = 0;
        _roundEnded = false;

        if (resultPanel != null) resultPanel.SetActive(false);
        SetOrderText("Order: Boiled Water + Fried Egg");
        UpdateScoreUI();
    }

    private void Update()
    {
        if (_roundEnded) return;

        _timeRemaining -= Time.deltaTime;
        if (timerText != null) timerText.text = $"Time: {Mathf.CeilToInt(_timeRemaining)}";

        if (_timeRemaining <= 0f) EndRound();
    }

    public void AddScore(int amount)
    {
        if (_roundEnded) return;
        _score += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null) scoreText.text = $"Score: {_score}";
    }

    public void SetOrderText(string message)
    {
        if (orderText != null) orderText.text = message;
    }

    private void EndRound()
    {
        _roundEnded = true;
        if (resultPanel != null) resultPanel.SetActive(true);

        bool win = _score >= targetScore;
        if (resultText != null)
        {
            resultText.text = win
                ? $"⭐ You Win!\nScore {_score}/{targetScore}\nChef status: still employed."
                : $"💥 You Lose!\nScore {_score}/{targetScore}\nKitchen status: smoky.";
        }
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevelMock()
    {
        ReloadLevel();
    }

    public bool IsRoundEnded() => _roundEnded;
}
