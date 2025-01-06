using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public float timeWait, ogWaitTime;
    public GameObject customerPrefab;

    void Start()
    {
        ogWaitTime = timeWait;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeWait > 0)
        {
            timeWait -= Time.deltaTime;
        }
        else
        {
            timeWait = ogWaitTime;
            Instantiate(customerPrefab, transform.position, Quaternion.identity);
        }
    }
}