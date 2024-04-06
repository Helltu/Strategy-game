using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;
using Unity.VisualScripting;

public class LobbyMenuUI : MonoBehaviour
{
    public static LobbyMenuUI Instance { get; private set; }

    [SerializeField] private GameObject lobbyMenu;
    [SerializeField] private VerticalLayoutGroup playersList;
    [SerializeField] private GameObject playerItemPrefab;
    [SerializeField] private GameObject hostItemPrefab;
    [SerializeField] private GameObject lobbyCode;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button startGameButton;
    private void Awake()
    {
        Instance = this;

        RectTransform menu = lobbyMenu.GetComponent<RectTransform>();
        menu.DOAnchorPosY(-(menu.rect.height + Screen.height) / 2, 0);

        closeButton.onClick.AddListener(delegate
        {
            LobbyManager.Instance.LeaveLobby();
            StartGameMenuUI.Instance.Open();
        });
        startGameButton.onClick.AddListener(delegate
        {
            //LobbyService.Instance.GetLobbyAsync(LobbyManager.JoinedLobby.Id)
            LobbyManager.Instance.StartGame();
        });
    }

    private void OnEnable()
    {
        Debug.Log(LobbyManager.Instance.IsUnityNull());
        LobbyManager.Instance.OnJoinedLobby += JoinLobby_Event;
        LobbyManager.Instance.OnJoinedLobbyUpdate += JoinedLobbyUpdate_Event;
        LobbyManager.Instance.OnKickedFromLobby += UpdateLobby_Event;

    }

    private void OnDisable()
    {
        LobbyManager.Instance.OnJoinedLobby -= JoinLobby_Event;
        LobbyManager.Instance.OnJoinedLobbyUpdate -= JoinedLobbyUpdate_Event;
        LobbyManager.Instance.OnKickedFromLobby -= UpdateLobby_Event;
    }

    private void JoinLobby_Event(object sender, LobbyManager.LobbyEventArgs e)
    {
        Open();
    }

    private void JoinedLobbyUpdate_Event(object sender, LobbyManager.LobbyEventArgs e)
    {
        lobbyCode.GetComponent<TMP_Text>().text = LobbyManager.Instance.JoinedLobby.LobbyCode;
        if (LobbyManager.Instance.JoinedLobby.IsPrivate && LobbyManager.Instance.IsLobbyHost())
        {
            lobbyCode.SetActive(true);
        }
        else
        {
            lobbyCode.SetActive(false);
        }
        UpdateLobbyPlayerList();
    }

    private void UpdateLobby_Event(object sender, LobbyManager.LobbyEventArgs e)
    {
        UpdateLobbyPlayerList();
    }

    public void UpdateLobbyPlayerList()
    {
        for (var i = playersList.transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(playersList.transform.GetChild(i).gameObject);
        }
        foreach (var player in LobbyManager.Instance.JoinedLobby.Players)
        {
            GameObject newItem;
            if (LobbyManager.Instance.IsLobbyHost(player))
            {
                newItem = Instantiate(hostItemPrefab);
            }
            else
            {
                newItem = Instantiate(playerItemPrefab);
                PlayerUI playerUI = newItem.GetComponent<PlayerUI>();
                playerUI.Player = player;
            }
            newItem.transform.GetChild (0).gameObject.GetComponent<TMP_Text>().text = player.Data["PlayerName"].Value;
            newItem.transform.SetParent (playersList.transform, false);
        };
    }

    public async void Open()
    {
        await UIAnimator.Instance.CurrentMenuOutro();
        startGameButton.gameObject.SetActive(LobbyManager.Instance.IsLobbyHost());
        for (var i = playersList.transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(playersList.transform.GetChild(i).gameObject);
        }
        lobbyCode.GetComponent<TMP_Text>().text = "";
        lobbyCode.SetActive(false);
        CurrentUIState.Instance.CurrentActiveMenu.SetActive(false);
        lobbyMenu.SetActive(true);
        CurrentUIState.Instance.CurrentActiveMenu = lobbyMenu;
        UIAnimator.Instance.MenuIntro(lobbyMenu.GetComponent<RectTransform>());
    }
}
