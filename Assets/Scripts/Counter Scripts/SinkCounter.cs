using System;
using UnityEngine;

public class SinkCounter : BaseCounter
{
    public event Action OnWashingDish;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact(Player player)
    {
        OnWashingDish?.Invoke();
    }
}
