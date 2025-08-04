
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSwitcher : MonoBehaviour
{
    public Sprite morningSprite;
    public Sprite daySprite;
    public Sprite eveningSprite;
    public Sprite nightSprite;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        DayNightCycle.OnTimeChanged += UpdateSprite;
    }

    void OnDestroy()
    {
        DayNightCycle.OnTimeChanged -= UpdateSprite;
    }

    void UpdateSprite(float time)
    {
        if (time >= 6 && time < 12)
            sr.sprite = morningSprite;
        else if (time >= 12 && time < 18)
            sr.sprite = daySprite;
        else if (time >= 18 && time < 21)
            sr.sprite = eveningSprite;
        else
            sr.sprite = nightSprite;
    }
}
