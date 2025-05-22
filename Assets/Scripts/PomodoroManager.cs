using UnityEngine;
using TMPro;
using System.Collections;

public class PomodoroManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    public float workDuration = 25f * 60f;
    public float shortBreakDuration = 5f * 60f;
    public float longBreakDuration = 15f * 60f;

    private int pomodoroCount = 0;
    private Coroutine timerCoroutine;
    private float currentTime;
    private string currentPhase;

    private bool isCycleRunning = false; //prevents re-triggering

    public void StartCycle()
    {
        if (isCycleRunning)
        {
            Debug.Log("Pomodoro cycle is already running.");
            return;
        }

        isCycleRunning = true;
        pomodoroCount = 0;
        StartPomodoroTimer(workDuration, "Work");
    }

    private void StartPomodoroTimer(float duration, string phase)
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }

        currentTime = duration;
        currentPhase = phase;
        timerCoroutine = StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (currentTime > 0)
        {
            UpdateTextDisplay();
            yield return new WaitForSeconds(1f);
            currentTime -= 1f;
        }

        currentTime = 0;
        UpdateTextDisplay();
        yield return new WaitForSeconds(1f);

        // Advance to next phase
        if (currentPhase == "Work")
        {
            pomodoroCount++;
            if (pomodoroCount < 4)
            {
                StartPomodoroTimer(shortBreakDuration, "Short Break");
            }
            else
            {
                StartPomodoroTimer(longBreakDuration, "Long Break");
            }
        }
        else if (currentPhase == "Short Break")
        {
            StartPomodoroTimer(workDuration, "Work");
        }
        else if (currentPhase == "Long Break")
        {
            timerText.text = "Text: 00:00 (Cycle Complete)";
            isCycleRunning = false; // ✅ Now it can be restarted
        }
    }

    private void UpdateTextDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = $"Text: {minutes:D2}:{seconds:D2} ({currentPhase})";
    }
}
