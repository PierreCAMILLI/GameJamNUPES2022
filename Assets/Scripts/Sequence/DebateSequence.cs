using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DebateSequence : ScriptableObject
{
    public const string AssetPath = "Sequences/";

    protected void SwitchToNextSequence(DebateSequence nextSequence)
    {
        if (nextSequence == null)
        {
            return;
        }
        DebateManager.Instance.SequencePlayer.SetNextSequence(nextSequence);
    }

    public virtual void OnStart() { }
    public virtual void OnUpdate() { }
    public virtual void OnEnd() { }
}
