using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public GameObject check1;
    public GameObject check2;
    public GameObject check3;
    public GameObject check4;


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
