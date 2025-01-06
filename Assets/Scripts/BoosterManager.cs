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

    public void CookRawButton()
    {
        DragAndDropSprite.useRawCookBooster = true;
    }

    public void FullFillButton()
    {
        Customer.eatSuccess = 1;
    }
}
