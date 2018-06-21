using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Girigiripoint : MonoBehaviour {
    //=============================================================
    private Manager manager;
    private Text _text;

    //=============================================================
    private void Init () {
        CRef();
    }

    //=============================================================
    private void CRef () {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        _text = GetComponent<Text>();
    }

    //=============================================================
    private void Awake () {
        Init();
    }

    private void Start () {

    }

    private void Update () {
        _text.text = "ギリギリポイント " + manager.Point;
    }
}