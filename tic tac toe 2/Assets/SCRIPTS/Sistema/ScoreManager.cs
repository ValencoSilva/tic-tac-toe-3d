using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int player1Score = 1000; // Initial score for Player 1
    public int player2Score = 1000; // Initial score for Player 2

    public Text player1ScoreText;   // UI Text element to display Player 1's score
    public Text player2ScoreText;   // UI Text element to display Player 2's score

    public void Start()
    {
        // Initialize the UI with the current scores
        UpdateScoreUI();
    }

    public bool CanAffordPower(int cost, GMTeste.PlayerType player)
    {
        if (player == GMTeste.PlayerType.Human)
        {
            return player1Score >= cost;
        }
        else if (player == GMTeste.PlayerType.Human2)
        {
            return player2Score >= cost;
        }
        return false;
    }

    public void DeductPoints(int cost, GMTeste.PlayerType player)
    {
        if (player == GMTeste.PlayerType.Human)
        {
            player1Score -= cost;
            if (player1Score < 0) player1Score = 0; // Prevent negative scores
        }
        else if (player == GMTeste.PlayerType.Human2)
        {
            player2Score -= cost;
            if (player2Score < 0) player2Score = 0; // Prevent negative scores
        }

        // Update the UI after deduction
        UpdateScoreUI();
    }

    public void AddPoints(int points, GMTeste.PlayerType player)
    {
        if (player == GMTeste.PlayerType.Human)
        {
            player1Score += points;
        }
        else if (player == GMTeste.PlayerType.Human2)
        {
            player2Score += points;
        }

        // Update the UI after addition
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (player1ScoreText != null)
        {
            player1ScoreText.text = $"Player 1 Score: {player1Score}";
        }

        if (player2ScoreText != null)
        {
            player2ScoreText.text = $"Player 2 Score: {player2Score}";
        }
    }
}
