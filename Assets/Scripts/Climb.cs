using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Climb : MonoBehaviour
{
    private CharacterController characterController;

    private ActionBasedContinuousMoveProvider continousMovement;

    private List<ActionBasedController> climbingHands = new List<ActionBasedController>();

    private Dictionary<ActionBasedController, Vector3> previousPositions = new Dictionary<ActionBasedController, Vector3>();

    private Vector3 currentVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        continousMovement = GetComponent<ActionBasedContinuousMoveProvider>();
    }

    void FixedUpdate()
    {
        foreach (ActionBasedController hand in climbingHands)
        {
            if (hand)
            {
                continousMovement.enabled = false;
                ClimbLadder(hand);
            }
        }

        if (climbingHands.Count == 0)
        {
            continousMovement.enabled = true;
        }
    }


    public void GrabKnob(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.CompareTag("Climbable"))
        {
            ActionBasedController hand = args.interactorObject.transform.gameObject.GetComponent<ActionBasedController>();
            climbingHands.Add(hand);

            previousPositions.Add(hand, hand.positionAction.action.ReadValue<Vector3>());
        }
    }

    public void ReleaseKnob(SelectExitEventArgs args)
    {
        if (args.interactableObject.transform.CompareTag("Climbable"))
        {
            var hand = climbingHands.Find(x => x.name == args.interactorObject.transform.gameObject.name);

            //           foreach(ActionBasedController hand in climbingHands)
            //            {
            //                if(hand.name == args.interactorObject.transform.gameObject.name)
            //                {
            //                    climbingHands.Remove(hand);
            //                    previousPositions.Remove(hand);
            //                }
            //            }

            if (hand)
            {
                climbingHands.Remove(hand);
                previousPositions.Remove(hand);
            }
        }
    }

    public void ClimbLadder(ActionBasedController hand)
    {
        if (previousPositions.TryGetValue(hand, out Vector3 previousPos))
        {
            Vector3 currentPos = hand.positionAction.action.ReadValue<Vector3>();

            currentVelocity = (currentPos - previousPos) / Time.fixedDeltaTime;
            characterController.Move(transform.rotation * -currentVelocity * Time.fixedDeltaTime);
            previousPositions[hand] = currentPos;
        }
    }
}
