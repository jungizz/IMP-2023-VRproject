using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour
{
    public GameObject check1;
    public GameObject check2;
    public GameObject check3;
    public GameObject check4;
    public GameObject check5;
    public GameObject check6;
    public GameObject check7;

    public Text[] timeText;
    float time = 5;
    int min, sec;
    int itemCount = 0;

    public GameObject FailText;
    public GameObject SuccessText;
    public GameObject OverText;
    public GameObject ClearText;
    public GameObject BackButton;
    public bool successCheck = false;
    public bool failCheck = false;

    public GameObject timerText1;
    public GameObject timerText2;
    public GameObject timerText3;
    public GameObject itemPanel;

    void Start()
    {
        timeText[0].text = "02";
        timeText[1].text = "00";
        itemCount = 0;
    }

    void Update()
    {
        time -= Time.deltaTime;

        min = (int)time / 60;
        sec = ((int)time - min * 60) % 60;

        if(itemCount >= 7)
        {
            timerText1.SetActive(false);
            timerText2.SetActive(false);
            timerText3.SetActive(false);
            itemPanel.SetActive(false);

            //SuccessText.SetActive(true); //show the result text
            //Destroy(SuccessText, 4f);    //remove the result text after 4 seconds
            StartCoroutine(Success());
            Invoke("SuccessEnding", 4.5f);   //show the ending text after 4.5 seconds

            successCheck = true;    //not using yet
        }

        //Timer
        if(min <= 0 && sec <= 0)
        {
            timeText[0].text = 0.ToString();
            timeText[1].text = 0.ToString();
            //Time.timeScale = 0;

            failCheck = true;

            if (itemCount < 7 && failCheck == true)
            {
                timerText1.SetActive(false);
                timerText2.SetActive(false);
                timerText3.SetActive(false);
                itemPanel.SetActive(false);

                //FailText.SetActive(true); //show the result text
                //Destroy(FailText, 4f);    //remove the result text after 4 seconds
                StartCoroutine(Fail());
                Invoke("FailEnding", 4.5f);   //show the ending text after 4.5 seconds

                failCheck = false;
            }
        }
        else
        {
            if(sec >= 60)
            {
                min += 1;
                sec -= 60;
            }
            else
            {
                timeText[0].text = min.ToString();
                timeText[1].text = sec.ToString();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Item"))
        {
            if(collision.gameObject == GameObject.Find("doll1"))
            {
                check1.SetActive(true);
                itemCount++;
            }

            if (collision.gameObject == GameObject.Find("doll2"))
            {
                check2.SetActive(true);
                itemCount++;
            }

            if (collision.gameObject == GameObject.Find("sword"))
            {
                check3.SetActive(true);
                itemCount++;
            }

            if (collision.gameObject == GameObject.Find("plant"))
            {
                check4.SetActive(true);
                itemCount++;
            }

            if (collision.gameObject == GameObject.Find("doll3"))
            {
                check5.SetActive(true);
                itemCount++;
            }

            if (collision.gameObject == GameObject.Find("lighter"))
            {
                check6.SetActive(true);
                itemCount++;
            }

            if (collision.gameObject == GameObject.Find("smartphone"))
            {
                check7.SetActive(true);
                itemCount++;
            }
        }
    }

    void SuccessEnding()
    {
        ClearText.SetActive(true);
        BackButton.SetActive(true);
    }

    void FailEnding()
    {
        OverText.SetActive(true);
        BackButton.SetActive(true);
    }

    IEnumerator Success()
    {
        SuccessText.SetActive(true); //show the result text
        yield return new WaitForSeconds(4.0f);  //disable the result text after 4 seconds
        SuccessText.SetActive(false);
    }

    IEnumerator Fail()
    {
        FailText.SetActive(true); //show the result text
        yield return new WaitForSeconds(4.0f);  //disable the result text after 4 seconds
        FailText.SetActive(false);
    }
}
