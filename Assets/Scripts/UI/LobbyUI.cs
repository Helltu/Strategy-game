using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    private Lobby lobby;
    public Lobby Lobby
    {
        get
        {
            return lobby;
        }
        set
        {
            lobby = value;
        }
    }
    [SerializeField]private Button joinButton;

    private void Awake()
    {
        joinButton.onClick.AddListener(delegate
        {
            LobbyManager.Instance.JoinLobby(lobby);
        });
    }
}
