using UnityEngine;
using UnityEngine.Events;

// This class allows objects to listen for specified events and then trigger an event
public class GameEventListener : MonoBehaviour
{
    // Game Event e.g. Game Over, Life Lost
    public GameEvent gameEvent;
    // Action to trigger e.g. show Game Over Screen
    public UnityEvent onEventTriggered;

    // When enabled add the listener to the game event
    void OnEnable()
    {
        gameEvent.AddListener(this);
    }
    // When disabled remove the listener
    void OnDisable()
    {
        gameEvent.RemoveListener(this);
    }
    // When the event is trigger start the event
    public void OnEventTriggered()
    {
        onEventTriggered.Invoke();
    }
}