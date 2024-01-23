using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationsController : MonoBehaviour
{

    /// <summary>
    /// キャラクターカスタマイズ
    /// </summary>
    public string characterParentObject1 = new SendResult().GetResponseFileName();
    public GameObject parentObject;
    public GameObject neckObject;
    public GameObject headObject;
    public GameObject faceObject;
    private string GhostStr = "Ghost";
    private string  Mii = "Mii";
    public String[] necklists = new string[5];
    public String[] headlists = new string[5];
    public String[] facelists = new string[5];

    public static string neckTargetName;
    public static string headTargetName;
    public static string faceTargetName;

    /// <summary>
    /// モデルがゴーストとノーマルで首飾りの種類が違うので判定
    /// </summary>
    void Start(){
        Debug.Log("SendResult().GetResponseFileName():" + new SendResult().GetResponseFileName());
        Debug.Log("characterParentObject1Start" + characterParentObject1);
        if(characterParentObject1.Contains(GhostStr) && !characterParentObject1.Contains(Mii)){
            necklists = new[]{"bell","ribon","scarf","apron","cape"};
            headlists = new[]{"hat","tiara","devilHone","cap","triangleHood"};
            facelists = new[]{"beard","glasses","amulet","sanGlasses","eyepatch"};
        }else{
            necklists = new[]{"bell","ribon","muffler","apron","cape"};
            headlists = new[]{"hat","tiara","devilHone","cap","triangleHood"};
            facelists = new[]{"beard","glasses","amulet","sanGlasses","eyepatch"};
        }
    }
    /// <summary>
    /// 首飾り
    /// </summary>
    public void Bell(){
        parentObject = GameObject.Find(characterParentObject1);
        Debug.Log("characterParentObject1" + characterParentObject1);
        for (int i = 0; i < necklists.Length; i++){
            neckObject = parentObject.transform.Find(necklists[i]).gameObject;
            if(necklists[i] == "bell")
            {
                neckObject.SetActive(true);
                neckTargetName = necklists[i];
            }
            else
            {
                neckObject.SetActive(false);
            }
        }
    }
    public void Ribon(){
        parentObject = GameObject.Find(characterParentObject1);
        for(int i = 0; i < necklists.Length; i++){
            neckObject = parentObject.transform.Find(necklists[i]).gameObject;
            if(necklists[i] == "ribon")
            {
                neckObject.SetActive(true);
                neckTargetName = necklists[i];
            }
            else
            {
                neckObject.SetActive(false);
            }
        }
    }
    public void Muffler(){
        parentObject = GameObject.Find(characterParentObject1);
        for(int i = 0; i < necklists.Length; i++){
            neckObject = parentObject.transform.Find(necklists[i]).gameObject;
            if(necklists[i] == "muffler" || necklists[i] == "scarf")
            {
                neckObject.SetActive(true);
                neckTargetName = necklists[i];
            }
            else
            {
                neckObject.SetActive(false);
            }
        }
    }
    public void Apron(){
        parentObject = GameObject.Find(characterParentObject1);
        for(int i = 0; i < necklists.Length; i++){
            neckObject = parentObject.transform.Find(necklists[i]).gameObject;
            if(necklists[i] == "apron")
            {
                neckObject.SetActive(true);
                neckTargetName = necklists[i];
            }
            else
            {
                neckObject.SetActive(false);
            }
        }
    }
    public void Cape(){
        parentObject = GameObject.Find(characterParentObject1);
        for(int i = 0; i < necklists.Length; i++){
            neckObject = parentObject.transform.Find(necklists[i]).gameObject;
            if(necklists[i] == "cape")
            {
                neckObject.SetActive(true);
                neckTargetName = necklists[i];
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
        parentObject = GameObject.Find(characterParentObject1);
        for(int i = 0; i < headlists.Length; i++){
            headObject = parentObject.transform.Find(headlists[i]).gameObject;
            if(headlists[i] == "hat")
            {
                headObject.SetActive(true);
                headTargetName = headlists[i];
            }
            else
            {
                headObject.SetActive(false);
            }
        }
    }
    public void Tiara(){
        parentObject = GameObject.Find(characterParentObject1);
        for(int i = 0; i < headlists.Length; i++){
            headObject = parentObject.transform.Find(headlists[i]).gameObject;
            if(headlists[i] == "tiara")
            {
                headObject.SetActive(true);
                headTargetName = headlists[i];
            }
            else
            {
                headObject.SetActive(false);
            }
        }
    }
    public void DevilHone(){
        parentObject = GameObject.Find(characterParentObject1);
        for(int i = 0; i < headlists.Length; i++){
            headObject = parentObject.transform.Find(headlists[i]).gameObject;
            if(headlists[i] == "devilHone")
            {
                headObject.SetActive(true);
                headTargetName = headlists[i];
            }
            else
            {
                headObject.SetActive(false);
            }
        }
    }
    public void Cap(){
        parentObject = GameObject.Find(characterParentObject1);
        for(int i = 0; i < headlists.Length; i++){
            headObject = parentObject.transform.Find(headlists[i]).gameObject;
            if(headlists[i] == "cap")
            {
                headObject.SetActive(true);
                headTargetName = headlists[i];
            }
            else
            {
                headObject.SetActive(false);
            }
        }
    }
    public void TrianglarHood(){
        parentObject = GameObject.Find(characterParentObject1);
        for(int i = 0; i < headlists.Length; i++){
            headObject = parentObject.transform.Find(headlists[i]).gameObject;
            if(headlists[i] == "triangleHood")
            {
                headObject.SetActive(true);
                headTargetName = headlists[i];
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
        parentObject = GameObject.Find(characterParentObject1);
        for(int i = 0; i < facelists.Length; i++){
            faceObject = parentObject.transform.Find(facelists[i]).gameObject;
            if(facelists[i] == "beard")
            {
                faceObject.SetActive(true);
                faceTargetName = facelists[i];
            }
            else
            {
                faceObject.SetActive(false);
            }
        }
    }

    public void Glasses(){
         parentObject = GameObject.Find(characterParentObject1);
        for(int i = 0; i < facelists.Length; i++){
            faceObject = parentObject.transform.Find(facelists[i]).gameObject;
            if(facelists[i] == "glasses")
            {
                faceObject.SetActive(true);
                faceTargetName = facelists[i];
            }
            else
            {
                faceObject.SetActive(false);
            }
        }
    }

    public void Amulet(){
        parentObject = GameObject.Find(characterParentObject1);
        for(int i = 0; i < facelists.Length; i++){
            faceObject = parentObject.transform.Find(facelists[i]).gameObject;
            if(facelists[i] == "amulet")
            {
                faceObject.SetActive(true);
                faceTargetName = facelists[i];
            }
            else
            {
                faceObject.SetActive(false);
            }
        }
    }


    public void SanGlasses(){
        parentObject = GameObject.Find(characterParentObject1);
        for(int i = 0; i < facelists.Length; i++){
            faceObject = parentObject.transform.Find(facelists[i]).gameObject;
            if(facelists[i] == "sanGlasses")
            {
                faceObject.SetActive(true);
                faceTargetName = facelists[i];
            }
            else
            {
                faceObject.SetActive(false);
            }
        }
    }

    public void Eyepatch(){
        parentObject = GameObject.Find(characterParentObject1);
        for(int i = 0; i < facelists.Length; i++){
            faceObject = parentObject.transform.Find(facelists[i]).gameObject;
            if(facelists[i] == "eyepatch")
            {
                faceObject.SetActive(true);
                faceTargetName = facelists[i];
            }
            else
            {
                faceObject.SetActive(false);
            }
        }
    }

}
