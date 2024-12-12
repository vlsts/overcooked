using UnityEngine;

public class FryingPan : KitchenObject, IKitchenObjectParent
{
    [SerializeField] private Transform holdPoint;
    [SerializeField] private KitchenObjectSO meatKitchenObjectSO;

    private KitchenObject meatKitchenObject;

    void Start()
    {

    }

    void Update()
    {
    }

    public Transform GetHoldPoint()
    {
        return holdPoint;
    }

    public KitchenObject GetKitchenObject()
    {
        return meatKitchenObject;
    }

    public bool HasKitchenObject()
    {
        return meatKitchenObject != null;
    }

    public void RemoveKitchenObject()
    {
        meatKitchenObject = null;
    }

    public bool SetKitchenObject(KitchenObject kitchenObject)
    {
        if (kitchenObject.GetKitchenObjectSO().name == meatKitchenObjectSO.name)
        {
            meatKitchenObject = kitchenObject;
            return true;
        }
        return false;
    }
}
