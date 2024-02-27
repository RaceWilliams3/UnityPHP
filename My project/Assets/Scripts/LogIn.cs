using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogIn : MonoBehaviour
{
    public TMP_InputField userNameInput;
    public TMP_InputField passWordInput;
    public UnityEngine.UI.Button logInButton;
    public UnityEngine.UI.Button registerButton;

    public GameObject registerScreen;

    // Start is called before the first frame update
    void Start()
    {
        logInButton.onClick.AddListener(() =>
        {
            StartCoroutine(Main.Instance.web.Login(userNameInput.text,passWordInput.text));
        });

        registerButton.onClick.AddListener(() =>
        {
            registerScreen.SetActive(true);
            this.gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
