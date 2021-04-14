using UnityEngine;
public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;

    bool isfocus = false;

    Transform player;

    bool hasInteracted = false;


    public virtual void Interact()
    {
        // this method is meant to be overwritten ex: dffent npc or enemies
   
        Debug.Log("Interacting with" + transform.name);


    }


     void Update()
    {
         if (isfocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                //Reminder to my self
                // took me couple of hours to solve this. must update void update for interact, that we set above for all interactable items override
                Interact();
                hasInteracted = true;
            }
        }
    }


    public void OnFocused (Transform playerTransform)
    {
        isfocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void DeFocused ()
    {
        isfocus = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {

        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
