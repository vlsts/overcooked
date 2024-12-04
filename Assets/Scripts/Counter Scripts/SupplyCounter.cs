using System;
using UnityEngine;

public class SupplyCounter : BaseCounter
{
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
        OnSupplyCounterOpened?.Invoke();
    }
}
