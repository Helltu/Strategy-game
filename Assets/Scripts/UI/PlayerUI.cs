using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private Player player;
    public Player Player
    {
        get
        {
            return player;
        }
        set
        {
            player = value;
        }
    }
    [SerializeField]private Button kickButton;

    private void Awake()
    {
        kickButton.gameObject.SetActive(LobbyManager.Instance.IsLobbyHost());
        kickButton.onClick.AddListener(delegate
        {
            LobbyManager.Instance.KickPlayer(player.Id);
        });
    }
}
