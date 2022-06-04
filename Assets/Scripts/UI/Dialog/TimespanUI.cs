using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimespanUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image _imageFill;
    [SerializeField, Range(0f, 1f)] private float _value;

    public float Value
    {
        get => _value;
        set
        {
            float newValue = Mathf.Clamp(value, 0f, 1f);
            if (_value != newValue)
            {
                _value = newValue;
                _imageFill.fillAmount = newValue;
            }
        }
    }
}
