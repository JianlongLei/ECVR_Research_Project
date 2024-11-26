using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using Mono.Data.Sqlite;
using TMPro;

public class SelectParentsWristMenuController : MonoBehaviour
{
    public Dictionary<string, int> robotNameToId = new Dictionary<string, int>();
    public TMP_Dropdown parent1Dropdown;
    public TMP_Dropdown parent2Dropdown;
    public TMP_Dropdown mutateDropdown;
    private AppConfig config;
    public int populationSize;

    void Start()
    {
        config = ConfigManager.LoadConfig("Assets/revolve2/vr/db/config.json");
        populationSize = config.POPULATION_SIZE;
        List<int> individualIds = DatabaseManager.GetIndividualIdsFromLatestGeneration();
        List<string> parentOptions = new List<string>();
        for (int i = 0; i < individualIds.Count; i++)
        {
            string robotName = $"Robot {i + 1}";
            robotNameToId.Add(robotName, individualIds[i]);
            parentOptions.Add(robotName);
        }
        parent1Dropdown.AddOptions(parentOptions);
        parent2Dropdown.AddOptions(parentOptions);
    }

    public (int parent1Id, int parent2Id, bool mutate) GetSelectionData()
    {
        int parent1Id = robotNameToId[parent1Dropdown.options[parent1Dropdown.value].text];
        string parent2Value = parent2Dropdown.options[parent2Dropdown.value].text;
        int parent2Id = parent2Value == "None" ? -1 : robotNameToId[parent2Value];
        bool mutate = mutateDropdown.options[mutateDropdown.value].text == "Mutate";
        return (parent1Id, parent2Id, mutate);
    }
}
