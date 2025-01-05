using UnityEngine;

public class DragAndDropCircle : MonoBehaviour
{
    public int circleID;
    private bool isDragging = false, isDragOnSquare;
    private Vector3 originalPosition;
    private GameObject square; // Tham chiếu đến Square
    public GameObject circlePrefab; // Tham chiếu đến prefab của Circle

    private void Start()
    {
        // Kiểm tra nếu không có circlePrefab được chỉ định
        if (circlePrefab == null)
        {
            Debug.LogError("CirclePrefab is not assigned!");
        }

        this.GetComponent<CircleCollider2D>().enabled = true;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        originalPosition = transform.position; // Lưu vị trí ban đầu
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Giữ z = 0 trong world space
            transform.position = mousePosition;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        square = GameObject.FindWithTag("Square");

        if (square != null && square.GetComponent<DragAndDropSprite>().isCooked && isDragOnSquare)
        {
            /*// Nếu Square đã được nấu chín, đặt Circle lên Square
            transform.position = square.transform.position; // Đặt Circle lên Square
            isCircleOnSquare = true; // Đánh dấu Circle đã lên Square
            this.GetComponent<CircleCollider2D>().enabled = false; // Vô hiệu hóa Collider của Circle khi đã đặt lên Square
            Debug.Log("Circle placed on the cooked square.");*/

            // Sinh ra một bản sao của Circle tại vị trí ban đầu của Circle
            if (circlePrefab != null)
            {
                Instantiate(circlePrefab, originalPosition, Quaternion.identity); // Tạo một bản sao của Circle tại vị trí ban đầu
                Debug.Log("New Circle spawned at original position.");
            }
            DragAndDropSprite.isPutCircle = true;
            DragAndDropSprite.circleIndex = circleID;

            Destroy(gameObject);
        }
        else
        {
            // Nếu Square chưa được nấu chín hoặc Circle đã có trên Square, quay về vị trí ban đầu
            transform.position = originalPosition;
            Debug.Log("Circle not placed because Square is not cooked.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Square"))
        {
            isDragOnSquare = true;
        }
    }
}
