
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteColorByTime : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Farben für Tageszeiten")]
    public Color nightColor = new Color(0.05f, 0.1f, 0.3f);
    public Color blueHourMorning = new Color(0.1f, 0.2f, 0.4f);
    public Color sunriseColor = new Color(0.9f, 0.5f, 0.3f);
    public Color dayColor = Color.white;
    public Color sunsetColor = new Color(1f, 0.5f, 0.5f);
    public Color blueHourEvening = new Color(0.1f, 0.2f, 0.4f);

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        DayNightCycle.OnTimeChanged += UpdateColor;
    }

    void OnDestroy()
    {
        DayNightCycle.OnTimeChanged -= UpdateColor;
    }

    void UpdateColor(float time)
    {
        Color color;

        if (time >= 4f && time < 6f) // Blaue Stunde (Morgen)
        {
            color = Color.Lerp(nightColor, sunriseColor, (time - 4f) / 2f);
        }
        else if (time >= 6f && time < 8f) // Sonnenaufgang
        {
            color = Color.Lerp(sunriseColor, dayColor, (time - 6f) / 2f);
        }
        else if (time >= 8f && time < 17f) // Tag
        {
            color = dayColor;
        }
        else if (time >= 17f && time < 19f) // Sonnenuntergang
        {
            color = Color.Lerp(dayColor, sunsetColor, (time - 17f) / 2f);
        }
        else if (time >= 19f && time < 21f) // Blaue Stunde (Abend)
        {
            color = Color.Lerp(sunsetColor, blueHourEvening, (time - 19f) / 2f);
        }
        else // Nacht
        {
            if (time >= 21f)
                color = Color.Lerp(blueHourEvening, nightColor, (time - 21f) / 3f);
            else
                color = nightColor;
        }

        sr.color = color;
    }
}
