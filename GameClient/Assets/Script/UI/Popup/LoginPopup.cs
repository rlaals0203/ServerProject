using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        }

        SucessLogin();
    }

    private void SucessLogin()
    {

    }

    private void OnLoginFailed(EPacketResult result)
    {

    }
}
