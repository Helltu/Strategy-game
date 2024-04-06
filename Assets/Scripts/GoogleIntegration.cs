using System.Threading.Tasks;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using IngameDebugConsole;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class GoogleIntegration : MonoBehaviour
{
    public string Token;
    public string Error;

    void Awake()
    {
        //Initialize PlayGamesPlatform
        PlayGamesPlatform.Activate();
        LoginGooglePlayGames();
    }

    public void LoginGooglePlayGames()
    {
        PlayGamesPlatform.Instance.Authenticate((success) =>
        {
            if (success == SignInStatus.Success)
            {
                Debug.Log("Login with Google Play games successful: " + PlayGamesPlatform.Instance.GetUserDisplayName());

                PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                {
                    Debug.Log("Authorization code: " + code);
                    Token = code;
                    // This token serves as an example to be used for SignInWithGooglePlayGames
                });
            }
            else
            {
                Error = "Failed to retrieve Google play games authorization code";
                Debug.Log("Login Unsuccessful");
                PopUpSystem popup = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PopUpSystem>();
                popup.popUp("Ошибка логина", "Невозможно подключиться к Гугл Плей Геймс");
                //add app quit
            }
        });
    }
    /*
    public string GooglePlayToken;
    public string GooglePlayError;

    private async void Start()
    {
        await Authenticate();
    }

    public async Task Authenticate()
    {
        PlayGamesPlatform.Activate();
        await UnityServices.InitializeAsync();

        PlayGamesPlatform.Instance.Authenticate((success) =>
        {
            if (success == SignInStatus.Success)
            {
                Debug.Log("Login with Google was successful.");
                PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                {
                    Debug.Log($"Auth code is {code}");
                    GooglePlayToken = code;
                });
            }
            else
            {
                GooglePlayError = "Failed to retrieve GPG auth code";
                Debug.LogError("Login Unsuccessful");
            }
        });

        await AuthenticateWithUnity();
    }

    private async Task AuthenticateWithUnity()
    {
        try
        {
            await AuthenticationService.Instance.SignInWithGoogleAsync(GooglePlayToken);
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
            throw;
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
            throw;
        }
    }*/
}