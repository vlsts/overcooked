using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private List<RecipeSO> availabileRecipesSO;

    private List<RecipeSO> orderedRecipesSO;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        orderedRecipesSO = new List<RecipeSO>();
        StartCoroutine(SpawnRecipeCoroutine());
    }

    private IEnumerator SpawnRecipeCoroutine()
    {
        while (true)
        {
            SpawnNewRecipe();
            yield return new WaitForSeconds(10f);
        }
    }

    private void SpawnNewRecipe()
    {
        if (availabileRecipesSO.Count > 0)
        {
            int randomIndex = Random.Range(0, availabileRecipesSO.Count);

            orderedRecipesSO.Add(availabileRecipesSO[randomIndex]);

            Debug.Log("New recipe added: " + orderedRecipesSO[orderedRecipesSO.Count-1].name);
        }
        else
        {
            Debug.LogWarning("No available recipes to spawn.");
        }
    }

    public void DeliverPlate(List<KitchenObject> foodItemsOnPlate)
    {
        foreach (RecipeSO recipe in orderedRecipesSO)
        {
            if (IsRecipeMatched(recipe, foodItemsOnPlate))
            {
                Debug.Log("Recipe matched: " + recipe.name);
                orderedRecipesSO.Remove(recipe);
                return;
            }
        }
        Debug.Log("No recipe matched.");
    }

    private bool IsRecipeMatched(RecipeSO recipe, List<KitchenObject> foodItemsOnPlate)
    {
        foreach (KitchenObjectSO recipeItem in recipe.kitchenObjectSOs)
        {
            bool isMatched = false;
            foreach (KitchenObject foodItem in foodItemsOnPlate)
            {
                if (foodItem.GetKitchenObjectSO().name == recipeItem.name)
                {
                    isMatched = true;
                    break;
                }
            }
            if (!isMatched)
            {
                return false;
            }
        }
        return true;
    }
}
