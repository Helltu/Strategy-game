using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class JoinLobbyByCodeUI : MonoBehaviour
{
    public static JoinLobbyByCodeUI Instance { get; private set; }

    [SerializeField] private GameObject joinLobbyByCodeMenu;
    [SerializeField] private TMP_InputField lobbyCodeInputField;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button confirmButton;

    private void Awake()
    {
        Instance = this;

        RectTransform menu = joinLobbyByCodeMenu.GetComponent<RectTransform>();
        menu.DOAnchorPosY(-(menu.rect.height + Screen.height) / 2, 0);

        closeButton.onClick.AddListener(delegate
        {
            JoinLobbyMenuUI.Instance.Open();
        });

        confirmButton.onClick.AddListener(delegate
        {
            if (!lobbyCodeInputField.text.IsNullOrEmpty())
            {
                LobbyManager.Instance.JoinLobbyByCode(lobbyCodeInputField.text);

            }
            else
            {
                PopUpSystem popUp = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PopUpSystem>();
                popUp.popUp("Ошибка", "Должен быть заполнен код лобби");
            }
        });
    }

    public async void Open()
    {
        await UIAnimator.Instance.CurrentMenuOutro();
        lobbyCodeInputField.text = "";
        CurrentUIState.Instance.CurrentActiveMenu.SetActive(false);
        joinLobbyByCodeMenu.SetActive(true);
        CurrentUIState.Instance.CurrentActiveMenu = joinLobbyByCodeMenu;
        UIAnimator.Instance.MenuIntro(joinLobbyByCodeMenu.GetComponent<RectTransform>());
    }
}
