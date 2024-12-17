using System;
using UnityEngine;
using System.Collections;

public class SinkCounter : BaseCounter
{
    public event Action<bool> OnTapToggle; 
    private bool isWashing = false;

    private Coroutine washingCoroutine = null;

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
            washingCoroutine = StartCoroutine(WashingPlate());
        }
        else
        {
            StopCoroutine(washingCoroutine);
        }
        OnTapToggle?.Invoke(isWashing);
    }

    private IEnumerator WashingPlate()
    {
        yield return new WaitForSeconds(4.5f);
        isWashing = false;
        OnTapToggle?.Invoke(isWashing);
        (GetKitchenObject() as Plate)?.Clean();
    }

    public bool IsWashing()
    {
        return isWashing;
    }
}
