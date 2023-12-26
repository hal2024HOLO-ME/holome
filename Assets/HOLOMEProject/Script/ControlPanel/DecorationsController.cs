using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationsController : MonoBehaviour
{

    /// <summary>
    /// キャラクターカスタマイズ
    /// </summary>
    public string CharacterParentObjectName = new SendResult().GetResponseFileName();
    public GameObject parentObject;
    public GameObject neckObject;
    public GameObject headObject;
    public GameObject faceObject;
    public String[] necklists = new[]{"bell","ribon","muffler","apron","cape"};
    public String[] headlists = new[]{"hat","tiara","devilHone","cap","triangleHood"};
    public String[] facelists = new[]{"beard","glasses","amulet","sanGlasses","eyepatch"};

    void Start(){
        Debug.Log("SendResult().GetResponseFileName():" + new SendResult().GetResponseFileName());
        Debug.Log("CharacterParentObjectNameStart" + CharacterParentObjectName);
        for(int i = 0; i < headlists.Length; i++){
            Debug.Log(headlists[i]);
        }
    }
    public void Bell(){
        parentObject = GameObject.Find(CharacterParentObjectName);
        Debug.Log("CharacterParentObjectName" + CharacterParentObjectName);
        for (int i = 0; i < necklists.Length; i++){
            neckObject = parentObject.transform.Find(necklists[i]).gameObject;
            if(necklists[i] == "bell")
            {
                neckObject.SetActive(true);
            }
            else
            {
                neckObject.SetActive(false);
            }
        }
    }
    public void Ribon(){
        parentObject = GameObject.Find(CharacterParentObjectName);
        for(int i = 0; i < necklists.Length; i++){
            neckObject = parentObject.transform.Find(necklists[i]).gameObject;
            if(necklists[i] == "ribon")
            {
                neckObject.SetActive(true);
            }
            else
            {
                neckObject.SetActive(false);
            }
        }
    }
    public void Muffler(){
        parentObject = GameObject.Find(CharacterParentObjectName);
        for(int i = 0; i < necklists.Length; i++){
            neckObject = parentObject.transform.Find(necklists[i]).gameObject;
            if(necklists[i] == "muffler")
            {
                neckObject.SetActive(true);
            }
            else
            {
                neckObject.SetActive(false);
            }
        }
    }
    public void Apron(){
        parentObject = GameObject.Find(CharacterParentObjectName);
        for(int i = 0; i < necklists.Length; i++){
            neckObject = parentObject.transform.Find(necklists[i]).gameObject;
            if(necklists[i] == "apron")
            {
                neckObject.SetActive(true);
            }
            else
            {
                neckObject.SetActive(false);
            }
        }
    }
    public void Cape(){
        parentObject = GameObject.Find(CharacterParentObjectName);
        for(int i = 0; i < necklists.Length; i++){
            neckObject = parentObject.transform.Find(necklists[i]).gameObject;
            if(necklists[i] == "cape")
            {
                neckObject.SetActive(true);
            }
            else
            {
                neckObject.SetActive(false);
            }
        }
    }
    /// <summary>
    /// 頭飾り
    /// </summary>
    public void Hat(){
        parentObject = GameObject.Find(CharacterParentObjectName);
        for(int i = 0; i < headlists.Length; i++){
            headObject = parentObject.transform.Find(headlists[i]).gameObject;
            if(headlists[i] == "hat")
            {
                headObject.SetActive(true);
            }
            else
            {
                headObject.SetActive(false);
            }
        }
    }
    public void Tiara(){
        parentObject = GameObject.Find(CharacterParentObjectName);
        for(int i = 0; i < headlists.Length; i++){
            headObject = parentObject.transform.Find(headlists[i]).gameObject;
            if(headlists[i] == "tiara")
            {
                headObject.SetActive(true);
            }
            else
            {
                headObject.SetActive(false);
            }
        }
    }
    public void DevilHone(){
        parentObject = GameObject.Find(CharacterParentObjectName);
        for(int i = 0; i < headlists.Length; i++){
            headObject = parentObject.transform.Find(headlists[i]).gameObject;
            if(headlists[i] == "devilHone")
            {
                headObject.SetActive(true);
            }
            else
            {
                headObject.SetActive(false);
            }
        }
    }
    public void Cap(){
        parentObject = GameObject.Find(CharacterParentObjectName);
        for(int i = 0; i < headlists.Length; i++){
            headObject = parentObject.transform.Find(headlists[i]).gameObject;
            if(headlists[i] == "cap")
            {
                headObject.SetActive(true);
            }
            else
            {
                headObject.SetActive(false);
            }
        }
    }
    public void TrianglarHood(){
        parentObject = GameObject.Find(CharacterParentObjectName);
        for(int i = 0; i < headlists.Length; i++){
            headObject = parentObject.transform.Find(headlists[i]).gameObject;
            if(headlists[i] == "triangleHood")
            {
                headObject.SetActive(true);
            }
            else
            {
                headObject.SetActive(false);
            }
        }
    }
    /// <summary>
    /// 顔飾り
    /// </summary>
    public void Beard(){
        parentObject = GameObject.Find(CharacterParentObjectName);
        for(int i = 0; i < facelists.Length; i++){
            faceObject = parentObject.transform.Find(facelists[i]).gameObject;
            if(facelists[i] == "beard")
            {
                faceObject.SetActive(true);
            }
            else
            {
                faceObject.SetActive(false);
            }
        }
    }

    public void Glasses(){
         parentObject = GameObject.Find(CharacterParentObjectName);
        for(int i = 0; i < facelists.Length; i++){
            faceObject = parentObject.transform.Find(facelists[i]).gameObject;
            if(facelists[i] == "glasses")
            {
                faceObject.SetActive(true);
            }
            else
            {
                faceObject.SetActive(false);
            }
        }
    }

    public void Amulet(){
        parentObject = GameObject.Find(CharacterParentObjectName);
        for(int i = 0; i < facelists.Length; i++){
            faceObject = parentObject.transform.Find(facelists[i]).gameObject;
            if(facelists[i] == "amulet")
            {
                faceObject.SetActive(true);
            }
            else
            {
                faceObject.SetActive(false);
            }
        }
    }


    public void SanGlasses(){
        parentObject = GameObject.Find(CharacterParentObjectName);
        for(int i = 0; i < facelists.Length; i++){
            faceObject = parentObject.transform.Find(facelists[i]).gameObject;
            if(facelists[i] == "sanGlasses")
            {
                faceObject.SetActive(true);
            }
            else
            {
                faceObject.SetActive(false);
            }
        }
    }

    public void Eyepatch(){
        parentObject = GameObject.Find(CharacterParentObjectName);
        for(int i = 0; i < facelists.Length; i++){
            faceObject = parentObject.transform.Find(facelists[i]).gameObject;
            if(facelists[i] == "eyepatch")
            {
                faceObject.SetActive(true);
            }
            else
            {
                faceObject.SetActive(false);
            }
        }
    }

}
