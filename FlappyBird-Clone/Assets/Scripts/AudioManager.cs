using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip level;
    public static AudioClip wing;
    public static AudioClip point;
    public static AudioClip hit;
    static AudioSource audioSrc;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        level = Resources.Load<AudioClip>("level");
    }

    private void Start()
    {
        wing = Resources.Load<AudioClip>("wing");
        point = Resources.Load<AudioClip>("point");
        hit = Resources.Load<AudioClip>("hit");
    }

    public static void PlaySound(string clip)
    {
        if (clip == "level")
        {
            audioSrc.Stop();
            audioSrc.loop = true;
            audioSrc.clip = level;
            audioSrc.volume = 0.2f;
            audioSrc.Play();
        }
        if (clip == "wing")
        {
            audioSrc.PlayOneShot(wing);
        }
        else if (clip == "hit")
        {
            audioSrc.PlayOneShot(hit);
        }
        else if (clip == "point")
        {
            audioSrc.PlayOneShot(point);
        }
    }
}
