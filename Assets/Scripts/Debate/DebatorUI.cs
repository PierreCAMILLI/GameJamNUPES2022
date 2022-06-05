using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DebatorUI : MonoBehaviour, ISerializationCallbackReceiver
{
    [System.Serializable]
    public struct Sound
    {
        public DebatorState state;
        public AudioClip sound;
    }

    [SerializeField, HideInInspector] private Animator _animator;
    [SerializeField, HideInInspector] private AudioSource _audioSource;
    [SerializeField] private Sound[] _sounds;

    private DebatorState _state;
    private bool _isTalking;
    private IDictionary<DebatorState, AudioClip> _soundsDictionary;


    public DebatorState State
    {
        get => _state;
        set
        {
            _state = value;
            _animator.SetTrigger(value.ToString());
            if (_soundsDictionary.TryGetValue(value, out AudioClip audio))
            {
                _audioSource.clip = audio;
                _audioSource.Play();
            }
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

    private void OnValidate()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        _soundsDictionary = _sounds.ToDictionary(s => s.state, s => s.sound);
    }
}
