using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Experimental.XR;

public class PlacementDemo : MonoBehaviour
{
    public Rigidbody grenadePrefab;
    public GameObject cursor;
    public LayerMask layer;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        LaunchGrenade();
    }

    void LaunchGrenade()
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if(Physics.Raycast(camRay, out hit, 100f))
        {
            cursor.SetActive(true);
            cursor.transform.position = hit.point + Vector3.up * 0.1f;
        }
    }

    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        //define the distance x and y direction first
        Vector3 distance = target - origin;
        Vector3 distnaceXZ = distance;
        distnaceXZ.y = 0f; //set Y force to zero, and only keeping the X and Z component

        //creat a float variable to represent the distance
        float horizontalDistance = distnaceXZ.magnitude;
        float verticalDistance = distance.y;

        //calculating velocity using projectil formula
        //
        float horizontalVelocity = horizontalDistance / time;
        float verticalVelocity = verticalDistance / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        //result to be returned
        Vector3 result = distnaceXZ.normalized;
        result *= horizontalVelocity;
        result.y = verticalVelocity;

        return result;
    }
}




//     public GameObject placementIndicator;
//     public GameObject objectToPlace;
//     private ARRaycastManager _arRaycastManager;
//     private Pose placementPose;
//     private bool isPlacementPoseValid = false;
//     // Start is called before the first frame update
//     // Run once
//     void Start()
//     {
//         _arRaycastManager = GetComponent<ARRaycastManager>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         UpdatePlacementPose();
//         UpdatePlacementInidcator();

//         if(isPlacementPoseValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
//             PlaceObject();
//         }
//     }
    
//     private void PlaceObject()
//     {
//         Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
//     }
//     private void UpdatePlacementPose()
//     {
//         var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
//         var hits = new List<ARRaycastHit>();
//         _arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes); 

//         isPlacementPoseValid = hits.Count > 0;
//         if (isPlacementPoseValid)
//         {
//             placementPose = hits[0].pose;

//             var cameraForward = Camera.current.transform.forward;
//             var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
//             placementPose.rotation = Quaternion.LookRotation(cameraBearing);
//         }
//     }

//     private void UpdatePlacementInidcator()
//     {
//         if (isPlacementPoseValid)
//         {
//             placementIndicator.SetActive(true);
//             placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
//         } else {
//             placementIndicator.SetActive(false);
//         }
//     }
// }
