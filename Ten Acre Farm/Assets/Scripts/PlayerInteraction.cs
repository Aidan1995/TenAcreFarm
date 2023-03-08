using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    PlayerController playerController;

    // The land the player is currently selecting
    Land selectedLand;

    void Start()
    {
        // Get access to the PlayerController component as PlayerInteraction
        // is a child class and cannot be directly accessed
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

    // Handles what happens when the interaction raycast hits something that is interactable
    void OnInteractableHit(RaycastHit hit)
    {
        Collider other = hit.collider;
        // Check if the player is going to interact with the land
        
        if (other.tag == "Land")
        {
            //Debug.Log("I am standing by farmable land");
            Land land = other.GetComponent<Land>();
            //land.Select(true);
            SelectLand(land);
            return;
        }

        // Unselect the land if the player is not currently standing on any land
        if(selectedLand != null) 
        {
            selectedLand.Select(false);
            selectedLand = null;
        }
    }

    // Handles the selection process of the land
    void SelectLand(Land land)
    {
        // Set the previously selected land to false (if any)
        if (selectedLand != null)
        {
            selectedLand.Select(false);
        }

        // Set the new selected land to the land we're selecting now
        selectedLand = land;
        land.Select(true);
    }

    // Triggered when the player presses the tool button
    public void Interact()
    {
        // Check if player is on farmable land
        if(selectedLand != null)
        {
            selectedLand.Interact();
            return;
        }
        else
        Debug.Log("Not on any land!");
    }

}
