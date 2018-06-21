using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class AddGirigiripoint : MonoBehaviour {
    //=============================================================
    private Animator _animator;

    //=============================================================
    private void Init () {
        CRef();
    }

    //=============================================================
    private void CRef () {
        _animator = GetComponent<Animator>();
    }

    //=============================================================
    private void Awake () {
        Init();
    }

    private void Start () {

    }

    private void Update () {
        if(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f) {
            Destroy(this.gameObject,0.1f);
        }
    }
}