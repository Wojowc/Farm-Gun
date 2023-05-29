using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAtRandomMoment : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]
    AudioClip startingClip;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        PlayAtRandom(startingClip);
    }

    public void PlayAtRandom(AudioClip clip)
    {
        float randomTime = Random.Range(0, clip.length);
        audioSource.clip = clip;
        audioSource.time = randomTime;
        audioSource.Play();
    }
}
