using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class VD : MonoBehaviour {
    public static readonly int[] ADD_POINT = { -1,1,3 };
    public enum TIMING_EVALUATION { BAD = 0, NICE = 1, EXCELLENT = 2 } //タイミング
    public static readonly string[] TIMING_EVALUATION_TEXT = { "ギリギリジャナーイ","ソコソコギリギリ！","ナイスギリギリ！" };
    public enum SCENE_STATE{ READY=0,GAME=1,GAMEOVER=2}
}