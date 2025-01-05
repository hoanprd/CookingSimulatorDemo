using UnityEngine;

public class Holder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Square"))
        {
            Debug.Log("Ingredient added to the pan!");
            // Thêm logic nấu ăn ở đây
        }
    }
}
