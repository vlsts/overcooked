using System;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            KitchenObject kitchenObject = player.GetKitchenObject();

            if (kitchenObject is Plate plate)
            {
                if (!plate.IsDirty())
                    plate.RemoveKitchenObject();
            }
            else if (kitchenObject is FryingPan fryingPan)
            {
                fryingPan.GetKitchenObject()?.DestroySelf();
                fryingPan.RemoveKitchenObject();
            }
            else
            {
                kitchenObject.DestroySelf();
            }
        }
    }
}
