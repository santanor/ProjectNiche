using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CursorAffordance : MonoBehaviour {
    
    [SerializeField]
    CameraRaycaster raycaster;

    [SerializeField]
    Texture2D walkCursor = null;
    [SerializeField]
    Texture2D enemyCursor = null;
    [SerializeField]
    Texture2D errorCursor = null;

    [SerializeField]
    Vector2 cursorHotSpot = new Vector2(96, 96);

	// Use this for initialization
	void Start () {
        Assert.IsNotNull(raycaster);
        Assert.IsNotNull(walkCursor);
        Assert.IsNotNull(enemyCursor);
        Assert.IsNotNull(errorCursor);
	}
	
	// Update is called once per frame
	void Update () {

        switch (raycaster.layerHit)
        {
            case Layer.Enemy:
                Cursor.SetCursor(enemyCursor, cursorHotSpot, CursorMode.Auto);
                break;
            case Layer.Walkable:
                Cursor.SetCursor(walkCursor, cursorHotSpot, CursorMode.Auto);
                break;
            case Layer.RaycastEndStop:
                Cursor.SetCursor(errorCursor, cursorHotSpot, CursorMode.Auto);
                break;
        }
	}
}
