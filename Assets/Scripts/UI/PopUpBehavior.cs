using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpBehavior : MonoBehaviour
{
    public GameObject popUpBox;
    public void close()
    {
        popUpBox.SetActive(false);
    }
}
