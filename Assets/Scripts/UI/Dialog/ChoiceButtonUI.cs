using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoiceButtonUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button _button;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    public UnityEngine.UI.Button Button => _button;
    public TextMeshProUGUI Text => _textMeshProUGUI;

    private void Reset()
    {
        _button = GetComponent<UnityEngine.UI.Button>();
        _textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }
}
