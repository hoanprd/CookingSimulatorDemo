using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterManager : MonoBehaviour
{
    public void MoreTimeButton()
    {
        CustomerManager.moreTimeManager = true;
        Customer.moreTimeCustomer = true;
    }
}
