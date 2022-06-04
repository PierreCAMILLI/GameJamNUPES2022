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

    public string Text
    {
        get => _text;
        set
        {
            _text = value; ;
            _startScrollingTime = Time.time;
            
            if (_activated)
            {
                _tmpText.text = value;
                _previousTextLength = 0;
            }
            else
            {
                _tmpText.text = value;
                _previousTextLength = value.Length;
            }

        }
    }

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

    public void ShowAllText()
    {
        _tmpText.text = _text;
        _previousTextLength = _text.Length;
    }

    private void UpdateScrollingText()
    {
        if (_activated)
        {
            int charactersToShowCount = Mathf.RoundToInt((Time.time - _startScrollingTime) * _charsPerSecond);
            charactersToShowCount = Mathf.Min(charactersToShowCount, _text.Length);
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
            }
        }
        else if (_previousTextLength != _text.Length)
        {
            ShowAllText();
        }
    }
}
