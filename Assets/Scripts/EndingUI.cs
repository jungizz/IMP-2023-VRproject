using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingUI : MonoBehaviour
{
    public GameObject ResultText;
    public GameObject EndingText;
    public GameObject BackButton;
    bool check = false;

    void Start()
    {
        
    }

    void Update()
    {
        Destroy(ResultText, 4f);    //remove the result text after 4 seconds
        Invoke("Ending", 4.5f);   //show the ending text after 4.5 seconds
    }

    void Ending()
    {
        EndingText.SetActive(true);
        BackButton.SetActive(true);
    }
}
