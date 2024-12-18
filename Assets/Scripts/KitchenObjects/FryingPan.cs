using System;
using UnityEngine;

public class FryingPan : KitchenObject, IKitchenObjectParent, IProgressable
{
    [SerializeField] private Transform holdPoint;
    [SerializeField] private KitchenObjectSO meatKitchenObjectSO;
    [SerializeField] private MeatTransformationSO meatTransformationSO;

    public event EventHandler<IProgressable.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnMeatStateChangedEventArgs> OnMeatStateChanged;
    public class OnMeatStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burning,
        InFlames,
        Burned
    }

    private KitchenObject meatKitchenObject;
    private StoveCounter stoveCounter;
    private State currentState;

    private const float fryingTime = 3f;
    private const float burningTime = 5f;

    private float elapsedTime = 0f;

    void Start()
    {
        stoveCounter = GetComponentInParent<StoveCounter>();
        base.SetKitchenObjectParent(stoveCounter);
        stoveCounter.OnStoveStateChanged += StoveCounter_OnStoveStateChanged;
    }

    void Update()
    {
        if (HasKitchenObject() && stoveCounter?.IsOn() == true)
        {
            switch(currentState)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    elapsedTime += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs
                    {
                        currentProgress = elapsedTime / fryingTime
                    });
                    
                    if (elapsedTime >= fryingTime)
                    {
                        elapsedTime = 0f;
                        currentState = State.Fried;
                        ChangeMeatState(meatTransformationSO.cookedMeat);

                        OnMeatStateChanged?.Invoke(this, new OnMeatStateChangedEventArgs { state = currentState });
                    }
                    break;
                case State.Fried:
                    currentState = State.Burning;
                    OnMeatStateChanged?.Invoke(this, new OnMeatStateChangedEventArgs { state = currentState });
                    break;
                case State.Burning:
                    elapsedTime += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs
                    {
                        currentProgress = elapsedTime / burningTime
                    });

                    if (elapsedTime >= burningTime)
                    {
                        elapsedTime = 0f;
                        currentState = State.InFlames;
                        ChangeMeatState(meatTransformationSO.burnedMeat);

                        OnMeatStateChanged?.Invoke(this, new OnMeatStateChangedEventArgs { state = currentState });
                    }
                    break;
                case State.InFlames:
                    break;
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
            StopCooking();
        }
    }

    private void StartCooking()
    {
        elapsedTime = 0.0f;
        switch (currentState)
        {
            case State.Idle:
                currentState = State.Frying;
                break;
            case State.Fried:
                currentState = State.Burning;
                break;
            case State.Burned:
                currentState = State.InFlames;
                break;
            default:
                break;
        }
        OnMeatStateChanged?.Invoke(this, new OnMeatStateChangedEventArgs { state = currentState });
    }

    private void StopCooking()
    {
        elapsedTime = 0.0f;
        switch (currentState) 
        {
            case State.Frying:
                currentState = State.Idle;
                break;
            case State.Burning:
                currentState = State.Fried;
                break;
            case State.InFlames:
                currentState = State.Burned;
                break;
            default:
                break;
        }

        OnMeatStateChanged?.Invoke(this, new OnMeatStateChangedEventArgs { state = currentState });
        OnProgressChanged?.Invoke(this, new IProgressable.OnProgressChangedEventArgs { currentProgress = 0 });
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
            currentState = State.Idle;
            OnMeatStateChanged?.Invoke(this, new OnMeatStateChangedEventArgs { state = currentState});
        }
    }

    public bool SetKitchenObject(KitchenObject kitchenObject)
    {
        if (kitchenObject.GetKitchenObjectSO() == meatKitchenObjectSO && !meatKitchenObject)
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
        if (stoveCounter != null)
        {
            stoveCounter.OnStoveStateChanged -= StoveCounter_OnStoveStateChanged;
        }
        base.SetKitchenObjectParent(newKitchenObjectParent);

        if (newKitchenObjectParent is StoveCounter newStove)
        {
            stoveCounter = newStove;
            stoveCounter.OnStoveStateChanged += StoveCounter_OnStoveStateChanged;
            if (stoveCounter.IsOn() && meatKitchenObject != null)
            {
                StartCooking();
            }
        }
        else
        {
            stoveCounter = null;
            StopCooking();
        }
    }

    public State GetCurrentState()
    {
        return currentState;
    }
}