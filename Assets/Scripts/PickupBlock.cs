using UnityEngine;

public class PickupBlock : MonoBehaviour
{
    public Transform player;
    public Transform boxContainer;
    public Rigidbody boxRb;
    public float pickUpRange;
    private bool isHoldingItem = false;

    // Start is called before the first frame update
    void Start()
    {
        boxRb.isKinematic = true; // So block doesn't move
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        // If player isn't holding anything and presses grab button
        if (!isHoldingItem && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.G)) // https://www.youtube.com/watch?v=8kKLUsn7tcg
        {
            isHoldingItem = true;
            transform.SetParent(boxContainer);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity); // box moved to same position as parent (boxContainer) w/o rotating it
            Debug.Log("Player is holding an item");
        }

        // If player is holding something and presses grab button
        else if (isHoldingItem && Input.GetKeyDown(KeyCode.G))
        {
            isHoldingItem = false;
            transform.SetParent(null);
            Debug.Log("Player has dropped their item");
        }
    }
}
