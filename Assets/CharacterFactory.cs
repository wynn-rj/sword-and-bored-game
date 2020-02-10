using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Battlefield.CreaturScripts;
using SwordAndBored.Battlefield;
using Cinemachine;
using SwordAndBored.Battlefield.CameraUtilities;
using SwordAndBored.Battlefield.TurnMechanism;

public class CharacterFactory : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject indicator;
    public CameraManager camManager;
    public TurnManager turnManager;
    public Transform unitHolder;
    
    void Awake()
    {
        for (int i = 0; i < 1; i++)
        {
            GameObject unit = Instantiate(playerPrefab, new Vector3(i, 1.5f, 0), Quaternion.identity);
            UniqueCreature uniqueCreature = unit.GetComponent<UniqueCreature>();
            UnitAbilitiesContainer abilities = unit.GetComponent<UnitAbilitiesContainer>();
            UnitStats stats = unit.GetComponent<UnitStats>();
            TurnBrain brain = unit.GetComponent<TurnBrain>();
            CinemachineVirtualCamera cam = uniqueCreature.currentCamera;
            camManager.cameras.Add(cam.gameObject);
            turnManager.units.Add(unit);
            turnManager.activePlayer = brain;

            //UniqueCreature
            uniqueCreature.creatureName = "Daniel";
            uniqueCreature.maxHealth = 20;
            uniqueCreature.maxMovement = 6;

            //abilities
            for (int j = 0; j < 5; j++)
            {
                //abilities.abilities.add();
            }

            //stats
            stats.health = 5;
            stats.attack = 5;
            stats.specialAttack = 5;
            stats.defense = 5;
            stats.specialDefense = 5;
            stats.movement = 5;
            stats.speedIntit = 5;

            //brain
            brain.tileIndictor = indicator;
            brain.startCoordinates = new Vector2(3, 5);

            unit.transform.parent = unitHolder;
        }


    }
    
}
