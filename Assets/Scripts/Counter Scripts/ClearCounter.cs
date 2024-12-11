using UnityEngine;

public class ClearCounter : BaseCounter
{
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
            SetKitchenObject(Player.Instance.GetKitchenObject());
            GetKitchenObject().SetKitchenObjectParent(this);
        }
        else if (!Player.Instance.HasKitchenObject() && HasKitchenObject())
        {
            if (Player.Instance.SetKitchenObject(GetKitchenObject())) 
            {
                Player.Instance.GetKitchenObject().SetKitchenObjectParent(Player.Instance);
            }
        }
        else if (Player.Instance.HasKitchenObject() && HasKitchenObject())
        {
            if (GetKitchenObject() is Plate plate)
            {
                plate.AddKitchenObject(Player.Instance.GetKitchenObject());
            }
        }
    }
}
