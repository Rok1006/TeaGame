using System.Collections;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public static PourDetector Instance;
    public int pourThreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    public bool isPouring = false;
    private Stream currentStream = null;
    public GameObject cup;
    public static float current_emission_rate;
    Vector3 targetPos;
    public float rotatespeed;
    [SerializeField] AudioSource tea_sound;
    [SerializeField] AudioClip pour_sound;
    //Method2: Particle System
    public GameObject teaParticles;
    public ParticleSystem tp;
    void Awake() {
        Instance = this;
    }
    void Start() {
        targetPos = new Vector3(cup.transform.position.x,cup.transform.position.y,cup.transform.position.z);
        teaParticles.SetActive(true);
        tp.emissionRate = 0;
    }
    private void Update(){
        RotateTowardsCup();
        bool pourCheck = CalculatePourAngle() < pourThreshold;

        if(isPouring != pourCheck){
            isPouring = pourCheck;

            if(isPouring){
                //teaParticles.SetActive(true);
                tp.emissionRate = 70;
                Debug.Log("pouring!");
                if (tea_sound.isPlaying == false) { tea_sound.PlayOneShot(pour_sound); }
                //StartPour();   //put this back on for method 1
            }
            else{

                tp.emissionRate = 10;
                tea_sound.Stop();
                Invoke("StopPouring", 1f);
                //EndPour();    //put this back on for method 1
            }
        }
        current_emission_rate= tp.emissionRate;
    }
    void StopPouring(){
        tp.emissionRate = 0;
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