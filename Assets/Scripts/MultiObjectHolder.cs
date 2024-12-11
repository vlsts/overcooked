using System.Collections.Generic;
using UnityEngine;


public class MultiObjectHolder : MonoBehaviour
{
    private List<KitchenObject> kitchenObjects = new List<KitchenObject>();

    public void AddKitchenObject(KitchenObject kitchenObject)
    {
        kitchenObjects.Add(kitchenObject);
    }

    public void RemoveKitchenObject(KitchenObject kitchenObject)
    {
        kitchenObjects.Remove(kitchenObject);
    }

    public void RemoveAllKitchenObjects()
    {
        kitchenObjects.Clear();
    }

    public List<KitchenObject> GetAllKitchenObjects()
    {
        return kitchenObjects;
    }

    public bool HasKitchenObjects()
    {
        return kitchenObjects.Count > 0;
    }
}

