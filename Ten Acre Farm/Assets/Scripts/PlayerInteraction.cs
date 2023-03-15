using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    PlayerController playerController;

    Land selectedLand;

    void Start()
    {        
        playerController = transform.parent.GetComponent<PlayerController>();
    }

    
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            OnInteractableHit(hit);
        }
    }

    
    void OnInteractableHit(RaycastHit hit)
    {
        Collider other = hit.collider;
        
        if (other.tag == "Land")
        {
            Land land = other.GetComponent<Land>();
            //land.Select(true);
            SelectLand(land);
            return;
        }

        if(selectedLand != null) 
        {
            selectedLand.Select(false);
            selectedLand = null;
        }
    }

    void SelectLand(Land land)
    {
        if (selectedLand != null)
        {
            selectedLand.Select(false);
        }

        selectedLand = land;
        land.Select(true);
    }

    public void Interact()
    {
        if(selectedLand != null)
        {
            selectedLand.Interact();
            return;
        }
        else
        Debug.Log("Not on any land!");
    }

}
