using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BGMController : MonoBehaviour
{
    static public BGMController instance;
    public GameObject BGM;
    public AudioSource audio;


    void Start()
    {
        audio = BGM.GetComponent<AudioSource>();
        //DontDestroyOnLoad(this);    //keep playing audio when the scene is changed

         if(instance == null)
         {
            instance = this;
             DontDestroyOnLoad(BGM);    //keep playing audio when the scene is changed
         }else Destroy(BGM); //keep playing audio when the scene is changed
    }

    void Update()
    {
        if (audio.isPlaying) return;    //if audio is playing, keep playing
        else
        {
            audio.Play();   //if audio is not playing, start to play the audio
        }
    }
}
