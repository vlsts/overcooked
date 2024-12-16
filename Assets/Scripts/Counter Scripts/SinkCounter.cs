using System;
using UnityEngine;
using System.Collections;

public class SinkCounter : BaseCounter
{
    public event Action<bool> OnTapToggle; 
    private bool isWashing = false;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject() && Player.Instance.GetKitchenObject() is Plate plate)
        {
            if (plate.IsDirty())
            {
                plate.SetKitchenObjectParent(this);
                SetKitchenObject(plate);
            }
        }
        else if (GetKitchenObject() is Plate plateOnSink)
        {
            plateOnSink.SetKitchenObjectParent(Player.Instance);
            Player.Instance.SetKitchenObject(plateOnSink);
        }
    }

    public override void InteractSecondary(Player player)
    {
        if (!HasKitchenObject())
            return;
        isWashing = !isWashing;
        if (isWashing)
        {
            StartCoroutine(WashingPlate());
        }
        OnTapToggle?.Invoke(isWashing);
    }

    private IEnumerator WashingPlate()
    {
        yield return new WaitForSeconds(3f);
        OnTapToggle?.Invoke(false);
        (GetKitchenObject() as Plate)?.Clean();
    }

    public bool IsWashing()
    {
        return isWashing;
    }
}
