//======= Copyright (c) Valve Corporation, All rights reserved. ===============

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
        private float zValue;

        private float xRequested;
        private float zRequested;

        private bool processingRequest = false;

        private void Start()
        {
            xValue = parentPlatform.transform.position.x;
            zValue = parentPlatform.transform.position.z;
        }

        private void Update()
        {
            
            /*float xDistance = */

            if (processingRequest)
            {
                xValue = parentPlatform.transform.position.x;


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

/*        private void ColorSelf(Color newColor)
        {
            Renderer[] renderers = this.GetComponentsInChildren<Renderer>();
            for (int rendererIndex = 0; rendererIndex < renderers.Length; rendererIndex++)
            {
                renderers[rendererIndex].material.color = newColor;
            }
        }*/

        private void movePlatform()
        {
            if (processingRequest == false)
            {
                processingRequest = true;

                if (gameObject.CompareTag("north"))
                {
                    zRequested = zValue + 10;
                }
                else if (gameObject.CompareTag("east"))
                {
                    xRequested = xValue + 10;
                }
                else if (gameObject.CompareTag("south"))
                {
                    zRequested = zValue - 10;
                }
                else if (gameObject.CompareTag("west"))
                {
                    xRequested = xValue - 10;
                }
            }
        }
    }
}