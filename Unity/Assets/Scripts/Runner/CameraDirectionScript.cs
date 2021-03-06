﻿using UnityEngine;
using System.Collections;

/*Classe utilisé pour bouger la camera du runner mais qui n'est finalement pas utilisée*/
public class CameraDirectionScript : MonoBehaviour {

    [SerializeField]
    float speedH = 2.0f;

    [SerializeField]
    float speedV = 2.0f;

    [SerializeField]
    float maxPitch = 40;

    [SerializeField]
    float minPitch = 15;

    [SerializeField]
    float maxYaw = 50;

    [SerializeField]
    float minYaw = -50;

    [SerializeField]
    Transform cameraTransform;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private bool mouseUp = false,mouseDown = false, mouseRight = false,mouseLeft = false;
    private float savMouseY;
    private float savMouseX;

    void Start()
    {

    }

    void Update()
    {

        if (mouseUp && Input.mousePosition.y<savMouseY)
        {
            mouseUp = false;
        }
        else if (pitch < minPitch && !mouseUp)
        {
            pitch = minPitch;
            mouseUp = true;
            savMouseY = Input.mousePosition.y;
        }
        else if (mouseDown && Input.mousePosition.y>savMouseY)
        {
            mouseDown = false;
        }
        else if (pitch > maxPitch && !mouseDown)
        {
            pitch = maxPitch;
            mouseDown = true;
            savMouseY = Input.mousePosition.y;
        }

        if (mouseLeft && Input.mousePosition.x > savMouseX)
        {
            mouseLeft = false;
        }
        else if (yaw < minYaw && !mouseLeft)
        {
            yaw = minYaw;
            mouseLeft = true;
            savMouseX = Input.mousePosition.x;
        }
        else if(mouseRight && Input.mousePosition.x < savMouseX)
        {
            mouseRight = false;
        }
        else if (yaw > maxYaw && !mouseRight)
        {
            yaw = maxYaw;
            mouseRight = true;
            savMouseX = Input.mousePosition.x;
        }


        if(!mouseLeft && !mouseRight)
            yaw += speedH * Input.GetAxis("Mouse X");
        if (!mouseUp && !mouseDown)
            pitch -= speedV * Input.GetAxis("Mouse Y");



        cameraTransform.eulerAngles = Vector3.right*pitch+Vector3.up* yaw+Vector3.forward* 0.0f;
    }
}
