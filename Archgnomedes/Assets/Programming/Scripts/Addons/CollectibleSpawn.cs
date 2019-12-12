/*----------------------------------------
File Name: CollectibleSpawn.cs
Purpose: Spawns collectible
Author: Tarn Cooper
Modified: 28/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;

public class CollectibleSpawn : MonoBehaviour
{
    public GameObject collectible = null;
    //-----------------------------------------------------------
    // Spawns a collectible into world
    //-----------------------------------------------------------
    public void SpawnCollectible()
    {
        collectible = Instantiate(collectible, transform);
        collectible.transform.parent = null;
    }

}
