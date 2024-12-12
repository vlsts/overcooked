using System;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    public event Action<bool> OnStoveStateChanged;

    private bool isOn = false;

    private void Awake()
    {
        SetKitchenObject(GetComponentInChildren<FryingPan>());
        GetKitchenObject().SetKitchenObjectParent(this);
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject() && Player.Instance.GetKitchenObject() is FryingPan)
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
            if (GetKitchenObject() is FryingPan fryingPan)
            {
                fryingPan.SetKitchenObject(Player.Instance.GetKitchenObject());
                fryingPan.GetKitchenObject().SetKitchenObjectParent(fryingPan);
            }
        }
    }

    public override void InteractSecondary(Player player)
    {
        isOn = !isOn;
        OnStoveStateChanged?.Invoke(isOn);
    }
}
