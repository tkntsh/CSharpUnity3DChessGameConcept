using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameScript : MonoBehaviour
{
    //gameobjects
    public GameObject player1NameInput;
    public GameObject player2NameInput;
    public GameObject errorText;
    public GameObject player1Text;
    public GameObject player2Text;

    public Button loginButton;
    //player 1 and 2 info
    public string player1Name;
    public string player2Name;

    // Start is called before the first frame update
    void Start()
    {
        //adding a listener to the button
        loginButton.onClick.AddListener(saveInfo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //method to check if players have entered info in both input fields
    public void saveInfo()
    {
        player1Name = player1NameInput.GetComponent<Text>().text;
        player2Name = player2NameInput.GetComponent<Text>().text;

        if(string.IsNullOrEmpty(player1Name) || string.IsNullOrEmpty(player2Name))
        {
            //error to display to users
            Debug.LogError("Error: Both player names have to be entered.");
            errorText.SetActive(true);
            return;
        }
        //saving users info and switching scenes
        errorText.SetActive(false);
        PlayerPrefs.SetString("Player 1", player1Name);
        PlayerPrefs.SetString("Player 2", player2Name);
        SceneManager.LoadScene("checkersScene");
        //adding names saved to text
        player1Text.GetComponent<Text>().text = player1Name;
        player2Text.GetComponent<Text>().text = player2Name;
        player1Text.SetActive(true);
        player2Text.SetActive(true);
    }
}
