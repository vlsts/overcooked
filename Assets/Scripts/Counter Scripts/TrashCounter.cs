using System;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnTrash;
    private bool trashedObject = false;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            KitchenObject kitchenObject = player.GetKitchenObject();

            if (kitchenObject is Plate plate)
            {
                if (!plate.IsDirty())
                    plate.RemoveKitchenObject();
                trashedObject = true;
            }
            else if (kitchenObject is FryingPan fryingPan)
            {
                if (fryingPan.HasKitchenObject())
                {
                    fryingPan.GetKitchenObject().DestroySelf();
                    fryingPan.RemoveKitchenObject();
                    trashedObject = true;
                }
            }
            else
            {
                kitchenObject.DestroySelf();
                trashedObject = true;
            }

            if (trashedObject)
            {
                OnTrash?.Invoke(this, EventArgs.Empty);
                trashedObject = false;
            }

        }
    }

    public static void ResetStaticEvents()
    {
        OnTrash = null;
    }
}
