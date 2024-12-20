using System;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IProgressable
{
    [SerializeField] private List<CuttableFoodSO> cuttableFoods;

    public static event EventHandler OnCut;
    public event Action OnObjectCut;
    public event EventHandler<IProgressable.OnProgressChangedEventArgs> OnProgressChanged;

    private int currentCuts;
    private CuttableFoodSO currentCuttableObject;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject() && Player.Instance.HasKitchenObject())
        {
            if (cuttableFoods.Find(cuttableFood => cuttableFood.originalFood.objectName == Player.Instance.GetKitchenObject().GetKitchenObjectSO().objectName) is CuttableFoodSO matchingCuttableFood) 
            {
                currentCuttableObject = matchingCuttableFood;
                OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs { currentProgress = 0 });
                SetKitchenObject(Player.Instance.GetKitchenObject());
                GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else if (!Player.Instance.HasKitchenObject() && HasKitchenObject())
        {
            if (Player.Instance.SetKitchenObject(GetKitchenObject())) 
            {
                Player.Instance.GetKitchenObject().SetKitchenObjectParent(Player.Instance);
                currentCuts = 0;
                OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs { currentProgress = 0 });
            }
        }
    }

    public override void InteractSecondary(Player player)
    {
        if (!HasKitchenObject())
            return;
        if (GetKitchenObject().GetKitchenObjectSO().objectName == currentCuttableObject.originalFood.objectName)
        {
            currentCuts++;
            OnObjectCut?.Invoke();
            OnCut?.Invoke(this, EventArgs.Empty);
            OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs
            {
                currentProgress = (float)currentCuts / currentCuttableObject.necessaryCuts
            });
            if (currentCuts >= currentCuttableObject.necessaryCuts)
            {
                GetKitchenObject().DestroySelf();
                KitchenObject.TrySpawnKitchenObject(currentCuttableObject.cutFood, this);
                currentCuts = 0;
            }
        }
    }

    public static void ResetStatcEvents()
    {
        OnCut = null;
    }
}
