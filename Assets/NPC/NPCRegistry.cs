using System.Collections.Generic;
using UnityEngine;

public static class NPCRegistry
{
    private static readonly HashSet<NPCIdentity> _alive = new();

    public static void Register(NPCIdentity npc)
    {
        if (npc != null)
            _alive.Add(npc);
    }

    public static void Unregister(NPCIdentity npc)
    {

        if (npc != null)
            _alive.Remove(npc);
    }

    public static bool TryGetRandomNPC(NPCIdentity exclude, out NPCIdentity result)
    {
        result = null;
        if (_alive.Count == 0)
            return false;

        int pick = Random.Range(0, _alive.Count);
        int i = 0;

        foreach (var npc in _alive)
        {
            if (npc == null)
                continue;
            if (exclude != null && npc == exclude)
                continue;
            if (i == pick)
            {
                result = npc;
                return true;
            }
        }
        return false;
    }
}
