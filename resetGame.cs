using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class resetGame : MonoBehaviour
{
    //array to contain game pieces
    public GameObject[] player1Pieces;
    public GameObject[] player2Pieces;
    public Vector3[] player1StartPositions;
    public Vector3[] player2StartPositions;

    void Start()
    {
        //saving inital positions of pieces
        StoreInitialPositions();
    }

    //method to store initial positions of objects
    public void StoreInitialPositions()
    {
        player1StartPositions = new Vector3[player1Pieces.Length];
        player2StartPositions = new Vector3[player2Pieces.Length];

        for (int i = 0; i < player1Pieces.Length; i++)
        {
            player1StartPositions[i] = player1Pieces[i].transform.position;
        }

        for (int i = 0; i < player2Pieces.Length; i++)
        {
            player2StartPositions[i] = player2Pieces[i].transform.position;
        }
    }
    //method to reset the pieces to their original positions after being moved
    public void ResetBoard()
    {
        //reseting player 1 pieces
        for (int i = 0; i < player1Pieces.Length; i++)
        {
            player1Pieces[i].SetActive(true);
            player1Pieces[i].transform.position = player1StartPositions[i];
        }

        //reseting player 2 pieces
        for (int i = 0; i < player2Pieces.Length; i++)
        {
            player2Pieces[i].SetActive(true);
            player2Pieces[i].transform.position = player2StartPositions[i];
        }
    }
    //switching scenes 
    public void switchScene()
    {
        SceneManager.LoadScene("homepageScene");
    }
}
