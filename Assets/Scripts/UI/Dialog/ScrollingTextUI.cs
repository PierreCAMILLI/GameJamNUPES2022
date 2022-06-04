using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScrollingTextUI : MonoBehaviour
{
    [SerializeField, HideInInspector] private TextMeshProUGUI _tmpText;
    [SerializeField] private bool _activated = true;
    [SerializeField] private float _charsPerSecond;
    [Tooltip("Requires Rich Text enabled")]
    [SerializeField] private bool _fixWordJumping = true;
    [SerializeField, TextArea(3,6)] private string _text;

    private float _startScrollingTime;
    private int _previousTextLength;
    private System.Action _onTextFullyDisplayed;

    public TextMeshProUGUI TextMeshProUGUI => _tmpText;
    public string Text => _text;

    private void Start()
    {
        _startScrollingTime = Time.time;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateScrollingText();
    }

    private void OnValidate()
    {
        _tmpText = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string text, System.Action onTextFullyDisplayed = null)
    {
        _text = text;
        _startScrollingTime = Time.time;
        _onTextFullyDisplayed = onTextFullyDisplayed;

        if (_activated)
        {
            _tmpText.text = string.Empty;
            _previousTextLength = 0;
        }
        else
        {
            _tmpText.text = text;
            _previousTextLength = text.Length;
            _onTextFullyDisplayed?.Invoke();
        }
    }

    public void ShowAllText()
    {
        if (_previousTextLength != _text.Length)
        {
            _tmpText.text = _text;
            _previousTextLength = _text.Length;
            _onTextFullyDisplayed?.Invoke();
        }
    }

    private void UpdateScrollingText()
    {
        if (_activated)
        {
            int charactersToShowCount = Mathf.RoundToInt((Time.time - _startScrollingTime) * _charsPerSecond);
            charactersToShowCount = Mathf.Min(charactersToShowCount, _text.Length);
            charactersToShowCount = Mathf.Max(charactersToShowCount, _previousTextLength);
            if (charactersToShowCount != _previousTextLength)
            {
                if (_fixWordJumping && _tmpText.richText)
                {
                    _tmpText.text = $"{_text.Substring(0, charactersToShowCount)}<alpha=#00>{_text.Substring(charactersToShowCount)}</alpha>";
                }
                else
                {
                    _tmpText.text = _text.Substring(0, charactersToShowCount);
                }
                _previousTextLength = charactersToShowCount;
                if (charactersToShowCount == _text.Length)
                {
                    _onTextFullyDisplayed?.Invoke();
                }
            }
        }
        else
        {
            ShowAllText();
        }
    }
}
