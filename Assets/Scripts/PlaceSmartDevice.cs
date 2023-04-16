using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceSmartDevice : MonoBehaviour
{
    public GameObject smartDevicePrefab;
    public Button resetButton; // Reference to the reset button
    private GameObject spawnedObject;
    private ARRaycastManager raycastManager;
    private Vector2 touchposition;
    private List<ARRaycastHit> Hits = new List<ARRaycastHit>();

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        resetButton.onClick.AddListener(ResetSmartDevicePosition); // Add listener to reset button
    }

    bool trygettouchposition(out Vector2 touchposition)
    {
        if (Input.touchCount > 0)
        {
            touchposition = Input.GetTouch(0).position;
            return true;
        }
        touchposition = default;
        return false;
    }

    public void Update()
    {
        if (!trygettouchposition(out Vector2 touchposition))
            return;

        if (raycastManager.Raycast(touchposition, Hits, TrackableType.PlaneWithinPolygon))
        {
            var hitpose = Hits[0].pose;

            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(smartDevicePrefab, hitpose.position, hitpose.rotation);
            }
            else
            {
                spawnedObject.transform.position = hitpose.position;
            }
        }
    }

    // Called when reset button is clicked
    public void ResetSmartDevicePosition()
    {
        if (spawnedObject != null)
        {
            // Reset the position of the spawned object in front of the camera
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 resetPosition = Camera.main.transform.position + cameraForward * 1.0f; // Set the reset position 1.0f units in front of the camera
            spawnedObject.transform.position = resetPosition;
        }
    }

    public void BackButtonOnClick()
    {
        SceneManager.LoadScene(0);

        SceneManager.UnloadScene(SceneManager.GetActiveScene());
    }
}
