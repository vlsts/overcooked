using System;
using UnityEngine;

public class SupplyCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event Action OnSupplyCounterOpened;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact(Player player)
    {
        if (KitchenObject.TrySpawnKitchenObject(kitchenObjectSO, Player.Instance))
            OnSupplyCounterOpened?.Invoke();
    }
}
