using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UniRx;
using System;

public class CinemachineController : MonoBehaviour
{
    [ReadOnly(false), SerializeField] private PlayableDirector playableDirector;
    [ReadOnly(true), SerializeField] private TimelineAsset[] timelineAssets;
    [ReadOnly(true), SerializeField] private KeyCode command;

    private void OnValidate()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    private void Start()
    {
        Observable
            .EveryUpdate()
            .Where(_ => Input.GetKeyDown(command))
            .AsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(1f))
            .Subscribe(delegate { PlayCinemachine(); })
            .AddTo(gameObject);
    }

    private void PlayCinemachine()
    {
        playableDirector.Play(timelineAssets[0]);
    }
}