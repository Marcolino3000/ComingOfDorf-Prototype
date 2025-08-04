using UnityEngine;
using TMPro;

public class TimeDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    private void OnEnable()
    {
        DayNightCycle.OnTimeChanged += UpdateTimeDisplay;
    }

    private void OnDisable()
    {
        DayNightCycle.OnTimeChanged -= UpdateTimeDisplay;
    }

    void UpdateTimeDisplay(float time)
    {
        int hours = Mathf.FloorToInt(time);
        int minutes = Mathf.FloorToInt((time - hours) * 60f);

        string suffix = time switch
        {
            >= 5f and < 11f => "Morgen",
            >= 11f and < 17f => "Tag",
            >= 17f and < 20f => "Abend",
            _ => "Nacht"
        };

        timeText.text = $"{hours:00}:{minutes:00} ({suffix})";
    }
}