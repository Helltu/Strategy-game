using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartGameMenuUI : MonoBehaviour
{
    public static StartGameMenuUI Instance { get; private set; }

    [SerializeField] private GameObject startGameMenu;
    [SerializeField] private Button joinLobbyButton;
    [SerializeField] private Button createLobbyButton;
    [SerializeField] private Button backButton;

    public void Awake()
    {
        Instance = this;

        RectTransform menu = startGameMenu.GetComponent<RectTransform>();
        menu.DOAnchorPosY(-(menu.rect.height + Screen.height) / 2, 0);

        joinLobbyButton.onClick.AddListener(delegate
        {
            JoinLobbyMenuUI.Instance.Open();
        });

        createLobbyButton.onClick.AddListener(delegate
        {
            CreateLobbyMenuUI.Instance.Open();
        });

        backButton.onClick.AddListener(delegate
        {
            MainMenuUI.Instance.Open();
        });
    }

    public async void Open()
    {
        await UIAnimator.Instance.CurrentMenuOutro();
        CurrentUIState.Instance.CurrentActiveMenu.SetActive(false);
        startGameMenu.SetActive(true);
        CurrentUIState.Instance.CurrentActiveMenu = startGameMenu;
        UIAnimator.Instance.MenuIntro(startGameMenu.GetComponent<RectTransform>());
    }
}
