using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Plate : KitchenObject, IKitchenObjectParent
{
    [SerializeField] private List<KitchenObjectSO> servableKitchenObjectsSO;
    [SerializeField] private Transform holdPoint;

    public event Action OnIngredientAdded;
    public event Action OnClearPlate;
    public static event EventHandler OnFoodItemAdded;

    private MultiObjectHolder multiObjectHolder;
    private bool isClean = true;

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
        if (IsValidKitchenObject(kitchenObject) && isClean)
        {
            kitchenObject.SetKitchenObjectParent(this);
            multiObjectHolder.AddKitchenObject(kitchenObject);
            OnIngredientAdded?.Invoke();
            OnFoodItemAdded?.Invoke(this, EventArgs.Empty);
            return true;
        }
        else if (kitchenObject.GetKitchenObjectSO().name == "Leftovers")
        {
            isClean = false;
            kitchenObject.SetKitchenObjectParent(this);
            multiObjectHolder.AddKitchenObject(kitchenObject);
        }
        return false;
    }

    public bool IsDirty()
    {
        return !isClean;
    }

    public void Clean()
    {
        isClean = true;
        RemoveKitchenObject();
    }

    public List<KitchenObject> GetAllKitchenObjects()
    {
        return multiObjectHolder.GetAllKitchenObjects();
    }

    public static void ResetStaticEvents()
    {
        OnFoodItemAdded = null;
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
            Destroy(kitchenObject.gameObject);
        }
        multiObjectHolder.RemoveAllKitchenObjects();
        OnClearPlate?.Invoke();
    }

    public bool SetKitchenObject(KitchenObject kitchenObject)
    {
        return false;
    }
}
