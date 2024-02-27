using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;
using TMPro;

public class Items : MonoBehaviour
{
    Action<string> _createItemsCallBack;

    void Start()
    {
        _createItemsCallBack = (jsonArrayString) =>
        {
            StartCoroutine(CreateItemsRoutine(jsonArrayString));
        };

        CreateItems();
    }

    public void CreateItems()
    {
        string userId = Main.Instance.userInfo.userID;
        StartCoroutine(Main.Instance.web.GetItemsIDs(userId, _createItemsCallBack));
    }

    IEnumerator CreateItemsRoutine(string jsonArrayString)
    {
        //Parsing the json array string as an array
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;
        Debug.Log(jsonArray);
        for (int i = 0; i < jsonArray.Count; i++)
        {
            //Create local variables 
            bool isDone = false;
            string itemId = jsonArray[i].AsObject["itemID"];
            JSONObject itemInfoJson = new JSONObject();

            //Create a callback to get the information from the web class
            Action<string> getItemInfoCallback = (itemInfo) =>
            {
                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;

            };

            StartCoroutine(Main.Instance.web.GetItem(itemId, getItemInfoCallback));

            //Wait until the callback is called from WEB 
            yield return new WaitUntil(() => isDone == true);

            //Instantiate GameObject
            GameObject item = Instantiate(Resources.Load("Prefabs/Item") as GameObject);
            item.transform.SetParent(this.transform);
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;

            //Fill Information
            item.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = itemInfoJson["name"];
            item.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = itemInfoJson["price"];
            item.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = itemInfoJson["description"];

            //Continue to the next item
        }
    }
}
