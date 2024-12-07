using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private List<CuttableFoodSO> cuttableFoods;

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
            if (cuttableFoods.Find(cuttableFood => cuttableFood.originalFood.objectName == Player.Instance.GetKitchenObject().GetKitchenObjectSO().objectName)) 
            {
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
}
