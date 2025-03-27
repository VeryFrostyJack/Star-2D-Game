using UnityEngine;

public class StarCollision : MonoBehaviour
{
    

    

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Player") {
            Debug.Log("enter");
            gameObject.SetActive(false);
        }
    }

    
}
