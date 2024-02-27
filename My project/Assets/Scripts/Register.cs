using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Register : MonoBehaviour
{
    public TMP_InputField userNameInput;
    public TMP_InputField passWordInput;
    public TMP_InputField passWordConfirm;
    public UnityEngine.UI.Button logInButton;

    // Start is called before the first frame update
    void Start()
    {
        logInButton.onClick.AddListener(() =>
        {
            if (passWordInput.text == passWordConfirm.text)
            {
                StartCoroutine(Main.Instance.web.RegisterUser(userNameInput.text, passWordInput.text));
                
            } else
            {
                Debug.Log("ERROR: Passwords don't match!");
            }
            
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
