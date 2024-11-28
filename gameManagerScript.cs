using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManagerScript : MonoBehaviour
{
    //gameobjects
    private GameObject selectedPiece;
    private Vector3 originalPosition;
    private pieceScript.Player currentPlayer = pieceScript.Player.Player1;

    //calling handle method to update every frame
    void Update()
    {
        HandlePieceSelection();
    }

    //handle method to select and move pieces
    void HandlePieceSelection()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //checking if object is hit
            if(Physics.Raycast(ray, out hit))
            {
                pieceScript piece = hit.transform.GetComponent<pieceScript>();
                if(piece != null && piece.player == currentPlayer)
                {
                    //updating position
                    selectedPiece = hit.transform.gameObject;
                    originalPosition = selectedPiece.transform.position;
                }
            }
        }
        //moving selected piece
        if(selectedPiece != null && Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Board")))
            {
                selectedPiece.transform.position = hit.point + Vector3.up * 0.5f;
            }
        }
        //releasing selected piece
        if(selectedPiece != null && Input.GetMouseButtonUp(0))
        {
            Vector3 newPosition = selectedPiece.transform.position;
            newPosition.x = Mathf.RoundToInt(newPosition.x);
            newPosition.z = Mathf.RoundToInt(newPosition.z);

            if(IsValidMove(originalPosition, newPosition))
            {
                CapturePiece(originalPosition, newPosition);
                selectedPiece.transform.position = newPosition;
                SwitchPlayer();
            }
            else
            {
                selectedPiece.transform.position = originalPosition;
            }

            selectedPiece = null;
        }
    }
    //checking if the move made by the user is a valid move according to rules
    bool IsValidMove(Vector3 originalPosition, Vector3 newPosition)
    {
        //checking if move is withing bounds
        if(newPosition.x >= 0 && newPosition.x < 8 && newPosition.z >= 0 && newPosition.z < 8)
        {
            Vector3 direction = newPosition - originalPosition;
            if(Mathf.Abs(direction.x) == 2 && Mathf.Abs(direction.z) == 2)
            {
                Vector3 middlePosition = originalPosition + direction / 2;
                RaycastHit hit;
                if(Physics.Raycast(middlePosition + Vector3.up * 0.5f, Vector3.down, out hit))
                {
                    pieceScript piece = hit.transform.GetComponent<pieceScript>();
                    if(piece != null && piece.player != currentPlayer)
                    {
                        return true;
                    }
                }
            }
            //checking if move was simple
            else if(Mathf.Abs(direction.x) == 1 && Mathf.Abs(direction.z) == 1)
            {
                RaycastHit hit;
                if(Physics.Raycast(newPosition + Vector3.up * 0.5f, Vector3.down, out hit))
                {
                    //checking if new position is empty
                    if(hit.transform.GetComponent<pieceScript>() == null)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    //method to check opponent pieces
    void CapturePiece(Vector3 originalPosition, Vector3 newPosition)
    {
        Vector3 direction = newPosition - originalPosition;
        Vector3 middlePosition = originalPosition + direction / 2;
        RaycastHit hit;
        if(Physics.Raycast(middlePosition + Vector3.up * 0.5f, Vector3.down, out hit))
        {
            pieceScript piece = hit.transform.GetComponent<pieceScript>();
            if(piece != null && piece.player != currentPlayer)
            {
                //disabling opponent object
                hit.transform.gameObject.SetActive(false);
            }
        }
    }
    //switching players
    void SwitchPlayer()
    {
        currentPlayer = (currentPlayer == pieceScript.Player.Player1) ? pieceScript.Player.Player2 : pieceScript.Player.Player1;
    }
}
