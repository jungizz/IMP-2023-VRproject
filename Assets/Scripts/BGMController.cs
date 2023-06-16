using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class BGMController : MonoBehaviour
{
    static public BGMController instance;
    public GameObject BGM;
    public AudioSource audio;


    void Start()
    {
        audio = BGM.GetComponent<AudioSource>();

         if(instance == null)
         {
            instance = this;
             DontDestroyOnLoad(BGM);    //keep playing audio when the scene is changed
         }else Destroy(BGM); //keep playing audio when the scene is changed
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Room")
        {
            if (audio.isPlaying) return;    //if audio is playing, keep playing
            else
            {
                audio.Play();   //if audio is not playing, start to play the audio
            }
        }
        else if (SceneManager.GetActiveScene().name == "Room")
        {
            Destroy(BGM);
        }
 
    }
}
