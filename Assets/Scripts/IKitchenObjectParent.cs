using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{
    public bool SetKitchenObject(KitchenObject kitchenObject);

    public KitchenObject GetKitchenObject();

    public void RemoveKitchenObject();

    public Transform GetHoldPoint();

    bool HasKitchenObject();
}
