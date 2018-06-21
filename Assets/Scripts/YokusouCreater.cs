using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class YokusouCreater : MonoBehaviour {
    //=============================================================
    private Manager manager;
    private GameObject pref;

    //=============================================================
    private void Init () {
        CRef();
    }

    //=============================================================
    private void CRef () {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        pref = Resources.Load("Yokusou") as GameObject;
    }

    //=============================================================
    private void Awake () {
        Init();
    }

    private void Start () {

    }

    private void Update () {
        CreateYokusou();
    }

    //=============================================================
    //浴槽を生成する
    private void CreateYokusou () {
        if(manager.SceneState == (int)VD.SCENE_STATE.GAME) {
            if(GameObject.FindGameObjectsWithTag("Yokusou").Length == 0) {
                GameObject obj = Instantiate(pref);
                obj.GetComponent<Yokusou>().Patturn = (int)Random.Range(0,2);
                obj.GetComponent<Yokusou>().PourSpeed = Random.Range(0.2f,1.5f) * ((float)manager.Count / 20f + 1);
            }
        }
    }
}