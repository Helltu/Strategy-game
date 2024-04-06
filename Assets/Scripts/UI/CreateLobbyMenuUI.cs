using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;
using DG.Tweening;

public class CreateLobbyMenuUI : MonoBehaviour
{
    public static CreateLobbyMenuUI Instance { get; private set; }

    [SerializeField] private GameObject createLobbyMenu;
    public GameObject CreateLobbyMenuUICanvas
    {
        get
        {
            return createLobbyMenu;
        }
        private set
        {
            createLobbyMenu = value;
        }
    }
    [SerializeField] private TMP_InputField lobbyNameInputField;
    [SerializeField] private Toggle isPrivateToggle;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button confirmButton;

    public void Awake()
    {
        Instance = this;

        RectTransform menu = createLobbyMenu.GetComponent<RectTransform>();
        menu.DOAnchorPosY(-(menu.rect.height + Screen.height) / 2, 0);

        closeButton.onClick.AddListener(delegate
        {
            StartGameMenuUI.Instance.Open();
        });
        confirmButton.onClick.AddListener(delegate
        {
            if (lobbyNameInputField.text.IsNullOrEmpty())
            {
                PopUpSystem popup = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PopUpSystem>();
                popup.popUp("Ошибка", "Должно быть заполнено имя лобби");
            }
            else
            {
                LobbyManager.Instance.CreateLobby(lobbyNameInputField.text.Trim(), isPrivateToggle.isOn);
            }
        });
    }
    public async void Open()
    {
        await UIAnimator.Instance.CurrentMenuOutro();
        lobbyNameInputField.text = "";
        isPrivateToggle.isOn = false;
        CurrentUIState.Instance.CurrentActiveMenu.SetActive(false);
        createLobbyMenu.SetActive(true);
        CurrentUIState.Instance.CurrentActiveMenu = createLobbyMenu;
        UIAnimator.Instance.MenuIntro(createLobbyMenu.GetComponent<RectTransform>());
    }
}
