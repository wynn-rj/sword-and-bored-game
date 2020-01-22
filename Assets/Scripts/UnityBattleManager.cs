using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityBattleManager : MonoBehaviour
{
    public GameObject ActivePlayer{ get{ return _battleManager.ActivePlayer;}}

    public GameObject[] entities = new GameObject[2];

    [SerializeField]
    private BattleManager _battleManager;

    void Awake()
    {
        _battleManager = new BattleManager(entities);
    }
    // Start is called before the first frame update
    void Start()
    {
        _battleManager.InitializeBattle();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
