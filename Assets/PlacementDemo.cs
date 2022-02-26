using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Experimental.XR;
public class PlacementDemo : MonoBehaviour
{


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
