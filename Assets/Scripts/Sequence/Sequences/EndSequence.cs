using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "End", menuName = AssetPath + "End")]
public class EndSequence : DebateSequence
{
    [SerializeField, TextArea(3, 6)] private string _text;
    [SerializeField] private DebatorState _debatorState;
    [SerializeField] private float _waitingTimeBeforeNext;
    [SerializeField] private UnityEngine.Object _sceneToLoad;

    private float _textFullyAppearedTime;

    public string Text => _text;

    public override void OnStart()
    {
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
            UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneToLoad.name);
        }
    }
}
