using UnityEngine;
using System.Collections;

public class ObjectifFloor : MonoBehaviour {

    public float timeToCatch = 10.0f;
    public float amountOfGold = 10f;

    private string teamInside;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerScript>())
        {

        }
    }
}
