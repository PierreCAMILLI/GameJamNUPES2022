using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(AudioSource))]
public class PublicOpinionUI : MonoBehaviour
{
    [SerializeField, HideInInspector] private AudioSource _audioSource;
    [SerializeField] private UnityEngine.UI.Image _gaugeFill;
    [SerializeField] private int _maxValue = 100;
    [SerializeField] private int _value = 50;
    [SerializeField] private float _gaugeSpeed = 5f;

    [Space]
    [SerializeField] private Color[] _gaugeColors;

    [Space]
    [SerializeField] private AudioClip _argumentSuccess;
    [SerializeField] private AudioClip _argumentBigSuccess;
    [SerializeField] private AudioClip _argumentFailure;
    [SerializeField] private AudioClip _argumentBigFailure;

    private float _smoothDampValue;
    private float _velocity;

    public int MaxValue
    {
        get => _maxValue;
        set => _maxValue = value;
    }

    public int Value
    {
        get => _value;
        set
        {
            PlayEvolutionSound(value - _value);
            _value = value;
        }
    }

    private void Start()
    {
        _smoothDampValue = ToLerpValue(_value);
    }

    // Update is called once per frame
    void Update()
    {
        float lerp = ToLerpValue(_value);

        _smoothDampValue = Mathf.SmoothDamp(_smoothDampValue, lerp, ref _velocity, _gaugeSpeed == 0f ? 0f : 1f / _gaugeSpeed, Mathf.Infinity, Time.deltaTime);
        _gaugeFill.fillAmount = _smoothDampValue;
        _gaugeFill.color = GetGaugeColor(_smoothDampValue);
    }

    private void OnValidate()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetValueWithoutTransition(int value)
    {
        _value = value;
        _smoothDampValue = ToLerpValue(_value);
    }

    private float ToLerpValue(int value)
    {
        _value = Mathf.Clamp(value, 0, _maxValue);
        return Mathf.InverseLerp(0, _maxValue, value);
    }

    private Color GetGaugeColor(float lerpValue)
    {
        if (_gaugeColors.Length == 0)
        {
            return Color.black;
        }
        if (_gaugeColors.Length == 1)
        {
            return _gaugeColors[0];
        }

        float position = lerpValue * (_gaugeColors.Length - 1);
        int lowerPosition = Mathf.FloorToInt(position);
        int upperPosition = Mathf.CeilToInt(position);

        return Color.Lerp(_gaugeColors[lowerPosition], _gaugeColors[upperPosition], position - lowerPosition);
    }

    public void PlayEvolutionSound(int evolution)
    {
        if (evolution >= 50)
        {
            _audioSource.clip = _argumentBigSuccess;
        }
        else if (evolution < 50 && evolution > 0)
        {
            _audioSource.clip = _argumentSuccess;
        }
        else if (evolution < 0 && evolution > -50)
        {
            _audioSource.clip = _argumentFailure;
        }
        else if (evolution <= 50)
        {
            _audioSource.clip = _argumentBigFailure;
        }
        else
        {
            _audioSource.clip = null;
        }

        _audioSource.Play();
    }
}
