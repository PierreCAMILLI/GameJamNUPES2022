using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = AssetPath + "Question")]
public class QuestionSequence : DebateSequence
{
    [System.Serializable]
    public class Answer
    {
        [field: SerializeField, HideInInspector] public QuestionSequence Question { get; set; }
        [field: SerializeField, TextArea(1, 2)] public string Text { get; private set; }
        [field: SerializeField, Range(-100, 100)] public int WinRate { get; private set; }
        [field: SerializeField] public DebatorState DebatorState { get; private set; }
        [field: SerializeField] public DebateSequence NextSequence { get; private set; }
    }

    public const int AnswersMinSize = 3;

    [SerializeField] private string _title;
    [SerializeField, TextArea(3, 6)] private string _text;
    [SerializeField] private int _multiplier = 1;

    [Space]
    [SerializeField] private float _timeToAnswer = 10f;
    [SerializeField, Range(0, 100)] private int _noAnswerPenalty = 10;
    [SerializeField] private bool _stopFrenesy = false;

    [Space]
    [SerializeField] private DebateSequence _victorySequence;
    [SerializeField] private DebateSequence _defeatSequence;
    [SerializeField] private DebateSequence _timeElapsedSequence;
    [SerializeField] private DebateSequence _defaultNextSequence;
    
    [Space]
    [SerializeField] private Answer[] _answers;

    private float _startTime;

    public string Text => _text;
    public int Multiplier => _multiplier;
    public float TimeToAnswer
    {
        get
        {
            return _timeToAnswer * Mathf.Pow(1f - DebateManager.Instance.FrenesyTimeDowngradePercent, DebateManager.Instance.Frenesy);
        }
    }
    public Answer[] Answers => _answers;

    private void OnValidate()
    {
        if (_answers.Length < AnswersMinSize)
        {
            System.Array.Resize(ref _answers, AnswersMinSize);
        }
        foreach (Answer answer in _answers)
        {
            if (answer != null)
            {
                answer.Question = this;
            }
        }
    }

    public override void OnStart()
    {
        ScrollingTextUI scrollingText = DebateManager.Instance.DebateTextUI;
        DebateManager.Instance.DebatorUI.IsTalking = true;
        scrollingText.SetText(_text, ShowAnswers);
        _startTime = Mathf.Infinity;
    }

    public override void OnUpdate()
    {
        UpdateTime();
    }

    public override void OnEnd()
    {
        DebateManager.Instance.ChoicesGeneratorUI.RemoveAllButtons();
        DebateManager.Instance.TimespanUI.Value = 1f;
    }

    private void UpdateTime()
    {
        float elapsedTime = Time.time - _startTime;
        float timeToAnswer = TimeToAnswer;
        DebateManager.Instance.TimespanUI.Value = Mathf.Max(1f - (elapsedTime / timeToAnswer), 0f);
        if (elapsedTime >= timeToAnswer)
        {
            OnTimeElapsed();
        }
    }

    private void ShowAnswers()
    {
        DebateManager.Instance.DebatorUI.IsTalking = false;
        foreach (Answer answer in _answers)
        {
            DebateManager.Instance.ChoicesGeneratorUI.AddButton(answer);
        }

        _startTime = Time.time;
    }

    private void OnTimeElapsed()
    {
        if (!SetPublicOpinionValue(-_noAnswerPenalty))
        {
            DebateManager.Instance.Frenesy = 0;
            SwitchToNextSequence(_timeElapsedSequence ?? _defaultNextSequence);
        }
    }

    private bool SetPublicOpinionValue(int value)
    {
        int newOpinion = DebateManager.Instance.PublicOpinion += value;
        if (newOpinion >= 100)
        {
            SwitchToNextSequence(_victorySequence);
            return true;
        }
        if (newOpinion <= 0)
        {
            SwitchToNextSequence(_defeatSequence);
            return true;
        }

        return false;
    }

    public void HandleButtonPressed(Answer answer)
    {
        int earnOpinion = Mathf.RoundToInt(answer.WinRate * answer.Question.Multiplier * (1f + DebateManager.Instance.FrenesyPublicOpinionBonus));
        DebateManager.Instance.DebatorUI.State = answer.DebatorState;
        if (!SetPublicOpinionValue(earnOpinion))
        {
            if (answer.WinRate <= 0 || _stopFrenesy)
            {
                DebateManager.Instance.Frenesy = 0;
            }
            else
            {
                DebateManager.Instance.Frenesy += 1;
            }
            DebateSequence nextSequence = answer.NextSequence ?? _defaultNextSequence;
            SwitchToNextSequence(nextSequence);
        }
    }
}
