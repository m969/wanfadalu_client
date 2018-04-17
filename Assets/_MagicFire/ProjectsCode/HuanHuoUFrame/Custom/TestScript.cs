using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using System;

public class TestScript : MonoBehaviour
{
    [SerializeField]
    private Image _headBar;

    // Use this for initialization
    void Start () {
        Debug.Log("TestScript:Start");
        //_headBar.OnBeginDragAsObservable().Subscribe(evt =>
        //{
        //    Debug.Log("TestScript:OnBeginDragAsObservable");
        //});
    }

    // Update is called once per frame
    void Update () {
		
	}
}
