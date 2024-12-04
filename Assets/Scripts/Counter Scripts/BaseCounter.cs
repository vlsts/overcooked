using UnityEngine;

public class BaseCounter : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public virtual void Interact(Player player)
    {
        Debug.Log("BaseCounter.Interact()");
    }
}
