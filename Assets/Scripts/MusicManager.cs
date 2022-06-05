using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Toolset.SingletonBehaviour<MusicManager>
{
    [SerializeField] private AudioSource _normal;
    [SerializeField] private AudioSource _high;

    private void Start()
    {
        PlayNormal();
    }

    public void PlayNormal()
    {
        _normal.volume = 1f;
        _high.volume = 0f;
    }

    public void PlayHigh()
    {
        _normal.volume = 0f;
        _high.volume = 1f;
    }

}
