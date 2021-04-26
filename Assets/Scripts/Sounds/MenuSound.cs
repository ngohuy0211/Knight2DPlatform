using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour
{
    public AudioSource backgroundAudio;
    void Start()
    {
        backgroundAudio.Play();
    }
}
