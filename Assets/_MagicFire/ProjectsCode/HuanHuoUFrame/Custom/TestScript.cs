using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using System;

public class TestScript : MonoBehaviour
{

    // Use this for initialization
    void Start () {
        this.GetComponent<Image>().OnPointerDownAsObservable().Subscribe(evt =>
        {
            Debug.Log("TestScript OnPointerDownAsObservable");
        });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
