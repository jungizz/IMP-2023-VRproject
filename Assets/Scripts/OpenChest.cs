using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    private Animator chestAnim;

    private void Start()
    {
        chestAnim = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            chestAnim.SetBool("isOpen", true);
            //Invoke("CloseChest", 5f);
        }
    }

    void CloseChest()
    {
        chestAnim.SetBool("isOpen", false);
    }
}
