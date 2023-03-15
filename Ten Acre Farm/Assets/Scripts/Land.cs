using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour, ITimeTracker
{
    public enum LandStatus
    {
        Soil, Farmland, Watered
    }

    public LandStatus landStatus;

    public Material soilMat, farmlandMat, wateredMat;
    new Renderer renderer;

    public GameObject select;

    GameTime timeWatered;

    void Start()
    {
        renderer = GetComponent<Renderer>();

        SwitchLandStatus(LandStatus.Soil);

        Select(false);

        TimeManager.Instance.RegisterTracker(this);
    }

    
    void Update()
    {
        
    }

    public void SwitchLandStatus(LandStatus statusToSwitch)
    {
        landStatus = statusToSwitch;
        Material materialToSwitch = soilMat;

        switch(statusToSwitch)
        {
            case LandStatus.Soil:
                materialToSwitch = soilMat;
                break;
            case LandStatus.Farmland:
                materialToSwitch = farmlandMat;
                break;
            case LandStatus.Watered:
                materialToSwitch = wateredMat;
                timeWatered = TimeManager.Instance.GetGameTime();
                break;

        }

        renderer.material = materialToSwitch;
    }

    public void Select(bool toggle)
    {
        select.SetActive(toggle);
    }

    public void Interact()
    {
        //Debug.Log("Interact");
        ItemData toolSlot = InventoryManager.Instance.equippedTool;

        EquipmentData equipmentTool = toolSlot as EquipmentData;

        if (equipmentTool != null)
        {
            EquipmentData.ToolType toolType = equipmentTool.Type;

            switch (toolType)
            {
                case EquipmentData.ToolType.Hoe:
                    SwitchLandStatus(LandStatus.Farmland);
                    break;
                case EquipmentData.ToolType.WateringCan:
                    SwitchLandStatus(LandStatus.Watered);
                    break;                
            }
        }
    }

    public void ClockUpdate(GameTime timeStamp)
    {
        if (landStatus == LandStatus.Watered)
        {
            int hoursElapsed = GameTime.CompareTimeStamps(timeWatered, timeStamp);
            Debug.Log(hoursElapsed + " hours since this was watered");

            if (hoursElapsed > 24)
            {
                SwitchLandStatus(LandStatus.Farmland);
            }
        }        
    }
}
    