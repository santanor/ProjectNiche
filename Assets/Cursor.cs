using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Cursor : MonoBehaviour {
    
    [SerializeField]CameraRaycaster raycaster;

	// Use this for initialization
	void Start () {
        Assert.IsNotNull(raycaster);
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(raycaster.layerHit);
	}
}
