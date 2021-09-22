using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCapture : MonoBehaviour{
    public string pathName;
    public GameObject targetObject;
    public int count = 0;
    public bool isRecording = false;
    private AnimationClip saveClip;

    void Start(){
        saveClip = null;
        pathName = "Assets/Animations/MyAnim" + count.ToString() + ".anim";
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.R)){
            isRecording = !isRecording;
        }

        if (isRecording){
            //recordclip
            Debug.Log("Recording Animation!");
        }
        else{
            Debug.Log("Stopped Recording Animation!");
            //saveclip
        }
    }
}
