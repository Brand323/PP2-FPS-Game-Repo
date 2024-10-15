using UnityEngine;

public class gameTimeManager : MonoBehaviour
{
    public static gameTimeManager Instance;

    public int currentDay = 1;
    public float dayLength = 30f; // One in game day lasts 30 seconds (test purposes)
    private float currentDayTimer = 0f;

    private int startHour = 0;
    private int totalHoursInDay = 24;

    public int hours;
    public int minutes;

    private bool isFirstDay = true;

    // Time control vars
    private float timeScale = 1f; // Normal timescalew
    private float[] timeScales = {0f, 1f, 2f, 3f}; // Pause, 1x, 2x, 3x speeds
    private int currentSpeedIndex = 1; // Start at normal timescale

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);


        if (isFirstDay)
        {
            hours += 6;
            minutes = 0;
        }
        else
        {
            hours = startHour;
            minutes = 0;
        }
    }

    private void Update()
    {
        Time.timeScale = timeScale;

        // Update the day timer
        currentDayTimer += Time.deltaTime * timeScale;
        // Calculate how much time has passed in the day
        float dayProgress = currentDayTimer / dayLength;
        // Total in game hours that passed since start
        hours = startHour + Mathf.FloorToInt(dayProgress * totalHoursInDay);
        if (isFirstDay)
        {
            hours += startHour;
        }
        // Calculate the minutes
        minutes = Mathf.FloorToInt((dayProgress * totalHoursInDay * 60f) % 60f);
        // Handle day and time rollover ( reset time after 24:59 and increment day)
        if (hours >= totalHoursInDay)
            IncrementDay();
    }

    private void IncrementDay()
    {
        currentDay++;
        currentDayTimer = 0f;
        hours = 0;
        minutes = 0;
        isFirstDay = false;
        // Call production cycle across all settlements
        KingdomManager.Instance.OnDayPassed();
    }

    // Convert the current day timer into in-game hours and minutes
    public string GetCurrentTime()
    {
        // return military format
        return $"{hours.ToString("D2")}:{minutes.ToString("D2")}";

    }

    // Method to switch between different time speeds
    public void ChangeTimeSpeed(int speedIndex)
    {
        if (speedIndex >= 0 && speedIndex < timeScales.Length)
        {
            currentSpeedIndex = speedIndex;
            timeScale = timeScales[speedIndex];
        }
    }

    // Method to pause the game
    public void PauseGame()
    {
        ChangeTimeSpeed(0); // 0f for pause
    }

    public void SetNormalSpeed()
    {
        ChangeTimeSpeed(1); // 1f for normal speed
    }

    public void SetFastForward()
    {
        ChangeTimeSpeed(2); // 2f for fast forward
    }

    public void SetFastForwardMAX()
    {
        ChangeTimeSpeed(3); // 3f for very fast
    }
}
