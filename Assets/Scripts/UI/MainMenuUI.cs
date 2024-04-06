using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuUI : MonoBehaviour
{
    public static MainMenuUI Instance { get; private set; }

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button exitButton;

    public void Awake()
    {
        Instance = this;
        startGameButton.onClick.AddListener(delegate
        {
           StartGameMenuUI.Instance.Open();
        });
        exitButton.onClick.AddListener(delegate
        {
            Application.Quit();
        });
    }
    public async void Open()
    {
        await UIAnimator.Instance.CurrentMenuOutro();
        CurrentUIState.Instance.CurrentActiveMenu.SetActive(false);
        mainMenu.SetActive(true);
        CurrentUIState.Instance.CurrentActiveMenu = mainMenu;
        UIAnimator.Instance.MenuIntro(mainMenu.GetComponent<RectTransform>());
    }
}
