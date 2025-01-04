using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform player; // The player (or camera) that the canvas should look at



    void Start()
    {
        player = GameManager.Instance.player.transform;
    }
    void Update()
    {
        if (player != null)
        {
            // Make the canvas face the player
            transform.LookAt(player);

            // If you want the canvas to rotate only on the Y axis, to avoid tilting:
            Vector3 direction = player.position - transform.position;
            direction.y = 0; // Lock the Y axis
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
