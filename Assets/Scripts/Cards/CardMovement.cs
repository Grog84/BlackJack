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

        [Range(1, 20)]
        public float inertia;

        Card card;
        Rigidbody rb;

        Vector3 deltaPosition;
        Vector3 oldPosition;
        Vector3 deltaSpeedPosition;
        Vector3 speedVec;

        bool grabbed;

        private void Awake()
        {
            card    = GetComponent<Card>();
            rb      = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (card.grabbed)
            {
                if (!grabbed) // grab just started
                {
                    grabbed = true;
                    oldPosition = transform.position + card.grabbingOffset;
                    StartCoroutine(RecordSpeedCO());
                }

                deltaPosition = InputManager.instance.GetPosition() - oldPosition;
                transform.Translate(deltaPosition.x * movementSpeed * Time.deltaTime, deltaPosition.y * movementSpeed * Time.deltaTime, yGrabPosition);
                oldPosition = transform.position + card.grabbingOffset;
            }
            else
            {
                if (grabbed) // grab just finished
                {
                    grabbed = false;
                    rb.AddForce(speedVec * inertia);
                }
            }
        }

        IEnumerator RecordSpeedCO()
        {
            Vector2 oldPos = oldPosition;
            Vector2 newPos = oldPosition;

            while (grabbed)
            {
                newPos = InputManager.instance.GetPosition();
                deltaSpeedPosition = newPos - oldPos;
                oldPos = newPos;

                speedVec.Set(deltaSpeedPosition.x, deltaSpeedPosition.y, 0);

                yield return new WaitForSeconds(0.1f);
            }

        }

    }
}
