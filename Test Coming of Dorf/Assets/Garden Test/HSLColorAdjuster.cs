using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HSLColorAdjuster : MonoBehaviour
{
    [Range(0f, 1f)] public float hue = 0f;           // Farbton
    [Range(0f, 1f)] public float saturation = 1f;    // Sättigung
    [Range(0f, 1f)] public float lightness = 0.5f;   // Helligkeit

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    void Update()
    {
        UpdateColor(); // Live-Update im Inspector
    }

    void UpdateColor()
    {
        sr.color = ColorFromHSL(hue, saturation, lightness);
    }

    // Wandelt HSL in RGB um
    public static Color ColorFromHSL(float h, float s, float l)
    {
        float r = l, g = l, b = l;
        if (s != 0)
        {
            float q = l < 0.5f ? l * (1f + s) : l + s - l * s;
            float p = 2f * l - q;
            r = HueToRGB(p, q, h + 1f / 3f);
            g = HueToRGB(p, q, h);
            b = HueToRGB(p, q, h - 1f / 3f);
        }
        return new Color(r, g, b, 1f);
    }

    private static float HueToRGB(float p, float q, float t)
    {
        if (t < 0f) t += 1f;
        if (t > 1f) t -= 1f;
        if (t < 1f / 6f) return p + (q - p) * 6f * t;
        if (t < 1f / 2f) return q;
        if (t < 2f / 3f) return p + (q - p) * (2f / 3f - t) * 6f;
        return p;
    }
}