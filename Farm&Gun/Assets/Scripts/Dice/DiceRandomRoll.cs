using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class DiceRandomRoll : MonoBehaviour
{
    public GameObject YellowDicePrefab;
    public GameObject RedDicePrefab;

    public Text RedDiceTextDisplay;
    public Text YellowDiceTextDisplay;

    public Image RedDiceImageDisplay;
    public Image YellowDiceImageDisplay;

    public Texture[] FarmAnimalImages;

    public DiceData RedDiceData, YellowDiceData;

    [FormerlySerializedAs("soundCollideFloor")]
    public AudioSource soundCollideFloor;

    [FormerlySerializedAs("soundCollideDice")]
    public AudioSource soundCollideDice;

    string yellowFaceName, redFaceName;
    bool generated = false;

    public DiceData GenerateDice(GameObject DicePrefab)
    {
        InitialState initial = SetInitialState();

        DiceData newDiceData = new DiceData(Instantiate(DicePrefab));

        for (int i = 0; i < 12; i++)
        {
            GameObject child = newDiceData.diceObject.transform.GetChild(i).gameObject;
            newDiceData.faceDetectors.Add(child);
        }

        newDiceData.diceObject.transform.position = initial.position;
        newDiceData.diceObject.transform.rotation = initial.rotation;
        newDiceData.rb.useGravity = true;
        newDiceData.rb.isKinematic = false;
        newDiceData.rb.velocity = initial.force;
        newDiceData.rb.AddTorque(initial.torque, ForceMode.VelocityChange);

        return newDiceData;

    }

    private InitialState SetInitialState()
    {
        //Randomize X, Y, Z position in the bounding box
        float x = transform.position.x + Random.Range(-transform.localScale.x / 2,
                                                       transform.localScale.x / 2);
        float y = transform.position.y + Random.Range(-transform.localScale.y / 2,
                                                       transform.localScale.y / 2);
        float z = transform.position.z + Random.Range(-transform.localScale.z / 2,
                                                       transform.localScale.z / 2);
        Vector3 position = new Vector3(x, y, z);

        x = Random.Range(0, 360);
        y = Random.Range(0, 360);
        z = Random.Range(0, 360);
        Quaternion rotation = Quaternion.Euler(x, y, z);

        x = Random.Range(0, 25);
        y = Random.Range(0, 25);
        z = Random.Range(0, 25);
        Vector3 force = new Vector3(x, -y, z);

        x = Random.Range(0, 50);
        y = Random.Range(0, 50);
        z = Random.Range(0, 50);
        Vector3 torque = new Vector3(x, y, z);
       
        return new InitialState(position, rotation, force, torque);
    }

    [System.Serializable]
    /// The data containing all references to all dices
    /// so we only need to do GetComponent call once in the script
    public struct DiceData
    {
        public GameObject diceObject;
        public Rigidbody rb;
        public List<GameObject> faceDetectors;

        public DiceData(GameObject diceObject)
        {
            this.diceObject = diceObject;
            this.rb = diceObject.GetComponent<Rigidbody>();
            this.rb.maxAngularVelocity = 1000;
            this.faceDetectors = new List<GameObject>();
        }
    }

    [System.Serializable]
    /// This is a struct to hold all data needed to initialize the dice
    public struct InitialState
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 force;
        public Vector3 torque;

        public InitialState(Vector3 position, Quaternion rotation,
                            Vector3 force, Vector3 torque)
        {
            this.position = position;
            this.rotation = rotation;
            this.force = force;
            this.torque = torque;
        }
    }

    public string FindFaceResult(List<GameObject> faceDetectors)
    {
        //Since we have all child objects for each face,
        //We just need to find the highest Y value
        int maxIndex = 0;
        string faceName = "";
        for (int i = 1; i < faceDetectors.Count; i++)
        {
            if (faceDetectors[maxIndex].transform.position.y <
                faceDetectors[i].transform.position.y)
            {
                maxIndex = i;
            }
        }
        faceName = faceDetectors[maxIndex].tag;
        faceName = faceName.Remove(0, 8);

        //faceDetectors[maxIndex].GetComponent<Renderer>().enabled=true;

        return faceName;
    }

    public void StartRoll()
    {
        //destroy old
        Destroy(RedDiceData.diceObject);
        Destroy(YellowDiceData.diceObject);
        YellowDiceTextDisplay.text = "";
        RedDiceTextDisplay.text = "";

        //generate newk
        RedDiceData = GenerateDice(RedDicePrefab);
        YellowDiceData = GenerateDice(YellowDicePrefab);
        generated = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Dice"))
        {
            PlaySoundCollideFloor();
        }
        if (collision.transform.CompareTag("Floor"))
        {
            PlaySoundCollideDice();
        }
    }

    public void PlaySoundCollideFloor()
    {
        if (!soundCollideFloor.isPlaying)
            soundCollideFloor.Play();
    }
    public void PlaySoundCollideDice()
    {
        if (!soundCollideDice.isPlaying)
            soundCollideDice.Play();
    }

    private void Update()
    {
        if (generated == true && RedDiceData.rb.velocity == Vector3.zero && YellowDiceData.rb.velocity == Vector3.zero)
        {
            yellowFaceName = FindFaceResult(YellowDiceData.faceDetectors);
            Debug.Log("wylosowana zolta to = " + yellowFaceName);
            YellowDiceTextDisplay.text = yellowFaceName;
            //YellowDiceImageDisplay.Icon;
            //update view to display rolled animal
            redFaceName = FindFaceResult(RedDiceData.faceDetectors);
            Debug.Log("wylosowana czerwona to = " + redFaceName);
            RedDiceTextDisplay.text = redFaceName;
            generated = false;
        } 
    }
}



