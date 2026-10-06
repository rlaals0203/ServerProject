using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginPopup : PopupUIBase
{
    [SerializeField] private TMP_InputField userNameInput;
    [SerializeField] private TMP_InputField passwordInput;

    protected override void Initialize()
    {

    }

    public async void OnTouchLogin()
    {
        await NetworkManager.I.SendLoginRequest(userNameInput.text, passwordInput.text, HandleLoginResult);
    }

    private void HandleLoginResult(UserLoginPacketResultData data)
    {
        if(data.Result != EPacketResult.Ok)
        {
            OnLoginFailed(data.Result);
            Debug.LogError($"Login Failed : {data.Result}");
            return;
        }

        Debug.Log(data.UserData);
        GameManager.I.MainUser = data.UserData;
        SucessLogin();
    }

    private void SucessLogin()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    private void OnLoginFailed(EPacketResult result)
    {

    }
}
