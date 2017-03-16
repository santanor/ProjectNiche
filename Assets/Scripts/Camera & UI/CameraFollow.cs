using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class CameraFollow : MonoBehaviour {

    public GameObject Player;

	// Use this for initialization
	void Start () {
        Assert.IsNotNull(Player); 
	}
	
	// Update is called once per frame
	void LateUpdate () {
        this.transform.position = Player.transform.position;
	}
}
