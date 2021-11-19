using System.Collections;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    private bool isPouring = false;
    private Stream currentStream = null;
    public GameObject cup;
    Vector3 targetPos;
    public float rotatespeed;

    void Start() {
        targetPos = new Vector3(cup.transform.position.x,cup.transform.position.y,cup.transform.position.z);
    }
    private void Update(){
        RotateTowardsCup();
        bool pourCheck = CalculatePourAngle() < pourThreshold;

        if(isPouring != pourCheck){
            isPouring = pourCheck;

            if(isPouring){
                StartPour();
            }else{
                EndPour();
            }
        }
    }

    private void StartPour(){
        print("Start");
        currentStream = CreateStream();
        currentStream.Begin();
    }
    private void EndPour(){
        print("End");
        currentStream.End();
        currentStream = null;
    }

    private float CalculatePourAngle(){
        return transform.forward.y * Mathf.Rad2Deg;
        //makre sure the direction s corrent every mesh is different   may be z
    }


    private Stream CreateStream(){
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }
    void RotateTowardsCup(){
        // float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        // Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotatespeed * Time.deltaTime);
    }
}