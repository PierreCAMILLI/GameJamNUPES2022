using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ChoicesGeneratorUI : MonoBehaviour
{
    [SerializeField] private ChoiceButtonUI _choiceButtonPrefab;

    private List<ChoiceButtonUI> _buttons;
    private ObjectPool<ChoiceButtonUI> _buttonsPool;

    // Start is called before the first frame update
    void Start()
    {
        _buttons = new List<ChoiceButtonUI>();
        _buttonsPool = new ObjectPool<ChoiceButtonUI>(
            () => Instantiate(_choiceButtonPrefab, transform),
            button => {
                button.gameObject.SetActive(true);
                button.transform.SetAsLastSibling();
                },
            button => button.gameObject.SetActive(false),
            button => Destroy(button),
            true,
            7
            );
    }

    public void AddButton(QuestionSequence.Answer answer)
    {
        ChoiceButtonUI button = _buttonsPool.Get();
        button.Text.text = answer.Title;
        button.Button.onClick.RemoveAllListeners();
        button.Button.onClick.AddListener(() => {
            answer.Question.HandleButtonPressed(answer);
        });

        _buttons.Add(button);
    }

    [ContextMenu("Add Button")]
    public void AddButton_Editor()
    {
        ChoiceButtonUI button = _buttonsPool.Get();
        button.Text.text = "Mock Text";

        _buttons.Add(button);
    }

    [ContextMenu("Remove Buttons")]
    public void RemoveAllButtons()
    {
        foreach (ChoiceButtonUI button in _buttons)
        {
            _buttonsPool.Release(button);
        }
        _buttons.Clear();
    }
}
