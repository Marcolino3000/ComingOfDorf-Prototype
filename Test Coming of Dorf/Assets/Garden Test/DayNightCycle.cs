using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0f, 24f)]
    public float timeOfDay = 6f;
    public float dayLengthInMinutes = 1f;

    public bool isPausedExternally = false;

    public delegate void TimeChanged(float time);
    public static event TimeChanged OnTimeChanged;

    private float lastReportedTime = -1f;

    void Update()
    {
        if (!isPausedExternally)
        {
            timeOfDay += (24f / (dayLengthInMinutes * 60f)) * Time.deltaTime;
            if (timeOfDay >= 24f) timeOfDay -= 24f;
        }

        // Event trotzdem auslösen, auch wenn Zeit steht
        if (OnTimeChanged != null)
        {
            OnTimeChanged.Invoke(timeOfDay);
        }
    }
}