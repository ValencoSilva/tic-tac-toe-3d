using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class OnlineMatch : MonoBehaviourPunCallbacks
{
    public Text queueStatusText;
    public Button joinQueueButton;
    public Button leaveQueueButton;

    private Queue<Player> playerQueue = new Queue<Player>();

    private bool isConnectedToMaster = false;

    private void Start()
    {
        // Ensure the UI is in the correct initial state
        queueStatusText.text = "Click 'Join Queue' to start.";
        leaveQueueButton.gameObject.SetActive(false);

        // Add listeners to buttons
        joinQueueButton.onClick.AddListener(JoinQueue);
        leaveQueueButton.onClick.AddListener(LeaveQueue);

        // Connect to the Photon Master Server
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        isConnectedToMaster = true;

        // Automatically join the lobby when connected to the Master Server
        PhotonNetwork.JoinLobby();
    }

    public void JoinQueue()
    {
        if (!isConnectedToMaster)
        {
            Debug.LogError("Not connected to Master Server yet.");
            return;
        }

        Player player = PhotonNetwork.LocalPlayer;

        // Add player to queue
        playerQueue.Enqueue(player);

        // Update UI
        queueStatusText.text = "Waiting for players... Queue Position: " + playerQueue.Count;
        joinQueueButton.gameObject.SetActive(false);
        leaveQueueButton.gameObject.SetActive(true);

        // Check if we have enough players to start a match
        TryMatchPlayers();
    }

    public void LeaveQueue()
    {
        Player player = PhotonNetwork.LocalPlayer;

        // Create a temporary queue to hold players that remain in the queue
        Queue<Player> newQueue = new Queue<Player>();

        // Re-add all players to the new queue except the one leaving
        foreach (Player p in playerQueue)
        {
            if (p != player)
            {
                newQueue.Enqueue(p);
            }
        }

        // Replace the old queue with the new one
        playerQueue = newQueue;

        // Update UI
        queueStatusText.text = "Click 'Join Queue' to start.";
        leaveQueueButton.gameObject.SetActive(false);
        joinQueueButton.gameObject.SetActive(true);
    }

    private void TryMatchPlayers()
    {
        // Assume 2 players for a match in this example
        if (playerQueue.Count >= 2)
        {
            // Dequeue two players and start a match
            Player player1 = playerQueue.Dequeue();
            Player player2 = playerQueue.Dequeue();

            StartMatch(player1, player2);
        }
    }

    private void StartMatch(Player player1, Player player2)
    {
        if (!isConnectedToMaster)
        {
            Debug.LogError("Not connected to Master Server yet.");
            return;
        }

        // Create a unique room name for the match
        string roomName = "Match_" + System.Guid.NewGuid().ToString();

        // Create room options
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;  // Set the maximum players to 2

        // Create a room and move both players into it
        PhotonNetwork.CreateRoom(roomName, roomOptions, null);

        // Update UI to indicate the match is starting
        queueStatusText.text = "Match found! Waiting for players to connect...";
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        // Check if there are two players in the room
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            // Start the game only when there are exactly two players
            StartGame();
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        // Check if there are now two players in the room
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            // Start the game when the second player joins
            StartGame();
        }
    }

    private void StartGame()
    {
        // This method starts the game when exactly two players are connected
        queueStatusText.text = "Starting game...";
        PhotonNetwork.LoadLevel("2Online"); // Make sure you have a scene named "GameScene"
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);

        // This callback is called when a player leaves the room.
        // Handle the disconnection, such as pausing the game or waiting for a reconnection.
        queueStatusText.text = "Player left. Waiting for another player...";
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);

        // This callback is called when the player fails to join a room.
        queueStatusText.text = "Failed to join the match. Please try again.";
    }

    
}
