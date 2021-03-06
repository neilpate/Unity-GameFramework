using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomEatMunchyAudioClipPlayer : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> AudioClips;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        //Pick a random clip to play
        var index = UnityEngine.Random.Range(0, AudioClips.Count);

        audioSource.clip = AudioClips[index];
        audioSource.Play();
    }


}
