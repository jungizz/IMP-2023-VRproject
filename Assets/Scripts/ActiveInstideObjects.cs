using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class ActiveInstideObjects : MonoBehaviour
{
    public GameObject insideObject;
    public GameObject gemsObject;


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
