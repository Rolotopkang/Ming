using System;
using System.Collections.Generic;
using Tools;

public static class EventCenter
{
    private static Dictionary<EnumTools.GameEvent, Action<Dictionary<string, object>>> eventTable = new Dictionary<EnumTools.GameEvent, Action<Dictionary<string, object>>>();

    public static void Subscribe(EnumTools.GameEvent gameEvent, Action<Dictionary<string, object>> callback)
    {
        if (!eventTable.ContainsKey(gameEvent))
            eventTable[gameEvent] = delegate { };

        eventTable[gameEvent] += callback;
    }

    public static void Unsubscribe(EnumTools.GameEvent gameEvent, Action<Dictionary<string, object>> callback)
    {
        if (eventTable.ContainsKey(gameEvent))
            eventTable[gameEvent] -= callback;
    }

    public static void Publish(EnumTools.GameEvent gameEvent, Dictionary<string, object> parameters)
    {
        if (eventTable.ContainsKey(gameEvent))
        {
            eventTable[gameEvent]?.Invoke(parameters);
        }
    }
}