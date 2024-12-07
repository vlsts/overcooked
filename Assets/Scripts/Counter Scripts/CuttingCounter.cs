using System;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private List<CuttableFoodSO> cuttableFoods;

    public event Action OnObjectCut;

    private int currentCuts;
    private CuttableFoodSO currentCuttableObject;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject() && Player.Instance.HasKitchenObject())
        {
            if (cuttableFoods.Find(cuttableFood => cuttableFood.originalFood.objectName == Player.Instance.GetKitchenObject().GetKitchenObjectSO().objectName) is CuttableFoodSO matchingCuttableFood) 
            {
                currentCuttableObject = matchingCuttableFood;
                SetKitchenObject(Player.Instance.GetKitchenObject());
                GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else if (!Player.Instance.HasKitchenObject() && HasKitchenObject())
        {
            if (Player.Instance.SetKitchenObject(GetKitchenObject())) 
            {
                Player.Instance.GetKitchenObject().SetKitchenObjectParent(Player.Instance);
            }
        }
    }

    public override void InteractSecondary(Player player)
    {
        if (GetKitchenObject().GetKitchenObjectSO().objectName == currentCuttableObject.originalFood.objectName)
        {
            currentCuts++;
            OnObjectCut?.Invoke();
            if (currentCuts >= currentCuttableObject.necessaryCuts)
            {
                GetKitchenObject().DestroySelf();
                KitchenObject.TrySpawnKitchenObject(currentCuttableObject.cutFood, this);
                currentCuts = 0;
            }
        }
    }
}
