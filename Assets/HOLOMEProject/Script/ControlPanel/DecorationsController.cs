using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationsController : MonoBehaviour
{

    /// <summary>
    ///  TODO:読み込んでくるモデルによってparentObjectが変わるため後ほど適用させる
    ///     下記parentNameの "MiiVerGhost"部分を選んだモデル名が入るようにする
    /// </summary>
    public string parentName = "MiiVerGhost";
    public GameObject parentObject;
    public GameObject neckObject;
    public GameObject headObject;
    public GameObject faceObject;
    public String[] necklist = new[]{"bell","ribon","muffler","apron","cape"};
    public String[] headlist = new[]{"hat","tiara","devilHone","cap","triangleHood"};
    public String[] facelist = new[]{"beard","glasses","amulet","sanGlasses","eyepatch"};

    public void Bell(){
        parentObject = GameObject.Find(parentName);
        for(int i = 0; i < necklist.Length; i++){
            neckObject = parentObject.transform.Find(necklist[i]).gameObject;
            if(necklist[i] == "bell")
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
        parentObject = GameObject.Find(parentName);
        for(int i = 0; i < necklist.Length; i++){
            neckObject = parentObject.transform.Find(necklist[i]).gameObject;
            if(necklist[i] == "ribon")
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
        parentObject = GameObject.Find(parentName);
        for(int i = 0; i < necklist.Length; i++){
            neckObject = parentObject.transform.Find(necklist[i]).gameObject;
            if(necklist[i] == "muffler")
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
        parentObject = GameObject.Find(parentName);
        for(int i = 0; i < necklist.Length; i++){
            neckObject = parentObject.transform.Find(necklist[i]).gameObject;
            if(necklist[i] == "apron")
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
        parentObject = GameObject.Find(parentName);
        for(int i = 0; i < necklist.Length; i++){
            neckObject = parentObject.transform.Find(necklist[i]).gameObject;
            if(necklist[i] == "cape")
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
        parentObject = GameObject.Find(parentName);
        for(int i = 0; i < headlist.Length; i++){
            headObject = parentObject.transform.Find(headlist[i]).gameObject;
            if(headlist[i] == "hat")
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
        parentObject = GameObject.Find(parentName);
        for(int i = 0; i < headlist.Length; i++){
            headObject = parentObject.transform.Find(headlist[i]).gameObject;
            if(headlist[i] == "tiara")
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
        parentObject = GameObject.Find(parentName);
        for(int i = 0; i < headlist.Length; i++){
            headObject = parentObject.transform.Find(headlist[i]).gameObject;
            if(headlist[i] == "devilHone")
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
        parentObject = GameObject.Find(parentName);
        for(int i = 0; i < headlist.Length; i++){
            headObject = parentObject.transform.Find(headlist[i]).gameObject;
            if(headlist[i] == "cap")
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
        parentObject = GameObject.Find(parentName);
        for(int i = 0; i < headlist.Length; i++){
            headObject = parentObject.transform.Find(headlist[i]).gameObject;
            if(headlist[i] == "triangleHood")
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
        parentObject = GameObject.Find(parentName);
        for(int i = 0; i < facelist.Length; i++){
            faceObject = parentObject.transform.Find(facelist[i]).gameObject;
            if(facelist[i] == "beard")
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
         parentObject = GameObject.Find(parentName);
        for(int i = 0; i < facelist.Length; i++){
            faceObject = parentObject.transform.Find(facelist[i]).gameObject;
            if(facelist[i] == "glasses")
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
        parentObject = GameObject.Find(parentName);
        for(int i = 0; i < facelist.Length; i++){
            faceObject = parentObject.transform.Find(facelist[i]).gameObject;
            if(facelist[i] == "amulet")
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
        parentObject = GameObject.Find(parentName);
        for(int i = 0; i < facelist.Length; i++){
            faceObject = parentObject.transform.Find(facelist[i]).gameObject;
            if(facelist[i] == "sanGlasses")
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
        parentObject = GameObject.Find(parentName);
        for(int i = 0; i < facelist.Length; i++){
            faceObject = parentObject.transform.Find(facelist[i]).gameObject;
            if(facelist[i] == "eyepatch")
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
