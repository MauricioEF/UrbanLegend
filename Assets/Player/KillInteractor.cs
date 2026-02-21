using UnityEngine;

public class KillInteractor : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private PlayerNPCTargeting targeting;

    private void Update()
    {
        if (inputReader == null || targeting == null)
            return;
        //Consume Kill action ONCE per press 
        if (!inputReader.ConsumeKillRequest())
            return;
        var target = targeting.FocusedKillable;
        if (target == null)
            return;
        if (!target.IsAlive)
            return;
        target.Kill();
    }
}
