using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using Unity.VisualScripting;
using WebSocketSharp;

public class SearchLobbyMenuUI : MonoBehaviour
{
    public static SearchLobbyMenuUI Instance { get; private set; }

    [SerializeField] private GameObject searchLobbyMenu;
    [SerializeField] private TMP_InputField lobbySearchInputField;
    [SerializeField] private VerticalLayoutGroup lobbyList;
    [SerializeField] private GameObject lobbyItemPrefab;
    [SerializeField] private GameObject noLobbiesLabel; 
    [SerializeField] private Button closeButton;

    private List<Lobby> lobbies;

    private void Awake()
    {
        Instance = this;

        RectTransform menu = searchLobbyMenu.GetComponent<RectTransform>();
        menu.DOAnchorPosY(-(menu.rect.height + Screen.height) / 2, 0);

        closeButton.onClick.AddListener(delegate
        {
            JoinLobbyMenuUI.Instance.Open();
        });

        lobbySearchInputField.onSubmit.AddListener(delegate
        {
            LobbyManager.Instance.RefreshLobbyList();
        });
    }

    private void OnEnable()
    {
        LobbyManager.Instance.OnLobbyListChanged += OnLobbyListChanged_Event;
    }

    private void OnDisable()
    {
        LobbyManager.Instance.OnLobbyListChanged -= OnLobbyListChanged_Event;
    }

    private void OnLobbyListChanged_Event(object sender, LobbyManager.OnLobbyListChangedEventArgs e)
    {
        UpdateLobbyList(e.lobbyList);
    }

    private void UpdateLobbyList(List<Lobby> lobbies)
    {
        this.lobbies = lobbies;
        for (var i = lobbyList.transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(lobbyList.transform.GetChild(i).gameObject);
        }
        if (lobbies != null && lobbies.Count > 0)
        {
            noLobbiesLabel.SetActive(false);
            foreach (Lobby lobby in lobbies)
            {
                if (lobby.Name.Contains(lobbySearchInputField.text))
                {
                    GameObject newItem = Instantiate(lobbyItemPrefab);
                    LobbyUI lobbyUI = newItem.GetComponent<LobbyUI>();
                    lobbyUI.Lobby = lobby;
                    newItem.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = lobby.Name;
                    newItem.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = (4 - lobby.AvailableSlots) + "/4";
                    newItem.transform.SetParent(lobbyList.transform, false);
                }
            };
        }
        else
        {
            noLobbiesLabel.SetActive(true);
        }
    }

    public async void Open()
    {
        LobbyManager.Instance.RefreshLobbyList();
        await UIAnimator.Instance.CurrentMenuOutro();
        lobbySearchInputField.text = "";
        CurrentUIState.Instance.CurrentActiveMenu.SetActive(false);
        searchLobbyMenu.SetActive(true);
        CurrentUIState.Instance.CurrentActiveMenu = searchLobbyMenu;
        UIAnimator.Instance.MenuIntro(searchLobbyMenu.GetComponent<RectTransform>());
    }
}
