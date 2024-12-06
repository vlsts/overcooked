using System;
using UnityEngine;

public class SinkCounter : BaseCounter
{
    public event Action<bool> OnTapToggle; 
    private bool isWashing = false; 

    public override void Interact(Player player)
    {
        isWashing = !isWashing; 
        OnTapToggle?.Invoke(isWashing); 
    }

    public bool IsWashing()
    {
        return isWashing;
    }
}
