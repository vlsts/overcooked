using System;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO leftovers;

    public static DeliveryCounter Instance { get; private set; }

    public event Action OnCorrectDelivery;
    public event Action OnWrongDelivery;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public override void Interact(Player player)
    {
        if (Player.Instance.GetKitchenObject() is Plate plate)
        {
            if (plate.HasKitchenObject())
            {
                if (DeliveryManager.Instance.DeliverPlate(plate.GetAllKitchenObjects()))
                {
                    OnCorrectDelivery?.Invoke();
                }
                else
                {
                    OnWrongDelivery?.Invoke();
                }
                plate.RemoveKitchenObject();
                plate.AddKitchenObject(Instantiate(leftovers.prefab).GetComponent<KitchenObject>());
            }
        }
    }
}
