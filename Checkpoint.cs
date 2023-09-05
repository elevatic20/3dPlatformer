using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //ako je u koliziji s objektom player postavlja se novi zadnji checkpoint
    private void OnTriggerEnter(Collider other)                                             
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Checkpoint");
            //Debug.Log(transform.position);
            CheckpointManager checkpointManager = FindObjectOfType<CheckpointManager>();
            if (checkpointManager != null)
            {
                //Debug.Log("Postavljen checkpoint");
                //Debug.Log(transform);
                checkpointManager.SetCheckpoint(transform);
                
            }

            //Deaktiviranje checkpoing gameObjekta i pokretanje zvuka za isti
            gameObject.SetActive(false);
            AudioManagerScript.Instance.PlayCheckpointSound();

        }
    }
}
