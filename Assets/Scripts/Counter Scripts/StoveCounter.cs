using UnityEngine;

public class StoveCounter : BaseCounter
{
    private void Awake()
    {
        SetKitchenObject(GetComponentInChildren<FryingPan>());
        GetKitchenObject().SetKitchenObjectParent(this);
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
    }
}
