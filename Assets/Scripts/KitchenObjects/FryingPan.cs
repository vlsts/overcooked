using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FryingPan : KitchenObject, IKitchenObjectParent
{
    [SerializeField] private Transform holdPoint;
    [SerializeField] private KitchenObjectSO meatKitchenObjectSO;
    [SerializeField] private MeatTransformationSO meatTransformationSO;

    public event Action<bool> OnFrying;
    public event Action<bool> OnBurning;

    private KitchenObject meatKitchenObject;
    private StoveCounter stoveCounter;
    private Coroutine cookingCoroutine;

    void Start()
    {
        stoveCounter = GetComponentInParent<StoveCounter>();
        stoveCounter.OnStoveStateChanged += StoveCounter_OnStoveStateChanged; ;
    }

    void Update()
    {
    }

    private void StoveCounter_OnStoveStateChanged(bool isOn)
    {
        if (isOn)
        {
            if (meatKitchenObject != null)
            {
                StartCooking();
            }
        }
        else
        {
            OnBurning?.Invoke(false);
            StopCooking();
        }
    }

    private void StartCooking()
    {
        if (cookingCoroutine == null)
        {
            cookingCoroutine = StartCoroutine(CookingProcess());
        }
    }

    private IEnumerator CookingProcess()
    {
        if (meatKitchenObject?.GetKitchenObjectSO().name == meatTransformationSO.rawMeat.name)
        {
            OnFrying?.Invoke(true);
            yield return new WaitForSeconds(3f);
            if (meatKitchenObject.GetKitchenObjectSO() == meatTransformationSO.rawMeat)
            {
                ChangeMeatState(meatTransformationSO.cookedMeat);
            }
        }
        if (meatKitchenObject?.GetKitchenObjectSO().name == meatTransformationSO.cookedMeat.name)
        {
            OnFrying?.Invoke(true);
            yield return new WaitForSeconds(5f);
            if (meatKitchenObject.GetKitchenObjectSO() == meatTransformationSO.cookedMeat)
            {
                ChangeMeatState(meatTransformationSO.burnedMeat);
                OnBurning?.Invoke(true);
            }
        }
        StopCooking();
    }

    private void ChangeMeatState(KitchenObjectSO newMeatSO)
    {
        if (meatKitchenObject != null)
        {
            Destroy(meatKitchenObject.gameObject);

            KitchenObject newMeat = Instantiate(newMeatSO.prefab).GetComponent<KitchenObject>();
            newMeat.SetKitchenObjectParent(this);
            meatKitchenObject = newMeat;
        }
    }

    private void StopCooking()
    {
        if (cookingCoroutine != null)
        {
            OnFrying?.Invoke(false);
            StopCoroutine(cookingCoroutine);
            cookingCoroutine = null;
        }
    }

    public Transform GetHoldPoint()
    {
        return holdPoint;
    }

    public KitchenObject GetKitchenObject()
    {
        return meatKitchenObject;
    }

    public bool HasKitchenObject()
    {
        return meatKitchenObject != null;
    }

    public void RemoveKitchenObject()
    {
        if (meatKitchenObject != null)
        {
            meatKitchenObject = null;
        }
    }

    public bool SetKitchenObject(KitchenObject kitchenObject)
    {
        if (kitchenObject.GetKitchenObjectSO().name == meatKitchenObjectSO.name)
        {
            meatKitchenObject = kitchenObject;
            if ((bool)(stoveCounter?.IsOn()))
            {
                StartCooking();
            }
            return true;
        }
        return false;
    }

    public override void SetKitchenObjectParent(IKitchenObjectParent newKitchenObjectParent)
    {
        base.SetKitchenObjectParent(newKitchenObjectParent);
        if (newKitchenObjectParent is StoveCounter newStove && newStove.IsOn())
        {
            StartCooking();
        }
        else
        {
            StopCooking();
            OnBurning?.Invoke(false);
        }
    }
}
