using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
