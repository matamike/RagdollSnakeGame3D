using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

    private Vector3 direction;
    public float acceleration;
    public bool isMoving = false;
    private Rigidbody rb;
    private GameObject bossBase;
    private LineRenderer lineRend;
    private GameObject grappledGO;
    //private GameObject rayParticle;
    public bool isHooked = false;

    void Start(){
        //rayParticle = GameObject.Find("RayParticle");
        //rayParticle.GetComponent<ParticleSystem>().Stop();
        lineRend = GetComponent<LineRenderer>();
        bossBase = GameObject.FindGameObjectWithTag("bossBase");
        rb = GetComponent<Rigidbody>();
        direction = Vector3.zero;
        acceleration = 40.0f;
        grappledGO = null;
    }

    void Update(){
        if (Input.GetKey(KeyCode.W)){
            isMoving = true;
            direction = Vector3.forward;
            rb.AddForce(direction * acceleration,ForceMode.Acceleration);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction, transform.up), 0.1f);
        }

        if (Input.GetKey(KeyCode.S)){
            isMoving = true;
            direction = Vector3.back;
            rb.AddForce(direction * acceleration, ForceMode.Acceleration);
           // transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction, transform.up), 0.05f);
        }

        if (Input.GetKey(KeyCode.A)){
            isMoving = true;
            direction = Vector3.left;
            rb.AddForce(direction * acceleration, ForceMode.Acceleration);
          //  transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction, transform.up), 0.05f);
        }

        if (Input.GetKey(KeyCode.D)){
            isMoving = true;
            direction = Vector3.right;
            rb.AddForce(direction * acceleration, ForceMode.Acceleration);
           // transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(direction, transform.up), 0.05f);
        }

        if (Input.GetKey(KeyCode.Space)){
            isMoving = true;
            direction = Vector3.up;
            rb.AddForce(direction * acceleration, ForceMode.Acceleration);
           // transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction, transform.up), 0.05f);
        }

        //Direction Reset and Force
        direction = Vector3.zero;
        if(rb.velocity!=Vector3.zero) rb.AddForce(direction * acceleration);

        //Grounding of player and enfore attacks / Enable movement
        if (Input.GetKeyDown(KeyCode.G)){

            if (!bossBase.TryGetComponent<CharacterJoint>(out CharacterJoint chJComp)){
                isMoving = false;
                bossBase.AddComponent<CharacterJoint>();
            }
            else{
                isMoving = true;
                Destroy(bossBase.GetComponent<CharacterJoint>());
            }
        }

        Debug.DrawLine(transform.position, transform.position + new Vector3(0, 0, 7f), Color.green);
        if (Physics.Linecast(transform.position, transform.position + new Vector3(0, 0, 7f),out RaycastHit hitInfo)){
            Debug.Log("Hit object : "+ hitInfo.collider.gameObject.name);

            lineRend.SetPosition(0, transform.position);
            lineRend.SetPosition(1, hitInfo.point);
            //rayParticle.transform.position = lineRend.GetPosition(1);
            //rayParticle.GetComponent<ParticleSystem>().Play();

            if (Input.GetKeyDown(KeyCode.H)){
                if (!isHooked){
                    isHooked = true;
                    //CharacterJoint tempCHJ = hitInfo.collider.gameObject.AddComponent<CharacterJoint>();
                    //tempCHJ.connectedBody = rb;
                    //grappledGO = tempCHJ.gameObject;
                    rb.AddForceAtPosition(Vector3.forward * 100f, hitInfo.point, ForceMode.Impulse);
                }
                else{
                    isHooked = false;
                    //Destroy(grappledGO.GetComponent<CharacterJoint>());
                    grappledGO = null;
                }
            }
        }
        else{
            if (grappledGO != null){
                isHooked = false;
                //Destroy(grappledGO.GetComponent<CharacterJoint>());
                grappledGO = null;
            }

            lineRend.SetPosition(0, transform.position);
            lineRend.SetPosition(1, transform.position + new Vector3(0, 0, 1f));
            //rayParticle.GetComponent<ParticleSystem>().Stop();
            //rayParticle.transform.position = new Vector3(100, 100, 100);
        }
    }
}
