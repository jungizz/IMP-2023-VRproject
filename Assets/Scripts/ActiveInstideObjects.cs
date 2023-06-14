using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class ActiveInstideObjects : MonoBehaviour
{
    public GameObject insideObject;
    public GameObject gemsObject;

    //There is a phenomenon in which the position is moved at the start due to Chest's animation.
    //So, when I put the item in the Chest and run it, the item bounces off.
    //To prevent this, the game was played and the object was activated one second later.
    private void Start()
    {
        Invoke("ObjectActive", 1f);
    }

    private void ObjectActive()
    {
        insideObject.SetActive(true);
        Transform[] myChildren = gemsObject.GetComponentsInChildren<Transform>();
        foreach (Transform gem in myChildren)
        {
            gem.gameObject.SetActive(true);
        }
    }

}
