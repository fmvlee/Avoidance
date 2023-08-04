using System.Collections.Generic;
using UnityEngine;

// Create menu option for scriptable object
[CreateAssetMenu(fileName = "Event", menuName = "Game Event")]
// This creates a scriptable game event to trigger all the events listening
public class GameEvent : ScriptableObject
{
    // List of GameEventListeners listening for the game event
    private List<GameEventListener> listeners = new List<GameEventListener>();
    public void TriggerEvent()
    {
        // Trigger an event for each listener
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventTriggered();
        }
    }
    // Adds a game event listener to the event
    public void AddListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }
    // Removes a game event listener to the event
    public void RemoveListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}