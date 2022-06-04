using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Answer
{
    [field: SerializeField, TextArea(1,2)] public string Text { get; private set; }
    [field: SerializeField] public ScriptableObject Effect { get; private set; }
}

[CreateAssetMenu(fileName = "Question", menuName = "ScriptableObjects/Debate/Question")]
public class Question : ScriptableObject
{
    public const int AnswersMinSize = 3;

    [field: SerializeField, TextArea(3,6)] public string Text { get; private set; }
    [SerializeField] private Answer[] _answers;

    public Answer[] Answers => _answers;

    private void OnValidate()
    {
        if (_answers.Length < AnswersMinSize)
        {
            System.Array.Resize(ref _answers, AnswersMinSize);
        }
    }
}
