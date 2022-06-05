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
    [SerializeField] private TimespanUI _timespanUI;

    [Space]
    [SerializeField] private int _publicOpinionInitialValue = 0;
    [SerializeField, Range(0f, 1f)] private float _frenesyPublicOpinionBonus = 0.25f;
    [SerializeField, Range(0f, 1f)] private float _frenesyTimeDowngradePercent = 0.25f;

    private int _publicOpinion;

    public ScrollingTextUI DebateTextUI => _debateTextUI;
    public ChoicesGeneratorUI ChoicesGeneratorUI => _choicesGeneratorUI;
    public PublicOpinionUI PublicOpinionUI => _publicOpinionUI;
    public TimespanUI TimespanUI => _timespanUI;

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
    public int Frenesy { get; set; }
    public float FrenesyPublicOpinionBonus => _frenesyPublicOpinionBonus;
    public float FrenesyTimeDowngradePercent => _frenesyTimeDowngradePercent;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _publicOpinion = _publicOpinionInitialValue;
        _publicOpinionUI.SetValueWithoutTransition(_publicOpinionInitialValue);

        Frenesy = 0;
    }

    public bool IsSequenceAlreadyMet(DebateSequence sequence)
    {
        return _sequencePlayer.IsSequenceAlreadyMet(sequence);
    }
}
