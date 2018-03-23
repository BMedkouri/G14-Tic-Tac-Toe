using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player
{
    public Image panel;
    public Text text;
}

[System.Serializable]
public class PlayerColor
{
    public Color panelColor;
    public Color textColor;
}

public class GameController : MonoBehaviour {

    public Text[] buttonList;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject restartButton;

    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;

    private string playerSide;
    private int moveCount;

    private void Awake()
    {
        SetGameControllerReferenceOnButtons();

        gameOverPanel.SetActive(false);

        moveCount = 0;

        restartButton.SetActive(false);
    }

    private void SetGameControllerReferenceOnButtons ()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    public string GetPlayerSide ()
    {
        return playerSide;
    }

    public void EndTurn ()
    {
        moveCount++;

        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver(playerSide);
        }
        else
        {
            ChangeSides();
        }

        if (moveCount >= 9)
        {
            GameOver("draw");
        }
    }

    private void GameOver (string winningPlayer)
    {

        SetBoardInteractable(false);

        if (winningPlayer == "draw")
        {
            SetGameOverText("It's a Draw!");
            SetPlayerColors(winningPlayer);
        }
        else
        {
            SetGameOverText(winningPlayer + " Wins!");
        }

        restartButton.SetActive(true);
    }

    private void ChangeSides ()
    {
        playerSide = (playerSide == "X") ? "O" : "X";

        if (playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }
    }

    private void SetGameOverText (string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    public void RestartGame()
    {
        moveCount = 0;
        gameOverPanel.SetActive(false);

        SetBoardInteractable(true);

        restartButton.SetActive(false);
    }

    private void SetBoardInteractable (bool toggle)
    {

        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;

            if(toggle)
            {
                buttonList[i].text = "";
            }
        }
    }

    private void SetPlayerColors (string text)
    {
        if (text == "draw")
        {
            playerX.panel.color = activePlayerColor.panelColor;
            playerX.text.color = activePlayerColor.textColor;
            playerO.panel.color = activePlayerColor.panelColor;
            playerO.text.color = activePlayerColor.textColor;
        }
    }

    private void SetPlayerColors (Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    public void SetStartingSide (string startingSide)
    {
        playerSide = startingSide;
       
        if (playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }
    }
}