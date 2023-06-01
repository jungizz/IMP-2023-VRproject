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

    public Text[] timeText;
    float time = 120;
    int min, sec;

    void Start()
    {
        timeText[0].text = "02";
        timeText[1].text = "00";
    }

    void Update()
    {
        time -= Time.deltaTime;

        min = (int)time / 60;
        sec = ((int)time - min * 60) % 60;

        if(min <= 0 && sec <= 0)
        {
            timeText[0].text = 0.ToString();
            timeText[1].text = 0.ToString();
            Time.timeScale = 0;
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
            }

            if (collision.gameObject == GameObject.Find("doll2"))
            {
                check2.SetActive(true);
            }

            if (collision.gameObject == GameObject.Find("sword"))
            {
                check3.SetActive(true);
            }

            if (collision.gameObject == GameObject.Find("plant"))
            {
                check4.SetActive(true);
            }
        }
    }
}
