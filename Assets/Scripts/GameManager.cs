using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //there is an 'object' called GameState
    //it stores the following:
    // a) an array of cards in the deck
    // b) an array of player objects
    // c) an array of door cards

    //each player 'object' stores:
    // a) an array of owned cards
    // b) a history of movement positions
    // c) player's character
    // d) whether they are an alien
    public int playerCount = 4;
    public int alienCount = 1;
    public int currentPlayerTurn;
    public GameState currGameState;

    void SetupUI()
    {
        //set up the UI elements, set up the network, etc
        //maybe should happen in a different object
    }

    //takes a player number and a coordinate
    void PerfMovement(int playerNum, Vector2Int coord)
    {
        //move currPlayers[playerNum] to coord
    }

    //takes a player number
    //has that player attack at their current position
    void AttackCoord(int playerNum)
    {

    }

    //takes a player number
    //draws card and gives it to player
    void DrawCard(int playerNum)
    {
        //draw card from top of cardDeck and give to player
    }

    //takes  player number
    //handles attempt to leave via airlock
    void EscapeAttempt(int playerNum)
    {
        //draw card from doorDeck and resolve escape attempt
    }

    //handles initial game setup
    void SetupGame()
    {
        //somehow get player names and numbers from UI
        //create player objects for each player
        //assign roles to each player, and inform them of the role
        //somehow load the map from the maploader
    }

    //ends a given player's turn
    //receives as a broadcast event
    void EndTurn(int playerNum)
    {
        
    }

    //starts a given player's turn
    //broadcasts as an event
    void StartTurn(int playerNum)
    {

    }
}

public struct GameState
{
    public List<int> cardDeck;
    public List<Player> currPlayers;
    public List<bool> doorDeck;
}

public struct Player
{
    public int playerNum;
    public string playerName;
    public List<int> ownedCards;
    public List<Vector2Int> posList;
    public int currCharacter;
    public bool isAlien;
}