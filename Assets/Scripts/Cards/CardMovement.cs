using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class CardMovement : MonoBehaviour {

        [Range(1, 20)]
        public float movementSpeed;

        [Range(0.01f, 0.1f)]
        public float yGrabPosition;

        public float inertia;

        public float gravity;

        Card card;
        Rigidbody rb;

        Vector3 deltaSpeedPosition;
        Vector3 speedVec;

        bool grabbed;

        private void Awake()
        {
            card    = GetComponent<Card>();
            rb      = GetComponent<Rigidbody>();

            grabbed = false;

            Physics.gravity = Vector3.down * gravity;
        }

        private void Update()
        {
            if (card.grabbed)
            {
                if (!grabbed) // grab just started
                {
                    rb.isKinematic = true;
                    grabbed = true;
                    StartCoroutine(RecordSpeedCO());
                }

                transform.position = InputManager.instance.GetPosition();

            }
            else
            {
                if (grabbed) // grab just finished
                {
                    grabbed = false;
                    rb.isKinematic = false;
                    rb.AddForce(speedVec * inertia);
                }
            }
        }

        IEnumerator RecordSpeedCO()
        {
            Vector2 oldPos = InputManager.instance.GetPosition();
            Vector2 newPos = InputManager.instance.GetPosition();

            while (grabbed)
            {
                newPos = InputManager.instance.GetPosition();
                deltaSpeedPosition = newPos - oldPos;
                oldPos = newPos;

                speedVec.Set(deltaSpeedPosition.x, deltaSpeedPosition.y, 0);

                yield return new WaitForSeconds(0.05f);
            }

        }

    }
}
