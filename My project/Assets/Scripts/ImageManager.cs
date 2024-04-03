using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ImageManager : MonoBehaviour
{
    public static ImageManager Instance;

    string _basePath;

    void Start()
    {
        if (Instance != null)
        {
            GameObject.Destroy(this);
            return;
        }
        Instance = this;

        _basePath = Application.persistentDataPath + "/Images/";
        if (!Directory.Exists(_basePath))
        {
            Directory.CreateDirectory(_basePath);
        }
    }

    bool ImageExists(string name)
    {
        return File.Exists(_basePath + name);
    }

    public void SaveImage(string name, byte[] bytes)
    {
        File.WriteAllBytes(_basePath + name, bytes);
    }

    public byte[] LoadImage(string name)
    {
        if (ImageExists(name))
        {
            return File.ReadAllBytes(_basePath + name);
        }
        else
        {
            return new byte[0];
        }
        
    }

    public Sprite BytesToSprite(byte[] bytes)
    {
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);

        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));



        return sprite;
    }
}
