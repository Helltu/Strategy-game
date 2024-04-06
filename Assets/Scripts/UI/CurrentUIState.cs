using UnityEngine;

public class CurrentUIState : MonoBehaviour
{
    public static CurrentUIState Instance { get; private set; }

    [SerializeField] private GameObject currentActiveCanvas;
    public GameObject CurrentActiveMenu
    {
        get 
        { 
            return currentActiveCanvas; 
        }
        set 
        {
            currentActiveCanvas = value; 
        }
    }

    private void Awake()
    {
        Instance = this;
    }
}
