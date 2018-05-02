/*
 * This component is in charge of the scripted movements of a card.
 * It is used when a card is anchored to a player area or is moving back into the deck
 * 
 * */

using System.Collections;
using UnityEngine;

namespace Cards
{
    public class CardAnimation : MonoBehaviour
    {
        enum FaceDirection { UP, DOWN }

        Card card;

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

            card = GetComponent<Card>();
        }

        IEnumerator FlipCO()
        {
            float timer = 0;
            Quaternion startingRotation = transform.rotation;

            Quaternion targetRotation = new Quaternion();
            if (currentFace == FaceDirection.DOWN)
            { targetRotation = faceUpRotation; }
            else if (currentFace == FaceDirection.UP)
            { targetRotation = faceDownRotation; }


            while (timer < flippingTime)
            {
                timer += Time.deltaTime;
                transform.rotation = Quaternion.Lerp(startingRotation, targetRotation, timer / flippingTime);
                yield return new WaitForEndOfFrame();
            }
        }

        IEnumerator FlipDownCO()
        {
            float timer = 0;
            Quaternion startingRotation = transform.rotation;
            Quaternion targetRotation = faceDownRotation;
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

            while (timer < anchorTime)
            {
                timer += Time.deltaTime;
                transform.position = Vector3.Lerp(startingPosition, targetPosition, timer / anchorTime);
                yield return new WaitForEndOfFrame();
            }
            card.animating = false;
            card.locked = true;
        }

        public void AnchorToPosition(Vector3 position)
        {
            card.animating = true;
            StartCoroutine(FlipCO());
            StartCoroutine(ApproachPositionCO(position));
            card.locked = true;
        }

        public void AnchorToDeckPosition(Vector3 position)
        {
            card.animating = true;
            GetComponent<Rigidbody>().isKinematic = true;
            StartCoroutine(FlipDownCO());
            StartCoroutine(ApproachPositionCO(position));
            card.locked = true;
        }
    }
}