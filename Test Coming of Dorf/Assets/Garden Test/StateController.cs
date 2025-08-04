using UnityEngine;
using UnityEngine.UI;

public class StateController : MonoBehaviour
{
    public enum GameState
    {
        State1,
        State2,
        State3,
        State4
    }

    [Header("Zustand & UI")]
    public GameState currentState;
    public Image stateImage;
    public Sprite spriteState1;
    public Sprite spriteState2;
    public Sprite spriteState3;
    public Sprite spriteState4;

    [Header("Hint Objekte")]
    public GameObject hint1;
    public GameObject hint2;
    public GameObject hint3;

    [Header("Zeitsteuerung")]
    public float totalTransitionDuration = 20f; // in Game-Stunden
    private float startTimeOfDay;
    private bool isTransitioning = false;

    void OnEnable()
    {
        DayNightCycle.OnTimeChanged += OnTimeChanged;
    }

    void OnDisable()
    {
        DayNightCycle.OnTimeChanged -= OnTimeChanged;
    }

    void Start()
    {
        // Spielstart: Status auf 1 setzen, keine automatische Steuerung
        SetStateDirectly(GameState.State1);
    }

   void OnTimeChanged(float currentTimeOfDay)
{
    float elapsed = GetElapsedHours(startTimeOfDay, currentTimeOfDay);
    float phaseLength = totalTransitionDuration / 3f;

    GameState newState = currentState;

    if (isTransitioning)
    {
        if (elapsed >= totalTransitionDuration)
        {
            newState = GameState.State1;
            isTransitioning = false;
        }
        else if (elapsed >= phaseLength * 2)
        {
            newState = GameState.State2;
        }
        else if (elapsed >= phaseLength * 1)
        {
            newState = GameState.State3;
        }
    }
    else if (currentState != GameState.State1)
    {
        // Nach der Transition, wenn wir nicht manuell auf State1 zurück sind
        newState = GameState.State1;
    }

    if (newState != currentState)
    {
        SetStateDirectly(newState);
    }
}

    float GetElapsedHours(float from, float to)
    {
        if (to >= from)
            return to - from;
        else
            return (24f - from) + to; // Zeit über Mitternacht
    }

    public void SetStateToFour()
    {
        // Button gedrückt: Zustand auf 4 setzen und Übergang starten
        startTimeOfDay = FindObjectOfType<DayNightCycle>().timeOfDay;
        isTransitioning = true;
        SetStateDirectly(GameState.State4);
    }

  private void SetStateDirectly(GameState newState)
{
    // Wenn der Zustand sich nicht ändert → nichts tun
    if (currentState == newState)
        return;

    currentState = newState;

    // Sichtbarkeit Hints
bool hintsVisible = (newState != GameState.State1);
hint1.SetActive(hintsVisible);
hint2.SetActive(hintsVisible);
hint3.SetActive(hintsVisible);

    // UI Sprite wechseln
    switch (newState)
    {
        case GameState.State1: stateImage.sprite = spriteState1; break;
        case GameState.State2: stateImage.sprite = spriteState2; break;
        case GameState.State3: stateImage.sprite = spriteState3; break;
        case GameState.State4: stateImage.sprite = spriteState4; break;
    }
}
}