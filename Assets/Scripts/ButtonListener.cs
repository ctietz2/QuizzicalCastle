using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Valve.VR.InteractionSystem.Sample
{
    public class ButtonExample : MonoBehaviour
    {
        public HoverButton westHoverButton;
        public HoverButton eastHoverButton;
        public HoverButton southHoverButton;
        public HoverButton northHoverButton;

        public float speed = 5.0f;

        private float xValue;
        private float yValue;
        private float zValue;

        private float xRequested;
        private float zRequested;

        private Vector3 direction;

        private bool processingRequest = false;

        private void Start()
        {
            westHoverButton.onButtonDown.AddListener(OnWestButtonDown);
            eastHoverButton.onButtonDown.AddListener(OnEastButtonDown);
            southHoverButton.onButtonDown.AddListener(OnSouthButtonDown);
            northHoverButton.onButtonDown.AddListener(OnNorthButtonDown);

            xValue = this.transform.position.x;
            yValue = this.transform.position.y;
            zValue = this.transform.position.z;
        }

        private void Update()
        {

            if (processingRequest == true)
            {
                float xMin = Mathf.Min(xValue, xRequested);
                float xMax = Mathf.Max(xValue, xRequested);
                float zMin = Mathf.Min(zValue, zRequested);
                float zMax = Mathf.Max(zValue, zRequested);

                float xNew = Mathf.Clamp(this.transform.position.x + direction.x * speed * Time.deltaTime, xMin, xMax);
                float zNew = Mathf.Clamp(this.transform.position.z + direction.z * speed * Time.deltaTime, zMin, zMax);

                transform.position = new Vector3(xNew, yValue, zNew);

                if ((xNew == xMin || xNew == xMax) && (zNew == zMin || zNew == zMax))
                {
                    processingRequest = false;
                    xValue = this.transform.position.x;
                    zValue = this.transform.position.z;
                }

            }
        }

        private void OnWestButtonDown(Hand hand)
        {
            if (processingRequest == false)
            {
                processingRequest = true;
                movePlatform("west");
            }
        }

        private void OnEastButtonDown(Hand hand)
        {
            if (processingRequest == false)
            {
                processingRequest = true;
                movePlatform("east");
            }
        }

        private void OnSouthButtonDown(Hand hand)
        {
            if (processingRequest == false)
            {
                processingRequest = true;
                movePlatform("south");
            }
        }

        private void OnNorthButtonDown(Hand hand)
        {
            if (processingRequest == false)
            {
                processingRequest = true;
                movePlatform("north");
            }
        }

        private void movePlatform(string buttonPressed)
        {
            if (string.Equals(buttonPressed, "north"))
            {
                direction = new Vector3(0, 0, 1);
                (xRequested, zRequested) = haltPoint(direction);
            }
            else if (string.Equals(buttonPressed, "east"))
            {
                direction = new Vector3(1, 0, 0);
                (xRequested, zRequested) = haltPoint(direction);
            }
            else if (string.Equals(buttonPressed, "south"))
            {
                direction = new Vector3(0, 0, -1);
                (xRequested, zRequested) = haltPoint(direction);
            }
            else if (string.Equals(buttonPressed, "west"))
            {
                direction = new Vector3(-1, 0, 0);
                (xRequested, zRequested) = haltPoint(direction);
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
                Debug.Log("Before x:" + xNew + "; y:" + yValue + "; z:" + zNew);
                newLocation = (directionality * 10) + new Vector3(xNew, yValue, zNew);
                Debug.Log("Probe x:" + newLocation.x + "; y:" + newLocation.y + "; z:" + newLocation.z);
                Collider[] intersecting = whatObjectsHere(newLocation);
                Debug.Log(intersecting.ToString());
                string moveType = "invalid";
                foreach (Collider collider in intersecting)
                {
                    if (collider.gameObject.name.Contains("ValidCube"))
                    {
                        moveType = collider.gameObject.tag;
                        Debug.Log(moveType);
                    }
                }
                if (string.Equals(moveType, "validMove"))
                {
                    xNew = newLocation.x;
                    zNew = newLocation.z;
                }
                else if (string.Equals(moveType, "snowMove"))
                {
                    validMove = false;
                    xNew = newLocation.x;
                    zNew = newLocation.z;
                }
                else if (string.Equals(moveType, "finishMove")) // Add logic to "unbuckle" player
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
            Debug.Log("Final x:" + xNew + "; y:" + yValue + "; z:" + zNew);
            return (xNew, zNew);
        }

        private Collider[] whatObjectsHere(Vector3 position)
        {
            Collider[] intersecting = Physics.OverlapSphere(position, 5.0f);
            return intersecting;
        }
    }
}