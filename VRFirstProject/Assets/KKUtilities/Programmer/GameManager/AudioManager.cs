﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : BaseManager<AudioManager>
{
    AudioSource audioSource;
    AudioSource loopAudioSource;

    GameObject oneShotAudioOriginal;

    [SerializeField]
    List<AudioClip> clipList = new List<AudioClip>();

    protected override void Start()
    {
        base.Start();

        oneShotAudioOriginal = transform.GetChild(0).gameObject;
        AudioSource[] temp = GetComponents<AudioSource>();

        audioSource = temp[0];
        loopAudioSource = temp[1];
    }

    protected override void OnDestroy()
    {
        loopAudioSource.Stop();
        base.OnDestroy();
    }

    public void Play(string clipName)
    {
        loopAudioSource.clip = GetAudioClip(clipName);

        if (loopAudioSource.clip == null) return;
        loopAudioSource.Play();
    }

    public void PlayOneShot(string clipName, float volume = 1.0f)
    {
        AudioClip clip = GetAudioClip(clipName);

        if (clip == null) return;
        audioSource.PlayOneShot(clip, volume);
    }

    public AudioSource PlayOneShot(string clipName, Vector3 position, float volume = 1.0f)
    {
        AudioClip clip = GetAudioClip(clipName);
        
        if (clip == null) return null;
        AudioSource temp = Instantiate(oneShotAudioOriginal, position, Quaternion.identity).GetComponent<AudioSource>();

        temp.clip = clip;
        temp.volume = volume;
        temp.transform.parent = transform;
        Destroy(temp.gameObject, clip.length);
        temp.Play();

        return temp;
    }

    AudioClip GetAudioClip(string clipName)
    {
        AudioClip clip = clipList.Find(n => n.name == clipName);

        if (clip != null) return clip;

        clip = Resources.Load<AudioClip>(clipName);

        if (clip != null)
        {
            clipList.Add(clip);
            return clip;
        }

        clip = Resources.Load<AudioClip>("Audios/" + clipName);

        if (clip != null)
        {
            clipList.Add(clip);
            return clip;
        }

        return null;
    }
}
