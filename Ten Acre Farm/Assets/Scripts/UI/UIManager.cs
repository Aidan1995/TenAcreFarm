using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set;}

    [Header("Status Bar")]

    public Image itemDisplayImage;

    [Header("Inventory System")]
        
    public GameObject inventoryPanel;

    public HandInventorySlot toolEquipSlot;

    public InventorySlot[] toolSlots;

    public HandInventorySlot itemEquipSlot;

    public InventorySlot[] itemSlots;

    public Text itemNameText;
    public Text itemDescriptionText;

    private void Awake()
    {
        // If there is more than one instance, destroy the extra ones
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {            
            Instance = this;
        }
    }

    private void Start()
    {
        RenderInventory();
        AssignSlotIndexes();
    }

    public void AssignSlotIndexes()
    {
        for (int i = 0; i < toolSlots.Length; i++)
        {
            toolSlots[i].AssignIndex(i);
            itemSlots[i].AssignIndex(i);
        }
    }
    
    public void RenderInventory()
    {        
        ItemData[] inventoryToolSlot = InventoryManager.Instance.tools;

        ItemData[] inventoryItemSlot = InventoryManager.Instance.items;

        RenderInventoryPanel(inventoryToolSlot, toolSlots);

        RenderInventoryPanel(inventoryItemSlot, itemSlots);

        toolEquipSlot.Display(InventoryManager.Instance.equippedTool);

        itemEquipSlot.Display(InventoryManager.Instance.equippedItem);

        

        ItemData equippedTool = InventoryManager.Instance.equippedTool;

        if (equippedTool != null)
        {
            itemDisplayImage.sprite = equippedTool.thumbnail;

            itemDisplayImage.gameObject.SetActive(true);

            return;
        }

        itemDisplayImage.gameObject.SetActive(false);
    }
    
    public void RenderInventoryPanel(ItemData[] slots, InventorySlot[] uiSlots)
    {       
        for (int i = 0; i < uiSlots.Length; i++)
        {
            uiSlots[i].Display(slots[i]);
        }
    }

    public void ToggleInventoryPanel()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);

        RenderInventory();
    }

    public void DisplayItemInfo(ItemData data)
    {
        if (data == null)
        {
            itemNameText.text = "";
            itemDescriptionText.text = "";

            return;
        }
        itemNameText.text = data.name;
        itemDescriptionText.text = data.description;
    }
}
