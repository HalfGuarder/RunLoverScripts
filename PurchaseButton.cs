using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseButton : MonoBehaviour
{

    public string targetProductId;

    public void HandleClick()
    {
        if(targetProductId == IAPManager.ProductMorning
            || targetProductId == IAPManager.ProductAfternoon
            || targetProductId == IAPManager.ProductEvening)
        {
            if(IAPManager.Instance.HadPurchased(targetProductId))
            {
                Debug.Log("Purchased Already");
                GameManager.Instance.purchaseCheckPanel.SetActive(false);
                GameManager.Instance.alreadyPurchase.SetActive(true);
                return;
            }
        }

        if(targetProductId == IAPManager.ProductMorning)
        {
            GameManager.Instance.purchaseCheckPanel.SetActive(false);
            GameManager.Instance.ThanksForSupport();
            Debug.Log("ThanksSupportMorning");
        }

        if(targetProductId == IAPManager.ProductAfternoon)
        {
            GameManager.Instance.purchaseCheckPanel.SetActive(false);
            GameManager.Instance.ThanksForSupport();
            Debug.Log("ThanksSupportAfternoon");
        }

        if(targetProductId == IAPManager.ProductEvening)
        {
            GameManager.Instance.purchaseCheckPanel.SetActive(false);
            GameManager.Instance.ThanksForSupport();
            Debug.Log("ThanksSupportEvening");
        }

        IAPManager.Instance.Purchase(targetProductId);
    }
}