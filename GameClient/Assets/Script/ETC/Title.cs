using MINLib.Script.Core.Resource;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Title : MonoBehaviour
{
    public async void OnTouchLogin()
    {
        //new OpenPopup<LoginPopup>("LoginPopup", Vector3.zero);
        await NetworkManager.I.SendLoginRequest("Admin", "1234", HandleLoginResult);
    }

    private void HandleLoginResult(UserLoginPacketResultData data)
    {
        if (data.Result != EPacketResult.Ok)
        {
            Debug.LogError($"Login Failed : {data.Result}");
            return;
        }

        Debug.Log($"Login Sucess: {data.UserData.UserId}");
        GameManager.I.User = data.UserData;
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void OnTouchExit()
    {
        Application.Quit();
    }
}