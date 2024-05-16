using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class IAPManager : MonoBehaviour, IDetailedStoreListener
{
    public const string ProductMorning = "morning"; // NonConsumable
    public const string ProductAfternoon = "afternoon"; // NonConsumable
    public const string ProductEvening = "evening"; // NonConsumable

    private const string _android_MorningId = "com.greekuda.runlover.morning";
    //private const string _iOS_MorningId = "test_id1";

    private const string _android_AfternoonId = "com.greekuda.runlover.afternoon";
    //private const string _iOS_AfternoonId = "test_id2";

    private const string _android_EveningId = "com.greekuda.runlover.evening";
    //private const string _iOS_EveningId = "test_id3";

    private static IAPManager m_instance;

    public static IAPManager Instance
    {
        get
        {
            if(m_instance != null) return m_instance;

            m_instance = FindFirstObjectByType<IAPManager>();

            if(m_instance == null) m_instance = new GameObject("IAP Manager").AddComponent<IAPManager>();
            return m_instance;
        }
    }

    private IStoreController storeController;
    private IExtensionProvider storeExtensionProvider;

    public bool IsInitialized => storeController != null && storeExtensionProvider != null;

    void Awake()
    {
        if(m_instance != null && m_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        InitUnityIAP();
    }

    private void InitUnityIAP()
    {
        if(IsInitialized) return;

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(
                ProductMorning, ProductType.NonConsumable, new IDs() {
                    {_android_MorningId, GooglePlay.Name}
                }
        );

        builder.AddProduct(
                ProductAfternoon, ProductType.NonConsumable, new IDs() {
                    {_android_AfternoonId, GooglePlay.Name}
                }
        );

        builder.AddProduct(
                ProductEvening, ProductType.NonConsumable, new IDs() {
                    {_android_EveningId, GooglePlay.Name}
                }
        );

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("Unity IAP Initialized Success");
        storeController = controller;
        storeExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"Unity IAP Initialized Fail {error}");
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message = null)
    {
        var errorMessage = $"Purchasing failed to initialize. Reason : {error}.";

        if (message != null)
        {
            errorMessage += $"More details : {message}";
        }

        Debug.Log(errorMessage);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        Debug.Log($"Purchase Success - ID : {args.purchasedProduct.definition.id}");

        if(args.purchasedProduct.definition.id == ProductMorning)
        {
            Debug.Log("Morning Support...");
        }
        else if(args.purchasedProduct.definition.id == ProductAfternoon)
        {
            Debug.Log("Afternoon Support...");
        }
        else if(args.purchasedProduct.definition.id == ProductEvening)
        {
            Debug.Log("Evening Support...");
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureReason reason)
    {
        Debug.LogWarning($"Purchase Fail - {product.definition.id}, {reason}");
    }

    public void OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureDescription failureDescription)
    {
        Debug.Log($"Purchase failed - Product : {product.definition.id}," +
            $"Purchase failure reason : {failureDescription.reason}," +
            $"Purchase failure details : {failureDescription.message}");
    }

    public void Purchase(string productId)
    {
        if(!IsInitialized) return;

        var product = storeController.products.WithID(productId);

        if (product != null && product.availableToPurchase)
        {
            Debug.Log($"Purchase Try - {product.definition.id}");
            storeController.InitiatePurchase(product);
        }
        else
        {
            Debug.Log($"Purchase Try Fail - {productId}");
        }
    }

    public void RestorePurchase()
    {
        if(!IsInitialized) return;

        if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("Restore Purchase Try");

            var appleExt = storeExtensionProvider.GetExtension<IAppleExtensions>();

            appleExt.RestoreTransactions((success, message) =>
            {
                if(success)
                {
                    Debug.Log($"Restore Purchase Try Success - {message}");
                }
                else
                {
                    Debug.Log($"Restore Purchase Try Fail - {message}");
                }
            });
        }
    }

    public bool HadPurchased(string productId)
    {
        if(!IsInitialized) return false;

        var product = storeController.products.WithID(productId);

        if(product != null)
        {
            return product.hasReceipt;
        }

        return false;
    }
}
