using UnityEngine;
using Common.Scripts;

public class TimeOnlyWhenCharacterMoves : MonoBehaviour
{
    [SerializeField] private BasicCharacter character;
    [SerializeField] private DayNightCycle dayNightCycle;

    private void Update()
    {
        if (character == null || dayNightCycle == null)
            return;

        Vector3 movement = GetPrivateMovement(character);

        // Zeit nur weiterlaufen lassen, wenn Bewegung vorhanden ist
        dayNightCycle.isPausedExternally = (movement.sqrMagnitude <= 0.01f);
    }

    private Vector3 GetPrivateMovement(BasicCharacter character)
    {
        var field = typeof(BasicCharacter).GetField("_movement",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return field != null ? (Vector3)field.GetValue(character) : Vector3.zero;
    }
}