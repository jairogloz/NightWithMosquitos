﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class MosquitoController : MonoBehaviour {

    VRInteractiveItem vrIntItem;

    // min distance from us
    public float minDistance = 0.55f;

    // mosquito speed
    public float speed = 0.5f;

    // flag to keep track whether this mosquito is moving
    bool isMoving = true;

    // rigidboyd
    Rigidbody rb;

    // target
    Vector3 target;

	// Use this for initialization
	void Awake () {
        // grab vr interactive component
        vrIntItem = GetComponent<VRInteractiveItem>();

        // grab the rigidbody component
        rb = GetComponent<Rigidbody>();

        // set target
        target = Camera.main.transform.position;

        // make the moskito look at us
        transform.LookAt(target);
	}// end of Awake

    // when our game object is enabled
    private void OnEnable()
    {
        vrIntItem.OnClick += HandleClick;
    }// end of OnEnable

    // when our game object is disabled
    private void OnDisable()
    {
        vrIntItem.OnClick -= HandleClick;
    }// end of OnDisable

    // this is called when the mosquito is clicked on
    private void HandleClick()
    {
        // check that it's moving
        if (rb.isKinematic) {
            // rotate mosquito transform
            transform.Rotate(new Vector3( 0, 0, 180) );

            // disable isKinematic property
            rb.isKinematic = false;

            // set the flag to false
            isMoving = false;
        }// end of if

    }// end of HandleClick



    // Update is called once per frame
    void Update () {
        // check that we are moving
        if (isMoving) {
            // calculate distance from the target
            float distance = Vector3.Distance(transform.position, target);

            // check min distance
            if (distance <= minDistance) {
                // we stop!
                isMoving = false;
            } else {
                // calculate movement step: v = d / t, d = v * t
                float movementStep = speed * Time.deltaTime;

                // move
                transform.position = Vector3.MoveTowards(transform.position, target, movementStep);
            }// end of if/else
        }// end of if
	}// end of Update
}// end of MosquitoController
