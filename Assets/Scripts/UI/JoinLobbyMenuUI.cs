using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class JoinLobbyMenuUI : MonoBehaviour
{
    public static JoinLobbyMenuUI Instance { get; private set; }

    [SerializeField] private GameObject joinLobbyMenu;
    [SerializeField] private Button searchLobbyButton;
    [SerializeField] private Button joinLobbyByCodeButton;
    [SerializeField] private Button backButton;

    public void Awake()
    {
        Instance = this;

        RectTransform menu = joinLobbyMenu.GetComponent<RectTransform>();
        menu.DOAnchorPosY(-(menu.rect.height + Screen.height) / 2, 0);

        searchLobbyButton.onClick.AddListener(delegate
        {
            SearchLobbyMenuUI.Instance.Open();
        });

        joinLobbyByCodeButton.onClick.AddListener(delegate
        {
            JoinLobbyByCodeUI.Instance.Open();
        });

        backButton.onClick.AddListener(delegate
        {
            StartGameMenuUI.Instance.Open();
        });
    }

    public async void Open()
    {
        await UIAnimator.Instance.CurrentMenuOutro();
        CurrentUIState.Instance.CurrentActiveMenu.SetActive(false);
        joinLobbyMenu.SetActive(true);
        CurrentUIState.Instance.CurrentActiveMenu = joinLobbyMenu;
        UIAnimator.Instance.MenuIntro(joinLobbyMenu.GetComponent<RectTransform>());
    }
}
