using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        // dohvacamo skriptu Inventory u kojoj je funkcija CheeseCollector
        Inventory playerInventor = other.GetComponent<Inventory>();

        if (playerInventor != null)
        {
            //povecavamo broj sira u inventory i postavljamo objekte sira da se ne renderiraju
            playerInventor.CheeseCollector();
            DisplayUI.Instance.UpdateCheeseCount(playerInventor);
            gameObject.SetActive(false);
        }
    }

}