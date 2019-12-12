/*----------------------------------------
File Name: PlayerSpawn.cs
Purpose: Spawns Enemies to set positions
Author: Tarn Cooper
Modified: 28/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

namespace Archgnomedes
{
    public class PlayerSpawn : MonoBehaviour
    {
        [Header("Spawn Settings")]
        public Transform[] spawnLocations;
        public GameObject playerToSpawn;
        private GameObject playerTemp = null;
        private int currentSpawnLocation = 0;

        //-----------------------------------------------------------
        // Start is by finding player or creating a new one if it 
        // does not exists
        //-----------------------------------------------------------
        private void Start()
        {
            currentSpawnLocation = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().currentSpawnLocation;
            playerTemp = Instantiate(playerToSpawn);
            Respawn();
        }

        //-----------------------------------------------------------
        // Moves player to set position
        //-----------------------------------------------------------
        public void Respawn()
        {
            playerTemp.SetActive(false);
            playerTemp.transform.position = spawnLocations[currentSpawnLocation].transform.position;
            playerTemp.transform.rotation = spawnLocations[currentSpawnLocation].transform.rotation;
            playerTemp.SetActive(true);
        }
    }
}
