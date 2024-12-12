using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Plate : KitchenObject, IKitchenObjectParent
{
    [SerializeField] private List<KitchenObjectSO> servableKitchenObjectsSO;
    [SerializeField] private Transform holdPoint;

    private MultiObjectHolder multiObjectHolder;

    private void Awake()
    {
        multiObjectHolder = gameObject.AddComponent<MultiObjectHolder>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private bool IsValidKitchenObject(KitchenObject kitchenObject)
    {
        foreach (var validObject in servableKitchenObjectsSO)
        {
            if (validObject.name == kitchenObject.GetKitchenObjectSO().name)
            {
                return true;
            }
        }
        return false;
    }

    public bool AddKitchenObject(KitchenObject kitchenObject)
    {
        if (IsValidKitchenObject(kitchenObject))
        {
            kitchenObject.SetKitchenObjectParent(this);
            multiObjectHolder.AddKitchenObject(kitchenObject);
            return true;
        }
        return false;
    }

    public void RemoveKitchenObject(KitchenObject kitchenObject)
    {
        multiObjectHolder.RemoveKitchenObject(kitchenObject);
    }

    public List<KitchenObject> GetAllKitchenObjects()
    {
        return multiObjectHolder.GetAllKitchenObjects();
    }

    //Interface implementations

    public Transform GetHoldPoint()
    {
        return holdPoint;
    }

    public KitchenObject GetKitchenObject()
    {
        return null;
    }

    public bool HasKitchenObject()
    {
        return multiObjectHolder.HasKitchenObjects();
    }

    public void RemoveKitchenObject()
    {
        foreach (var kitchenObject in multiObjectHolder.GetAllKitchenObjects())
        {
            multiObjectHolder.RemoveKitchenObject(kitchenObject);
        }
    }

    public bool SetKitchenObject(KitchenObject kitchenObject)
    {
        return false;
    }
}
