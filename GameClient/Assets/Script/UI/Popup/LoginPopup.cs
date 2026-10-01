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
        var testPacket = new MakeRequestLoginPacket(userNameInput.text, passwordInput.text).UserLoginPacketData;
        await NetworkManager.I.SendWebRequest<UserLoginPacketData, UserLoginPacketResultData>(testPacket.ROUTE, testPacket, HandleLoginResult);
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
