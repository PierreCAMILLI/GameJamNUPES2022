using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebatePlayer : MonoBehaviour
{
    [SerializeField] private DebateSequence _startSequence;

    private ISet<DebateSequence> _sequencesDone;

    public DebateSequence CurrentSequence { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _sequencesDone = new HashSet<DebateSequence>();
        _sequencesDone.Add(_startSequence);
        _startSequence?.OnStart();
        CurrentSequence = _startSequence;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentSequence?.OnUpdate();
    }

    public void SetNextSequence(DebateSequence nextSequence)
    {
        CurrentSequence.OnEnd();
        CurrentSequence = nextSequence;
        _sequencesDone.Add(nextSequence);
        nextSequence.OnStart();
    }

    public bool IsSequenceAlreadyMet(DebateSequence sequence)
    {
        return _sequencesDone.Contains(sequence);
    }
}
