using System;
using UnityEngine;

[Serializable]
public class BattleManager
{
    private readonly TurnController _turnController;
    public GameObject ActivePlayer{ get {return _activePlayer;}}

    [SerializeField]
    private GameObject _activePlayer;

    public BattleManager(GameObject[] entities)
    {
        _turnController = new TurnController(entities, new RandomShuffler<GameObject>());
    }

    public void InitializeBattle()
    {
        this.setNextPlayer();
    }

    public void EndCurrentTurn()
    {
        this.setNextPlayer();
    }

    private void setNextPlayer()
    {
        _activePlayer = _turnController.nextEntity();
    }



}
