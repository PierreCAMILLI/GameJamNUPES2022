using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebatorUI : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private DebatorState _state;
    private bool _isTalking;

    public DebatorState State
    {
        get => _state;
        set
        {
            _state = value;
            _animator.SetTrigger(value.ToString());
        }
    }

    public bool IsTalking
    {
        get => _isTalking;
        set
        {
            _isTalking = value;
            _animator.SetBool("Talking", value);
        }
    }

    private void Reset()
    {
        _animator = GetComponent<Animator>();
    }
}
