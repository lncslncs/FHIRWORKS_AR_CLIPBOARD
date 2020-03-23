//Lilly Neubauer FHIRWorks Project
//Connects to the FHIR API and does some basic JSON parsing to prepare data for display
//Listens for patient ID provided in text input field and fetches data object

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using PatientData;



//reference for HttpClient https://stackoverflow.com/questions/4015324/how-to-make-http-post-web-request
public class FHIRConnection : MonoBehaviour
{
    public Button showData;
    public InputField IDToShow;

    [SerializeField]
    public string clientID;
    public string client_Secret;
    public string scope;
    public string token_url;

    public string patientDisplayData;

    private static readonly HttpClient client = new HttpClient();
    
    void Start()
    {
        //fetch data for a patientID when the showData button is clicked
        showData.onClick.AddListener(delegate {getPatientDataByID(IDToShow.text);});

        //Open connection to FHIR by getting auth token
        getFHIRAuthToken();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class token_response {
        public string token_type { get; set; }
        public string expires_in {get; set; }
        public string ext_expires_in {get;  set;}
        public string access_token {get; set;} 
    }

    private async void getFHIRAuthToken()
      {
        var body = new Dictionary<string, string> {
            {"grant_type", "client_credentials"},
            {"client_id", clientID},
            {"client_secret", client_Secret},
            {"scope", scope}
        };

        FormUrlEncodedContent content = new FormUrlEncodedContent(body);

        var response = await client.PostAsync(token_url, content);

        String responseString = await response.Content.ReadAsStringAsync();

        var new_token = JsonConvert.DeserializeObject<token_response>(responseString);

        String token = new_token.access_token;

        //Debug.Log("Token is: " + token);

        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

        //Start with a random patient to display
        getPatientDataByID("ac529235-6fbc-48f0-a3fa-de7f1924d497");

        // another random patient ID for testing is: ac529235-6fbc-48f0-a3fa-de7f1924d497

      }

      //this function allows you to get all patient data
      private async void getAllPatientData() {

          var result = await client.GetAsync("https://gosh-fhir-synth.azurehealthcareapis.com/Patient");

          String patients = await result.Content.ReadAsStringAsync();

          Debug.Log(patients);
      }

      //Get patient Data given a patientID
      private async void getPatientDataByID(string patientID) {

        var id_result = await client.GetAsync("https://gosh-fhir-synth.azurehealthcareapis.com/Patient/" + patientID);

        String patientInfo = await id_result.Content.ReadAsStringAsync();

        var patient_object = JObject.Parse(patientInfo);

        var firstName = patient_object["name"][0]["given"][0];
        var lastName = patient_object["name"][0]["family"];
        var title = patient_object["name"][0]["prefix"][0];

        var city = patient_object["address"][0]["city"];
        var state = patient_object["address"][0]["state"];
        var country = patient_object["address"][0]["country"];
        var street = patient_object["address"][0]["line"][0];

        //Debug.Log("Patient's Name is:" + title + " " + firstName + " " + lastName);

        var _patient = Patient.FromJson(patientInfo);

        string _gender =  _patient.Gender.ToString();
        string _address = street + ", " + city + ", " + state + ", " + country;
        string _DOB = _patient.BirthDate.ToString();
        string _name = title + " " + firstName + " " + lastName;

        patientDisplayData = "Name: " + _name + "\n" + "Gender: " + _gender + "\n" + "Address: " + _address + "\n" + "DOB: " + _DOB + "\n";

        //Debug.Log(patientDisplayData);

      }

    
}
