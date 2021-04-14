using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerCon : MonoBehaviour
{

    public Interactable focus;

    public LayerMask movement;

    Camera cam;

    //reference on PlayerMotor Script Component
    PlayerMotor motor;


    private void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movement))
            {
                motor.MoveToPoint(hit.point);
                // Move our player to what we hit

                //Stop focusing any objects
                RemoveFocus();
            }

        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                //Check if we hit an interactable

                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    

                    //if we did set 
                            if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }

        }
    }
        void SetFocus (Interactable newFocus)
    {

        if (newFocus != focus)

        {
            if (focus != null)
            focus.DeFocused();
            focus = newFocus;
            motor.FollowTarget(newFocus);

        }
        newFocus.OnFocused(transform);



    }

    void RemoveFocus()
    {
        if (focus != null)
        focus.DeFocused();
        focus = null;
        motor.StopFollowingTarget();
    }

}
