using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpSystem : MonoBehaviour
{
    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpHeader;
    public TMP_Text popUpText;

    public void popUp(string header, string text)
    {
        popUpBox.SetActive(true);
        popUpHeader.text = header;
        popUpText.text = text;
        animator.SetTrigger("pop");
    }
}
