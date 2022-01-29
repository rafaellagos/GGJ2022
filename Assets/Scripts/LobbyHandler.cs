using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LobbyHandler : MonoBehaviour
{
    //UI Elements
    [Header("UI Elements")]
    public InputField createInput;
    public InputField joinInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //creates a random value for the lobby with 6 characters from {a-z,0-9,A-Z}
    public void randomizeLobby()
    {
        string values = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUWVXYZ0123456789";
        string resultString = "";
        for(int i = 0; i<6; i++)
        {
            resultString += values.Substring(Random.Range(0, values.Length), 1);
        }
        createInput.text = resultString;
    }

    public void clearCreateInputValue()
    {
        createInput.text = "";
    }

    public void clearJoinInputValue()
    {
        joinInput.text = "";
    }

    public void joinServer()
    {
        if(createInput.text == "" && joinInput.text == "")
        {
            return;
        }
        else if( createInput.text != "" && joinInput.text == "")
        {
            Debug.Log("Creating server!");
        }
        else if(createInput.text == "" && joinInput.text != "")
        {
            Debug.Log("Joining server!");
        }
    }
}
