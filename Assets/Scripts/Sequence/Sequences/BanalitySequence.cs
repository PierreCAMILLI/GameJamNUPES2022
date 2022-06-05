using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Banality", menuName = AssetPath + "Banality")]
public class BanalitySequence : DebateSequence
{
    [SerializeField, TextArea(3, 6)] private string _text;
    [SerializeField] private float _waitingTimeBeforeNext;

    [Space]
    [SerializeField] private bool _changeDebatorState = false;
    [SerializeField] private DebatorState _nextState;

    [Space]
    [SerializeField] private DebateSequence _nextSequence;

    private float _textFullyAppearedTime;

    public string Text => _text;

    public override void OnStart()
    {
        _textFullyAppearedTime = float.MaxValue;
        ScrollingTextUI scrollingText = DebateManager.Instance.DebateTextUI;
        if (_changeDebatorState)
        {
            DebateManager.Instance.DebatorUI.State = _nextState;
        }
        DebateManager.Instance.DebatorUI.IsTalking = true;
        scrollingText.SetText(_text, () =>
        {
            DebateManager.Instance.DebatorUI.IsTalking = false;
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
