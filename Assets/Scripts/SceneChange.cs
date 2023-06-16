using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    //Script that changes the game scene

    public void RoomScene()
    {
        SceneManager.LoadScene("Room");

    }

    public void TitleScene()
    {
        SceneManager.LoadScene("Title");

    }
    public void OptionScene()
    {
        SceneManager.LoadScene("Option");
    }

    public void CreditScene()
    {
        SceneManager.LoadScene("Credit");

    }

    public void StartStoryScene()
    {
        SceneManager.LoadScene("StartStoryScene");

        Invoke("RoomScene", 5);

    }
}
