//Lilly Neubauer FHIRWorks Project
//Gets data from FHIRConnection script and displays on textMeshPro that is attached to the plane prefab that follows the tracked AR image

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ShowData : MonoBehaviour
{
    string dataToDisplay;

    public GameObject displayText;

    TextMesh textMesh;
    
    void Start()
    {
        
        

    }

    void Update()
    {
        //Gets patient data from the FHIRConnector script
        dataToDisplay = GameObject.Find("FHIRConnector").GetComponent<FHIRConnection>().patientDisplayData;

        //Debug.Log("Received string from FHIR Connector: " + dataToDisplay);

        //Displays on the text mesh that this script is attached to
        GetComponent<TextMeshPro>().text = dataToDisplay;
        
        
    }
}
