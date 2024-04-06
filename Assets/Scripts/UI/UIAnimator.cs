using UnityEngine;
using DG.Tweening;
using UnityEditor;
using System.Threading.Tasks;

public class UIAnimator: MonoBehaviour
{
    public static UIAnimator Instance { get; private set; }
    [SerializeField] private float tweenDuration;
    private void Awake()
    {
        Instance = this;
    }
    public async Task CurrentMenuOutro()
    {
        RectTransform menu = CurrentUIState.Instance.CurrentActiveMenu.GetComponent<RectTransform>();
        await menu.DOAnchorPosY(-(menu.rect.height + Screen.height) / 2, tweenDuration).AsyncWaitForCompletion();
    }
    public void MenuIntro(RectTransform menu)
    {
        menu.DOAnchorPosY(0, tweenDuration);
    }
}
