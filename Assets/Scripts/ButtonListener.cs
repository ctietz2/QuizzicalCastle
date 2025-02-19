using UnityEngine;
using System.Collections;

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

            if (processingRequest)
            {

                float xNew = this.transform.position.x + direction.x * speed * Time.deltaTime;
                float zNew = this.transform.position.z + direction.z * speed * Time.deltaTime;

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

        private void OnWestButtonDown(Hand hand)
        {
            movePlatform("west");
        }

        private void OnEastButtonDown(Hand hand)
        {
            movePlatform("east");
        }

        private void OnSouthButtonDown(Hand hand)
        {
            movePlatform("south");
        }

        private void OnNorthButtonDown(Hand hand)
        {
            movePlatform("north");
        }

        private void movePlatform(string buttonPressed)
        {

            if (processingRequest == false)
            {
                processingRequest = true;

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