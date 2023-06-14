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


    public void OpenChestwithKey() //control by socket
    {
        chestAnim.SetBool("isOpen", true);
    }

    public void CloseChest() //control by socket
    {
        chestAnim.SetBool("isOpen", false);
    }
}
