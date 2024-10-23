using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveHandler : MonoBehaviour
{
    public LayerMask GraveMask;
    public float InteractionRange = 0.1f;

    public Transform InteractionPoint;
    public GameObject Graves;

    private Dictionary<GameObject, string> GraveInfo = new Dictionary<GameObject, string>(); // Dictionaries are like lists but in 2d, meaning you can have a value assigned to a named key
    public ItemsHandler ItemsHandler;
    public Sprite OpenedSprite;

    public void SetGraveItem(Transform Grave)
    {
        int DropChance = Random.Range(1, 101); // Up to 101 as the last number is ignored, therefore its actually 100

        // Rough random drop rate system
        if (DropChance >= 1 && DropChance <= 20) // Money (20%)
        {
            GraveInfo.Add(Grave.gameObject, "Money"); // Assign "Money" to the GameObject
        }
        else if (DropChance >= 21 && DropChance <= 50) // Oil (30%)
        {
            GraveInfo.Add(Grave.gameObject, "Oil"); // Assign "Oil" to the GameObject
        }
        else // Nothing (50%)
        {
            GraveInfo.Add(Grave.gameObject, "Nothing"); // Assign "Nothing" to the GameObject
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Give every existing grave an item upon starting the game
        foreach (Transform Grave in Graves.transform)
        {
            SetGraveItem(Grave);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Collider2D[] HitGraves = Physics2D.OverlapCircleAll(InteractionPoint.position, InteractionRange, GraveMask); // Get all collisions within a circle that have the mask given

            foreach (Collider2D hit in HitGraves)
            {
                if (GraveInfo.ContainsKey(hit.gameObject)) // Other then checking to ensure that it exists which is kinda redundant, we use to see if its been opened, and remove it when opened
                {
                    ItemsHandler.ItemCollected(GraveInfo[hit.gameObject]); // Tell the item script that we collected whatever the grave had generated
                    hit.gameObject.GetComponent<SpriteRenderer>().sprite = OpenedSprite;
                    GraveInfo.Remove(hit.gameObject); // Remove it from the list so it can't be opened again
                    // Update sprite stuff here to update the sprite to an opened grave etc
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (!InteractionPoint) // If the transform InteractionPoint doesn't exist then we return, which ends execution of this function past line 30 which is the return line
        {
            return;
        }

        Gizmos.DrawWireSphere(InteractionPoint.position, InteractionRange);
    }
}
