using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebatePlayer : MonoBehaviour
{
    [SerializeField] private DebateSequence _startSequence;

    public DebateSequence CurrentSequence { get; set; }

    // Start is called before the first frame update
    void Start()
    {
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
        nextSequence.OnStart();
    }
}
