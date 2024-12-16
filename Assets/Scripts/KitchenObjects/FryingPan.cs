using System;
using UnityEngine;

public class FryingPan : KitchenObject, IKitchenObjectParent, IProgressable
{
    [SerializeField] private Transform holdPoint;
    [SerializeField] private KitchenObjectSO meatKitchenObjectSO;
    [SerializeField] private MeatTransformationSO meatTransformationSO;

    public event Action<bool> OnFrying;
    public event Action<bool> OnBurningStarted;
    public event EventHandler<IProgressable.OnProgressChangedEventArgs> OnProgressChanged;

    private KitchenObject meatKitchenObject;
    private StoveCounter stoveCounter;

    private float cookingTime = 3f;
    private float burningTime = 5f;

    private float elapsedTime = 0f;
    private bool isCooking = false;
    private bool isBurning = false;

    void Start()
    {
        stoveCounter = GetComponentInParent<StoveCounter>();
        stoveCounter.OnStoveStateChanged += StoveCounter_OnStoveStateChanged;
    }

    void Update()
    {
        if (isCooking || isBurning)
        {
            elapsedTime += Time.deltaTime;

            if (isCooking)
            {
                OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs
                {
                    currentProgress = elapsedTime / cookingTime
                });

                if (elapsedTime >= cookingTime)
                {
                    if (meatKitchenObject?.GetKitchenObjectSO().name == meatTransformationSO.rawMeat.name)
                    {
                        ChangeMeatState(meatTransformationSO.cookedMeat);
                    }
                    StartBurning();
                }
            }
            else if (isBurning)
            {
                OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs
                {
                    currentProgress = elapsedTime / burningTime
                });

                if (elapsedTime >= burningTime)
                {
                    if (meatKitchenObject?.GetKitchenObjectSO().name == meatTransformationSO.cookedMeat.name)
                    {
                        ChangeMeatState(meatTransformationSO.burnedMeat);
                        OnBurningStarted?.Invoke(true);
                        OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs
                        {
                            currentProgress = 0f
                        });
                        isBurning = false;
                    }
                }
            }
        }
    }

    private void StoveCounter_OnStoveStateChanged(bool isOn)
    {
        if (isOn)
        {
            if (meatKitchenObject != null && stoveCounter != null)
            {
                StartCooking();
            }
        }
        else
        {
            OnBurningStarted?.Invoke(false);
            StopCooking();
        }
    }

    private void StartCooking()
    {
        if (meatKitchenObject?.GetKitchenObjectSO().name == meatTransformationSO.rawMeat.name)
        {
            isCooking = true;
            isBurning = false;
            elapsedTime = 0f;
            OnFrying?.Invoke(true);
        }
        else if (meatKitchenObject?.GetKitchenObjectSO().name == meatTransformationSO.cookedMeat.name)
        {
            StartBurning();
        }
    }

    private void StartBurning()
    {
        isCooking = false;
        isBurning = true;
        elapsedTime = 0f;
        OnFrying?.Invoke(false);  
    }

    private void StopCooking()
    {
        isCooking = false;
        isBurning = false;

        OnFrying?.Invoke(false);
        OnBurningStarted?.Invoke(false);
        OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs
        {
            currentProgress = 0f
        });
        elapsedTime = 0f;
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
        if (kitchenObject.GetKitchenObjectSO().name == meatKitchenObjectSO.name && !meatKitchenObject)
        {
            meatKitchenObject = kitchenObject;
            if (stoveCounter?.IsOn() == true)
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
        if (newKitchenObjectParent is StoveCounter newStove)
        {
            stoveCounter = newStove;
            if (stoveCounter.IsOn())
            {
                StartCooking();
            }
        }
        else
        {
            stoveCounter = null;
            StopCooking();
            OnFrying?.Invoke(false);
            OnBurningStarted?.Invoke(false);
        }
    }

    public bool IsBurning()
    {
        return isBurning;
    }
}
