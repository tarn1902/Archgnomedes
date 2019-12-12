/*----------------------------------------
File Name: Splatter.cs
Purpose: Creates blood on collision
Author: Tarn Cooper
Modified: 28/11/2019
------------------------------------------
Copyright 2019 Tarn Cooper.
-----------------------------------*/
using UnityEngine;
using System.Collections.Generic;

public class Splatter : MonoBehaviour
{
    public GameObject blood = null;
    private GameObject tempBlood = null;
   
    //-----------------------------------------------------------
    // creates particle emitter on point face away from normal
    //-----------------------------------------------------------
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint cp = collision.GetContact(0);
        tempBlood = Instantiate(blood);
        tempBlood.transform.position = cp.point;
        tempBlood.transform.up = cp.normal;
        tempBlood.transform.parent = null;
    }
}
