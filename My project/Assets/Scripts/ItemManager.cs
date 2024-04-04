using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;
using TMPro;

public class ItemManager : MonoBehaviour
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
            string id = jsonArray[i].AsObject["ID"];
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
            GameObject itemGo = Instantiate(Resources.Load("Prefabs/Item") as GameObject);
            Item item = itemGo.AddComponent<Item>();
            item.ID = id;
            item.ItemID = itemId;

            itemGo.transform.SetParent(this.transform);
            itemGo.transform.localScale = Vector3.one;
            itemGo.transform.localPosition = Vector3.zero;

            //Fill Information
            itemGo.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = itemInfoJson["name"];
            itemGo.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = itemInfoJson["price"];
            itemGo.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = itemInfoJson["description"];


            byte[] bytes = ImageManager.Instance.LoadImage(itemId + ".png");
            if (bytes.Length == 0)
            {
                Action<byte[]> getItemIconCallback = (downloadedBytes) =>
                {
                    //convert bytes into sprite
                    Sprite sprite = ImageManager.Instance.BytesToSprite(downloadedBytes);
                    itemGo.transform.Find("Image").GetComponent<Image>().sprite = sprite;
                    ImageManager.Instance.SaveImage(itemId + ".png", downloadedBytes);

                };

                StartCoroutine(Main.Instance.web.GetItemIcon(itemId + ".png", getItemIconCallback));
            } 
            else
            {
                Debug.Log("Loaded Bytes Length: " + bytes.Length);
                Sprite sprite = ImageManager.Instance.BytesToSprite(bytes);
                itemGo.transform.Find("Image").GetComponent<Image>().sprite = sprite;
            }


            //Set the Sell Button
            itemGo.transform.Find("SellButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                string idInInventory = id;
                string iId = itemId;
                string userId = Main.Instance.userInfo.userID;

                StartCoroutine(Main.Instance.web.SellItem(idInInventory, iId, userId));
            });
            
            //Continue to the next item
        }
    }
}
