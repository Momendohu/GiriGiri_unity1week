using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {
    //=============================================================
    private SoundManager SMBGM;
    private SoundManager SMSE;
    private GameObject evaluateText;
    private GameObject addGirigiripoint;

    private GameObject hpImageOriginal;
    private GameObject[] hpImage = new GameObject[3];

    private GameObject titleText;
    private GameObject gameoverText;
    private GameObject sousasetumeiText;

    private float gameOverWaitTime; //キー入力の重なりを阻止するため

    public bool IsPushKey;
    public int Point;
    public int Hp;
    public int Count;

    public int SceneState;

    //=============================================================
    private void Init () {
        CRef();
        Point = 0;
        Hp = 3;
        Count = 0;
        SceneState = (int)VD.SCENE_STATE.READY;
    }

    //=============================================================
    private void CRef () {
        SMBGM = GameObject.Find("SMBGM").GetComponent<SoundManager>();
        SMSE = GameObject.Find("SMSE").GetComponent<SoundManager>();
        evaluateText = Resources.Load("EvaluateText") as GameObject;
        addGirigiripoint = Resources.Load("AddGirigiripoint") as GameObject;

        hpImageOriginal = Resources.Load("HpImage") as GameObject;
        hpImage[0] = CreateHpImage(new Vector3(220,250,0));
        hpImage[1] = CreateHpImage(new Vector3(280,250,0));
        hpImage[2] = CreateHpImage(new Vector3(340,250,0));

        titleText = GameObject.Find("Canvas/Title");
        gameoverText = GameObject.Find("Canvas/Gameover");
        sousasetumeiText = GameObject.Find("Canvas/SousasetumeiText");
    }

    //=============================================================
    private void Awake () {
        Init();

    }

    private void Start () {
        gameoverText.SetActive(false);
        sousasetumeiText.SetActive(false);
    }

    private void Update () {
        /*if(Input.GetKeyDown(KeyCode.O)) {
            ScreenCapture.CaptureScreenshot("Screenshot2.png");
        }*/

        switch(SceneState) {
            case (int)VD.SCENE_STATE.READY:
            if(Input.GetKeyDown(KeyCode.Space)) {
                SceneState = (int)VD.SCENE_STATE.GAME;
                SMBGM.Trigger(0,true);
                titleText.SetActive(false);
                sousasetumeiText.SetActive(true);
            }
            break;

            case (int)VD.SCENE_STATE.GAME:
            if(Input.GetKeyDown(KeyCode.Space)) {
                IsPushKey = true;
            } else {
                IsPushKey = false;
            }

            //体力が0になったら
            if(Hp <= 0) {
                SceneState = (int)VD.SCENE_STATE.GAMEOVER;
                gameoverText.SetActive(true);
                sousasetumeiText.SetActive(false);
                StartCoroutine(Gameover());
            }
            break;

            case (int)VD.SCENE_STATE.GAMEOVER:
            gameOverWaitTime += Time.deltaTime;
            if(gameOverWaitTime >= 0.3f) {
                if(Input.GetKeyDown(KeyCode.Space)) {
                    SceneManager.LoadScene("Main");
                }
            }
            break;
        }

        switch(Hp) {
            case 0:
            hpImage[0].SetActive(false);
            hpImage[1].SetActive(false);
            hpImage[2].SetActive(false);
            break;

            case 1:
            hpImage[0].SetActive(true);
            hpImage[1].SetActive(false);
            hpImage[2].SetActive(false);
            break;

            case 2:
            hpImage[0].SetActive(true);
            hpImage[1].SetActive(true);
            hpImage[2].SetActive(false);
            break;

            case 3:
            hpImage[0].SetActive(true);
            hpImage[1].SetActive(true);
            hpImage[2].SetActive(true);
            break;

            default:
            break;
        }
    }

    //=============================================================
    private void CreateEvaluateText (int evaluation) {
        GameObject obj = Instantiate(evaluateText) as GameObject;
        obj.transform.SetParent(GameObject.Find("Canvas").transform,false);
        obj.transform.SetAsLastSibling();

        obj.GetComponent<Text>().text = VD.TIMING_EVALUATION_TEXT[evaluation];
    }

    //=============================================================
    private void CreateAddGirigiripoint (int evaluation) {
        GameObject obj = Instantiate(addGirigiripoint) as GameObject;
        obj.transform.SetParent(GameObject.Find("Canvas").transform,false);
        obj.transform.SetAsLastSibling();

        string str = "";
        if(evaluation == 0) {
            str = "" + VD.ADD_POINT[evaluation];
        } else {
            str = "+" + VD.ADD_POINT[evaluation];
        }

        obj.GetComponent<Text>().text = str;
    }

    //=============================================================
    private GameObject CreateHpImage (Vector3 vec) {
        GameObject obj = Instantiate(hpImageOriginal) as GameObject;
        obj.transform.SetParent(GameObject.Find("Canvas").transform,false);
        obj.transform.SetAsLastSibling();
        obj.GetComponent<RectTransform>().localPosition = vec;

        return obj;
    }

    //=============================================================
    //ギリギリポイントを加算
    //evaluation 評価
    public void AddGirigiripoint (int evaluation) {
        CreateEvaluateText(evaluation);
        CreateAddGirigiripoint(evaluation);
        Point += VD.ADD_POINT[evaluation];
    }

    //=============================================================
    //ゲームオーバー時の演出
    private IEnumerator Gameover () {

        SMSE.TriggerSE(4);
        yield return new WaitForSeconds(0.1f);
        SMSE.TriggerSE(4);
        yield return new WaitForSeconds(0.1f);
        SMSE.TriggerSE(4);
        yield return new WaitForSeconds(0.1f);
        SMSE.TriggerSE(4);
        yield return new WaitForSeconds(0.1f);
        SMSE.TriggerSE(4);
        yield return new WaitForSeconds(0.1f);
        SMSE.TriggerSE(4);
        yield return new WaitForSeconds(0.1f);
        SMSE.TriggerSE(4);
        yield return new WaitForSeconds(0.1f);
        SMSE.TriggerSE(4);
        yield return new WaitForSeconds(0.1f);
    }
}