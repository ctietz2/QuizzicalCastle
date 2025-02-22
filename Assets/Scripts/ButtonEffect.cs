﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

namespace Valve.VR.InteractionSystem.Sample
{
    public class ButtonEffect : MonoBehaviour
    {

        public GameObject parentPlatform;

        public float speed = 5.0f;
        /*        private float zMax = 7.5f;
                private float zMin = -7.5f;*/
        /*        private int xDirection;
                private int zDirection;*/

        private float xValue;
        private float yValue;
        private float zValue;

        private float xRequested;
        private float zRequested;

        private Vector3 direction;

        private bool processingRequest = false;

        private void Start()
        {
            xValue = parentPlatform.transform.position.x;
            yValue = parentPlatform.transform.position.y;
            zValue = parentPlatform.transform.position.z;
        }

        private void Update()
        {

            if (processingRequest)
            {

                float xNew = parentPlatform.transform.position.x + direction.x * speed * Time.deltaTime;
                float zNew = parentPlatform.transform.position.z + direction.z * speed * Time.deltaTime;

                bool xCloseEnough = Mathf.Approximately(xNew, xRequested);
                bool zCloseEnough = Mathf.Approximately(zNew, zRequested);

                if (xCloseEnough)
                {
                    xNew = xRequested;
                }

                if (zCloseEnough)
                {
                    zNew = zRequested;
                }

                transform.position = new Vector3(xNew, yValue, zNew);

                if (xCloseEnough && zCloseEnough)
                {
                    processingRequest = false;
                }

            }
        }

        public void OnButtonDown(Hand fromHand)
        {
            movePlatform();
            fromHand.TriggerHapticPulse(1000);
        }

        public void OnButtonUp(Hand fromHand)
        {

        }

        private void movePlatform()
        {

            if (processingRequest == false)
            {
                processingRequest = true;

                if (gameObject.CompareTag("north"))
                {
                    direction = new Vector3(0, 0, 1);
                    (xRequested, zRequested) = haltPoint(direction);
                }
                else if (gameObject.CompareTag("east"))
                {
                    direction = new Vector3(1, 0, 0);
                    (xRequested, zRequested) = haltPoint(direction);
                }
                else if (gameObject.CompareTag("south"))
                {
                    direction = new Vector3(0, 0, -1);
                    (xRequested, zRequested) = haltPoint(direction);
                }
                else if (gameObject.CompareTag("west"))
                {
                    direction = new Vector3(-1, 0, 0);
                    (xRequested, zRequested) = haltPoint(direction);
                }
            }
        }

        private (float, float) haltPoint(Vector3 directionality)
        {
            bool validMove = true;
            float xNew = xValue;
            float zNew = zValue;
            Vector3 newLocation;

            while (validMove)
            {
                // Probe 10 units in the chosen direction for any objects. If one exists, the movement is no longer valid.
                print("Before x:" + xNew + "; y:" + yValue + "; z:" + zNew);
                newLocation = (directionality * 10) + new Vector3(xNew, yValue, zNew);
                print("Actual x:" + newLocation.x + "; y:" + newLocation.y + "; z:" + newLocation.z);
                Collider[] intersecting = whatObjectsHere(newLocation);
                print(intersecting);
                foreach (Collider collider in intersecting)
                {
                    if (collider.gameObject.CompareTag("validMove"))
                    {
                        xNew = newLocation.x;
                        zNew = newLocation.z;
                    }
                    else if (collider.gameObject.CompareTag("snowMove"))
                    {
                        validMove = false;
                        xNew = newLocation.x;
                        zNew = newLocation.z;
                    }
                    else if (collider.gameObject.CompareTag("finishMove")) // Add logic to "unbuckle" player
                    {
                        validMove = false;
                        xNew = newLocation.x;
                        zNew = newLocation.z;
                    }
                    else
                    {
                        validMove = false;
                    }
                }
            }

            return (xNew, zNew);
        }

        private Collider[] whatObjectsHere(Vector3 position)
        {
            Collider[] intersecting = Physics.OverlapSphere(position, 5.0f);
            print(intersecting);
            return intersecting;
        }
    }
}