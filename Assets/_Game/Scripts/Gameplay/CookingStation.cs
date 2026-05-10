using System.Collections;
using TMPro;
using UnityEngine;

public class CookingStation : MonoBehaviour
{
    public enum StationType { Boil, Fry }

    [Header("Station")]
    public StationType stationType;
    public float perfectMin = 2.5f;
    public float perfectMax = 3.5f;

    [Header("Feedback")]
    public TMP_Text feedbackText;

    private bool _playerInRange;
    private bool _isCooking;
    private float _holdDuration;

    private void Update()
    {
        if (GameManager.Instance == null || GameManager.Instance.IsRoundEnded()) return;

        if (_playerInRange && Input.GetKey(KeyCode.E))
        {
            _holdDuration += Time.deltaTime;
            _isCooking = true;
        }

        if (_isCooking && Input.GetKeyUp(KeyCode.E))
        {
            ResolveCooking();
            _isCooking = false;
            _holdDuration = 0f;
        }
    }

    private void ResolveCooking()
    {
        if (_holdDuration >= perfectMin && _holdDuration <= perfectMax)
        {
            int points = stationType == StationType.Boil ? 45 : 55;
            GameManager.Instance.AddScore(points);
            ShowFeedback($"{stationType} perfect! +{points}");
            return;
        }

        if (_holdDuration < perfectMin)
        {
            ShowFeedback("Undercooked! Release later.");
        }
        else
        {
            ShowFeedback("Burnt! Release sooner.");
        }
    }

    private void ShowFeedback(string message)
    {
        if (feedbackText == null)
        {
            Debug.Log(message);
            return;
        }

        StopAllCoroutines();
        StartCoroutine(FeedbackRoutine(message));
    }

    private IEnumerator FeedbackRoutine(string message)
    {
        feedbackText.text = message;
        feedbackText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        feedbackText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) _playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) _playerInRange = false;
    }
}
