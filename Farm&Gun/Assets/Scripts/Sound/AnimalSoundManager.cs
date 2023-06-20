using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip defaultSound;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        PlayAtRandomTime(defaultSound);
    }

    private void PlayAtRandomTime(AudioClip clip)
    {
        source.clip = clip;
        source.time = Random.Range(0, clip.length);
        source.Play();
    }
}
