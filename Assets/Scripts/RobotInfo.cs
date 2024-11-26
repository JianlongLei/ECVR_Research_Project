using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using TMPro;

public class RobotInfo : MonoBehaviour
{
    public TMP_Text infoText;
    public GameObject infoUi;
    private XRSimpleInteractable interactable;

    void Start()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(SelectEntered);
        }
    }
    
    void SelectEntered(SelectEnterEventArgs args)
    {
        infoUi.SetActive(!infoUi.activeSelf);
    }

    public void UpdateRobotInfo(string name, float fitness, int id) {
        string robotInfo = $"Robot name: {name}\n" +
                           $"Robot fitness: {fitness:F2}\n" +
                           $"Robot ID: {id}";
        infoText.text = robotInfo;
    }
}
