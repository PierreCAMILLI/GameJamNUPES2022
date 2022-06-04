using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Random Next Sequence", menuName = AssetPath + "Random Next Sequence")]
public class RandomNextSequence : DebateSequence
{
    [SerializeField] private DebateSequence[] _nextSequences;

    public override void OnStart()
    {
        SwitchToNextSequence(_nextSequences.Random());
    }
}
