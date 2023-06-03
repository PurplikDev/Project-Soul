using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource source;
    public void PlaySound()
    {
        source.PlayOneShot(clip);
    }
}
