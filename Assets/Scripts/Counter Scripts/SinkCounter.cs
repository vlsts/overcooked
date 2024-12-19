using System;
using UnityEngine;
using System.Collections;

public class SinkCounter : BaseCounter, IProgressable
{
    public event Action<bool> OnTapToggle;
    public event EventHandler<IProgressable.OnProgressChangedEventArgs> OnProgressChanged;

    private bool isWashing = false;
    private float currentTime = 0f;
    private const float totalWashingTime = 4.5f;

    private void Update()
    {
        if (isWashing)
        {
            currentTime += Time.deltaTime;
            OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs
            {
                currentProgress = (float)currentTime / totalWashingTime
            });

            if (currentTime >= totalWashingTime)
            {
                FinishWashing();
            }
        }
        else
        {
            currentTime = 0.0f;
        }
    }

    private void FinishWashing()
    {
        isWashing = false;
        currentTime = 0f;
        OnTapToggle?.Invoke(isWashing);
        (GetKitchenObject() as Plate)?.Clean();
    }

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
            currentTime = 0f;
        }
        else
        {
            OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs
            {
                currentProgress = 0f
            });
        }
        OnTapToggle?.Invoke(isWashing);
    }

    public bool IsWashing()
    {
        return isWashing;
    }
}
