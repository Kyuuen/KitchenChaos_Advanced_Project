using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangeManager : MonoBehaviour
{
    public static ArrangeManager Instance { get; private set; }

    private Transform markedCounter;

    private void Awake()
    {
        Instance = this;
    }

    public void SwapCounter(Transform swapCounter)
    {
        Vector3 tempPos = markedCounter.position;
        Quaternion tempRot = markedCounter.rotation;

        markedCounter.position = swapCounter.position;
        markedCounter.rotation = swapCounter.rotation;

        swapCounter.position = tempPos;
        swapCounter.rotation = tempRot;
        UnsetMarkedCounter();
    }

    public Transform GetMarkedCounter()
    {
        return markedCounter;
    }

    public void SetMarkedCounter(Transform markedCounter)
    {
        this.markedCounter = markedCounter;
    }

    public void UnsetMarkedCounter()
    {
        markedCounter.GetComponent<BaseCounter>().HideMark();
        markedCounter = null;
    }

    public bool HasMarkedCounter()
    {
        return markedCounter != null;
    }
}
