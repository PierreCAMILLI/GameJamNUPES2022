using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebateManager : Toolset.SingletonBehaviour<DebateManager>
{
    [SerializeField] private DebatePlayer _sequencePlayer;

    [Space]
    [SerializeField] private ScrollingTextUI _debateTextUI;
    [SerializeField] private ChoicesGeneratorUI _choicesGeneratorUI;
    [SerializeField] private PublicOpinionUI _publicOpinionUI;

    [Space]
    [SerializeField] private int _publicOpinionInitialValue = 0;

    [SerializeField, ReadOnly] private int _publicOpinion;

    public ScrollingTextUI DebateTextUI => _debateTextUI;
    public ChoicesGeneratorUI ChoicesGeneratorUI => _choicesGeneratorUI;

    public DebatePlayer SequencePlayer => _sequencePlayer;
    public int PublicOpinion
    {
        get => _publicOpinion;
        set
        {
            int newValue = Mathf.Clamp(value, 0, 100);
            if (_publicOpinion != newValue)
            {
                _publicOpinion = newValue;
                _publicOpinionUI.Value = newValue;
            }
        }
    }
    public DebatorState DebatorState { get; set; }

    private void Start()
    {
        _publicOpinion = _publicOpinionInitialValue;
        _publicOpinionUI.SetValueWithoutTransition(_publicOpinionInitialValue);
    }
}
