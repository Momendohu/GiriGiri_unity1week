using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class Yokusou : MonoBehaviour {
    //=============================================================
    private GameObject water;
    private Animator _animator;
    private SoundManager SMBGM;
    private SoundManager SMSE;
    private Manager manager;

    private Vector3 startPos;
    private Vector3 goalPos;
    private bool onceFlag;

    public float PourSpeed;
    public int Patturn;

    //=============================================================
    private void Init () {
        CRef();

        startPos = transform.position + new Vector3(0,-3.2f,0);
        goalPos = transform.position + new Vector3(0,0.2f,0);

        water.transform.position = startPos;
    }

    //=============================================================
    private void CRef () {
        water = transform.Find("Outside/Mask/Water").gameObject;
        _animator = GetComponent<Animator>();
        SMBGM = GameObject.Find("SMBGM").GetComponent<SoundManager>();
        SMSE = GameObject.Find("SMSE").GetComponent<SoundManager>();
        manager = GameObject.Find("Manager").GetComponent<Manager>();
    }

    //=============================================================
    private void Awake () {
        Init();
    }

    private void Start () {
        SMSE.TriggerSE(0);

        //パターンによって細かい特徴を変化させる
        if(Patturn == 1) {
            SMSE.TriggerSE(3);
            transform.Find("Outside/Cat").gameObject.SetActive(true);

        }
    }

    private void Update () {
        if(onceFlag == false) {
            if(_animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Prepare")) {
                if(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f) {
                    _animator.SetBool("IsPrepared",true);
                    SMSE.TriggerSE(1);
                    StartCoroutine(PourWater());
                    onceFlag = true;
                }
            }
        }
    }

    //=============================================================
    //水を注ぐ
    private IEnumerator PourWater () {
        float time = 0;
        SMBGM.Trigger(1,true);

        while(true) {
            time += Time.deltaTime * PourSpeed;
            if(manager.IsPushKey) {
                manager.AddGirigiripoint(EvaluateTiming(time)); //ポイントを加算
                SuccessRemove();
                break;
            }

            if(time >= 1) {
                FailureRemove();
                break;
            }

            water.transform.position = Vector3.Lerp(startPos,goalPos,time);

            yield return null;
        }
        yield break;
    }

    //=============================================================
    //成功時オブジェクトを削除するときの処理
    private void SuccessRemove () {
        _animator.SetBool("IsPoured",true);
        Destroy(this.gameObject,0.5f);
        SMBGM.StopMusic(1);
        SMSE.TriggerSE(2);

        manager.Count++;
    }

    //=============================================================
    //失敗時オブジェクトを削除するときの処理
    private void FailureRemove () {
        _animator.SetBool("IsFailure",true);
        Destroy(this.gameObject,0.5f);
        SMBGM.StopMusic(1);
        SMSE.TriggerSE(5);

        manager.Hp--;
    }

    //=============================================================
    //タイミングを評価する
    public int EvaluateTiming (float time) {
        if(time <= 0.6f) {
            return (int)VD.TIMING_EVALUATION.BAD;
        }

        if(time <= 0.85f) {
            return (int)VD.TIMING_EVALUATION.NICE;
        }

        if(time <= 1f + 1f) {
            return (int)VD.TIMING_EVALUATION.EXCELLENT;
        }

        return -1;
    }
}