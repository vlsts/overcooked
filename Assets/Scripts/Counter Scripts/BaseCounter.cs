using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform holdPoint;

    private KitchenObject kitchenObject;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public virtual void Interact(Player player)
    {
        Debug.Log("BaseCounter.Interact()");
    }

    public bool SetKitchenObject(KitchenObject placedKitchenObject)
    {
        if (!kitchenObject)
        {
            kitchenObject = placedKitchenObject;
            return true;
        }
        return false;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void RemoveKitchenObject()
    {
        kitchenObject = null;
    }

    public Transform GetHoldPoint()
    {
        return holdPoint;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
