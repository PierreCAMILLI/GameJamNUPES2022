using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Random Next Sequence", menuName = AssetPath + "Random Next Sequence")]
public class RandomNextSequence : DebateSequence
{
    [SerializeField] private DebateSequence[] _nextSequences;

    [Header("Already met next sequence")]
    [SerializeField] private bool _activate = false;
    [SerializeField, TextArea(3, 6)] private string _text;
    [SerializeField] private float _waitingTimeBeforeNext;

    private float _textFullyAppearedTime;
    private DebateSequence _nextSequence;

    public override void OnStart()
    {
        _nextSequence = _nextSequences.Random();
        if (!_activate || !DebateManager.Instance.IsSequenceAlreadyMet(_nextSequence))
        {
            SwitchToNextSequence(_nextSequence);
            return;
        }

        _textFullyAppearedTime = float.MaxValue;
        ScrollingTextUI scrollingText = DebateManager.Instance.DebateTextUI;
        scrollingText.SetText(_text, () =>
        {
            _textFullyAppearedTime = Time.time;
        });
    }

    public override void OnUpdate()
    {
        if (Time.time - _textFullyAppearedTime >= _waitingTimeBeforeNext)
        {
            SwitchToNextSequence(_nextSequence);
        }
    }
}
