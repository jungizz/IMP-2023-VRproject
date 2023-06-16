using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider bgm;
    public Slider effect;


    public void SetBGM()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(bgm.value) * 20);    //control the bgm sound
    }
}
