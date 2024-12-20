using UnityEngine;

public class ClearCounter : BaseCounter
{
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
                if (Player.Instance.GetKitchenObject() is not IKitchenObjectParent)
                {
                    plate.AddKitchenObject(Player.Instance.GetKitchenObject());
                }
                else if (Player.Instance.GetKitchenObject() is FryingPan fryingPan && fryingPan.HasKitchenObject())
                {
                    plate.AddKitchenObject(fryingPan.GetKitchenObject());
                }
            }
            else if (GetKitchenObject() is FryingPan fryingPan)
            {
                if (Player.Instance.GetKitchenObject() is not IKitchenObjectParent)
                {
                    if (fryingPan.SetKitchenObject(Player.Instance.GetKitchenObject()))
                        fryingPan.GetKitchenObject().SetKitchenObjectParent(fryingPan);
                }
            }
            else
            {
                if (Player.Instance.GetKitchenObject() is IKitchenObjectParent)
                {
                    if (Player.Instance.GetKitchenObject() is Plate playersPlate)
                    {
                        playersPlate.AddKitchenObject(GetKitchenObject());
                    }
                    else if (Player.Instance.GetKitchenObject() is FryingPan playersFryingPan)
                    {
                        if (playersFryingPan.SetKitchenObject(GetKitchenObject()))
                            playersFryingPan.GetKitchenObject().SetKitchenObjectParent(playersFryingPan);
                    }
                }
            }
        }
    }
}
