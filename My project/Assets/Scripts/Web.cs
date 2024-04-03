using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{

    string dbURL = "https://mysim421.000webhostapp.com/";
    void Start()
    {
        //StartCoroutine(GetDate());
        //StartCoroutine(RegisterUser("testuser3", "1234"));
    }
    
    
    public void ShowUserItems()
    {
       // StartCoroutine(GetItemsIDs(Main.Instance.userInfo.userID));
    }

    public IEnumerator GetDate()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(dbURL + "GetDate.php"))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                byte[] results = www.downloadHandler.data;
            }
        }
    }

    public IEnumerator GetUsers()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(dbURL + "GetUsers.php"))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                byte[] results = www.downloadHandler.data;
            }
        }
    }

    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post(dbURL + "Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                Main.Instance.userInfo.SetCredentials(username,password);
                Main.Instance.userInfo.SetID(www.downloadHandler.text);

                if (www.downloadHandler.text.Contains("Username does not exist") || www.downloadHandler.text.Contains("Wrong Credentials"))
                {
                    Debug.Log("Try Again");
                } else
                {
                    Main.Instance.userProfile.SetActive(true);
                    Main.Instance.login.gameObject.SetActive(false);
                }

                //If we logged in correctly
            }

        }
    }

    public IEnumerator RegisterUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post(dbURL + "RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }

        }
    }

    public IEnumerator GetItemsIDs(string userID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);


        using (UnityWebRequest www = UnityWebRequest.Post(dbURL + "GetItemsIDs.php", form))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Download Handler Text: ");
                Debug.Log(www.downloadHandler.text);

                string jsonArray = www.downloadHandler.text;
                //Call callback function to pass results

                callback(jsonArray);
            }
        }
    }

    public IEnumerator GetItem(string itemID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);


        using (UnityWebRequest www = UnityWebRequest.Post(dbURL + "GetItem.php", form))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                string jsonArray = www.downloadHandler.text;
                //Call callback function to pass results

                callback(jsonArray);
            }
        }
    }


    public IEnumerator SellItem(string ID, string itemID, string userID)
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", ID);
        form.AddField("itemID", itemID);
        form.AddField("userID", userID);


        using (UnityWebRequest www = UnityWebRequest.Post(dbURL + "SellItem.php", form))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetItemIcon(string itemID, System.Action<byte[]> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);

        using (UnityWebRequest www = UnityWebRequest.Post(dbURL + "GetItemIcon.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            } else
            {
                byte[] bytes = www.downloadHandler.data;

               
                callback(bytes);
            }
        }
    }
}
