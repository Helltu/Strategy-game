using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CanvasUI : MonoBehaviour
{
    public static CanvasUI Instance { get; private set; }

    [SerializeField] private GameObject canvas;

    public void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        LobbyManager.Instance.OnGameStarted += OnGameStarted_Event;
    }

    private void OnDisable()
    {
        LobbyManager.Instance.OnGameStarted -= OnGameStarted_Event;
    }

    private void OnGameStarted_Event(object sender, System.EventArgs e)
    {
        HideUI();
    }

    private void HideUI()
    {
        canvas.SetActive(false);
    }
}
