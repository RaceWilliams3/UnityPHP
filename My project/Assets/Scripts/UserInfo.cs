using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    public string userID { get; private set; }
    private string userName;
    private string userPassword;
    private string userLevel;
    private string userCoins;

    public void SetCredentials(string userName, string userPassword)
    {
        this.userName = userName;
        this.userPassword = userPassword;
    }

    public void SetID(string id)
    {
        userID = id;
    }
}
