using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform orders;
    [SerializeField] private Transform individualOrderTemplate;

    private List<Transform> addedOrders;

    private void Awake()
    {
        addedOrders = new List<Transform>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnOrderAdded += DeliveryManager_OnOrderAdded;
        DeliveryManager.Instance.OnOrderServed += DeliveryManager_OnOrderServed;
    }

    private void DeliveryManager_OnOrderServed(object sender, DeliveryManager.OnOrderServedEventArgs e)
    {
        if (e.servedOrderIndex >= 0 && e.servedOrderIndex < addedOrders.Count)
        {
            Destroy(addedOrders[e.servedOrderIndex].gameObject);
            addedOrders.RemoveAt(e.servedOrderIndex);
        }
    }

    private void DeliveryManager_OnOrderAdded()
    {
        ClearExistingOrders();

        List<RecipeSO> activeOrders = DeliveryManager.Instance.GetAllActiveOrders();

        foreach (RecipeSO recipeSO in activeOrders)
        {
            Transform orderTransform = Instantiate(individualOrderTemplate, orders);
            orderTransform.gameObject.SetActive(true);
            orderTransform.GetComponent<OrderUI>().SetRecipeSO(recipeSO);
            addedOrders.Add(orderTransform);
        }
    }

    private void ClearExistingOrders()
    {
        foreach (Transform child in orders)
        {
            Destroy(child.gameObject);
        }

        addedOrders.Clear();
    }
}
