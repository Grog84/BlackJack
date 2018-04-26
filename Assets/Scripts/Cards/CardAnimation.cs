﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class CardAnimation : MonoBehaviour
    {
        enum FaceDirection { UP, DOWN }

        FaceDirection currentFace;

        [Range(0.2f, 2f)]
        public float flippingTime;
        [Range(0.2f, 2f)]
        public float anchorTime;

        Quaternion faceDownRotation;
        Quaternion faceUpRotation;

        private void Start()
        {
            faceDownRotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            faceUpRotation = Quaternion.Euler(new Vector3(90, 0, 0));

            currentFace = FaceDirection.DOWN;
        }

        IEnumerator FlipCO()
        {
            float timer = 0;
            Quaternion startingRotation = transform.rotation;

            Quaternion targetRotation = new Quaternion();
            if (currentFace == FaceDirection.DOWN)
            { targetRotation = faceDownRotation; }
            else if (currentFace == FaceDirection.UP)
            { targetRotation = faceUpRotation; }


            while (timer < flippingTime)
            {
                timer += Time.deltaTime;
                transform.rotation = Quaternion.Lerp(startingRotation, targetRotation, timer / flippingTime);
                yield return new WaitForEndOfFrame();
            }
        }

        IEnumerator ApproachPositionCO(Vector3 targetPosition)
        {
            float timer = 0;
            Vector3 startingPosition = transform.position;

            StartCoroutine(FlipCO());

            while (timer < anchorTime)
            {
                timer += Time.deltaTime;
                transform.position = Vector3.Lerp(startingPosition, targetPosition, timer / anchorTime);
                yield return new WaitForEndOfFrame();
            }
        }

        public void AnchorToPosition(Vector3 position)
        {
            StartCoroutine(ApproachPositionCO(position));
        }
    }
}