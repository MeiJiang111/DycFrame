using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEditor.VersionControl;

public class LoginManager : GameSystem
{
    public event Action<bool, string> LoginResultEvent;

    public LoginManager()
    {

    }

    public override void Init()
    {
      
    }


    //private void OnExclusiveMessageTimeOut(MessageId resp_)
    //{
      
    //}

    /// <summary>
    /// ∂œœﬂ÷ÿ¡¨
    /// </summary>
    public void ReTryLogin()
    {

    }

    public void LoginIn(string account, string password)
    {
        if (string.IsNullOrEmpty(account) && string.IsNullOrEmpty(password)) 
        {

            return;
        }
        GameManager.Instance.PlayerLoginIn(true);
        GameManager.Instance.GameStart();
    }
}
