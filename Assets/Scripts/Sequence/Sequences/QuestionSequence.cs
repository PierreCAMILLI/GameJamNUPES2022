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
    [SerializeField] private DebateSequence _victorySequence;
    [SerializeField] private DebateSequence _defeatSequence;
    [SerializeField] private DebateSequence _defaultNextSequence;
    
    [Space]
    [SerializeField] private Answer[] _answers;

    public string Text => _text;
    public int Multiplier => _multiplier;
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
        scrollingText.SetText(_text, ShowAnswers);
    }

    public override void OnEnd()
    {
        DebateManager.Instance.ChoicesGeneratorUI.RemoveAllButtons();
    }

    public void ShowAnswers()
    {
        foreach (Answer answer in _answers)
        {
            DebateManager.Instance.ChoicesGeneratorUI.AddButton(answer);
        }
    }

    public void HandleButtonPressed(Answer answer)
    {
        int newOpinion = DebateManager.Instance.PublicOpinion += answer.WinRate * answer.Question.Multiplier;
        if (newOpinion >= 100)
        {
            SwitchToNextSequence(_victorySequence);
            return;
        }
        if (newOpinion <= 0)
        {
            SwitchToNextSequence(_defeatSequence);
            return;
        }

        DebateSequence nextSequence = answer.NextSequence ?? _defaultNextSequence;
        SwitchToNextSequence(nextSequence);
    }
}
