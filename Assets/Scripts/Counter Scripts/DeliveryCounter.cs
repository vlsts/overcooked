using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO leftovers;

    public override void Interact(Player player)
    {
        if (Player.Instance.GetKitchenObject() is Plate plate)
        {
            if (plate.HasKitchenObject())
            {
                DeliveryManager.Instance.DeliverPlate(plate.GetAllKitchenObjects());
                plate.RemoveKitchenObject();
                plate.AddKitchenObject(Instantiate(leftovers.prefab).GetComponent<KitchenObject>());
            }
        }
    }
}
