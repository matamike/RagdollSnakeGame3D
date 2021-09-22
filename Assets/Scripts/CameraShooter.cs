using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShooter : MonoBehaviour{
    private Camera cam;
    private Vector3 camDir;
    public GameObject goHolder;
    void Start(){
        goHolder = null;
        camDir = transform.rotation * transform.position ;
        cam = GetComponent<Camera>();    
    }

    void Update(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        

        Debug.DrawRay(transform.position, ray.direction * 40f, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 40f)){
            if (goHolder == null){
                goHolder = GameObject.CreatePrimitive(PrimitiveType.Cube);
                goHolder.AddComponent<Light>();
                
            }
            else
            {
                goHolder.transform.position = hitInfo.point;
                goHolder.GetComponent<Light>().color = Color.yellow;
                goHolder.GetComponent<Collider>().enabled = false;
            }

            if (Input.GetMouseButton(0) && goHolder!=null){
                if (hitInfo.collider.gameObject.TryGetComponent<Rigidbody>(out Rigidbody component)){
                    //Assisted Aim
                    //if(hitInfo.collider.gameObject.transform.position!= goHolder.transform.position)

                    goHolder.transform.position = hitInfo.collider.gameObject.transform.position;

                    if(!goHolder.TryGetComponent<CharacterJoint>(out CharacterJoint chJoint))goHolder.AddComponent<CharacterJoint>();
                    goHolder.GetComponent<CharacterJoint>().connectedBody = GameObject.Find("Player").GetComponent<Rigidbody>();
                }
            }
            else{
                if (goHolder.TryGetComponent<CharacterJoint>(out CharacterJoint chJoint))
                {
                    chJoint.connectedBody = null;
                    
                }
                Destroy(goHolder);
            }

            
        }
        else{
            if (goHolder != null) DestroyImmediate(goHolder);
        }

    }
}
