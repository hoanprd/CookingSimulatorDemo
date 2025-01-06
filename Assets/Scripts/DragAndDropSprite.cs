using UnityEngine;

public class DragAndDropSprite : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 originalPosition;
    private bool isOverPan = false; // Kiểm tra nếu Square đang trên Pan
    private bool hasBeenDroppedInPan = false; // Tránh việc nấu lại nếu đã thả vào Pan
    private bool isOverDisk = false; // Kiểm tra Square có vào Disk chưa
    public bool isCooked = false; // Đổi thành public
    private bool isInDisk = false; // Kiểm tra Square đã vào Disk chưa
    private bool isGivenToCustomer = false; // Kiểm tra Square đã vào miệng khách hàng chưa
    public static bool isPutCircle = false;
    public static int circleIndex;

    public GameObject squarePrefab; // Tham chiếu đến prefab của Square
    public GameObject[] squareChild; // Tham chiếu đến đối tượng con của Square khi đã nấu chín

    private void Start()
    {
        // Kiểm tra nếu không có squarePrefab được chỉ định
        if (squarePrefab == null)
        {
            Debug.LogError("SquarePrefab is not assigned!");
        }
    }

    void Update()
    {
        if (isPutCircle)
        {
            isPutCircle = false;
            for (int i = 0; i < squareChild.Length; i++)
            {
                if (i == circleIndex)
                    squareChild[i].SetActive(true);
                else
                    squareChild[i].SetActive(false);
            }
        }
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

        if (isInDisk)
        {
            // Kiểm tra nếu Square đã được nấu chín trước khi cho vào Disk
            if (isCooked)
            {
                if (isOverDisk)
                {
                    transform.position = GameObject.FindWithTag("Disk").transform.position;
                    Debug.Log("Square is in disk.");
                }
                else if (isGivenToCustomer)
                {
                    // Nếu đã vào miệng khách hàng, Square sẽ bị hủy
                    Customer.eatSuccess = 1;
                    Destroy(gameObject); // Hủy Square
                    Debug.Log("Square given to the customer.");
                }
                else
                {
                    // Không làm gì nếu không thả đúng chỗ
                    transform.position = originalPosition;
                    Debug.Log("Dropped outside disk.");
                }
            }
            else
            {
                // Nếu chưa nấu chín, không cho vào Disk
                transform.position = originalPosition;
                Debug.Log("Cannot drop into disk before cooking.");
            }
        }
        else if (isOverPan && !hasBeenDroppedInPan)
        {
            // Đặt vị trí của Square vào vị trí của Pan
            transform.position = GameObject.FindWithTag("Pan").transform.position;
            Debug.Log("Dropped in the pan!");

            // Đánh dấu đã thả vào Pan và bắt đầu quá trình nấu
            hasBeenDroppedInPan = true;
            Invoke("CookSquare", 3f); // Sau 3 giây sẽ gọi hàm nấu chín

            // Sinh ra một bản sao của Square tại vị trí ban đầu của Square
            if (squarePrefab != null)
            {
                Instantiate(squarePrefab, originalPosition, Quaternion.identity); // Tạo một bản sao của Square tại vị trí ban đầu
                Debug.Log("New Square spawned at original position.");
            }
        }
        else
        {
            // Nếu không thả đúng chỗ, quay về vị trí ban đầu
            transform.position = originalPosition;
            Debug.Log("Dropped outside pan or disk.");
        }

        // Reset lại các trạng thái
        isOverPan = false;
        isOverDisk = false;
    }

    private void CookSquare()
    {
        // Sau khi thả vào Pan 3 giây, Square sẽ bị "nấu" (thay đổi màu hoặc hiệu ứng)
        Debug.Log("Square is cooked!");
        GetComponent<SpriteRenderer>().color = Color.yellow; // Thay đổi màu sắc của Square khi nấu
        isCooked = true; // Đánh dấu Square đã được nấu chín
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pan"))
        {
            isOverPan = true; // Đánh dấu Square đã vào Pan
        }
        else if (other.CompareTag("Disk"))
        {
            if (isCooked)
            {
                isOverDisk = true; // Đánh dấu Square đã vào Disk
                isInDisk = true; // Đánh dấu Square đã vào Disk
                Debug.Log("Square is in disk.");
            }
            else
            {
                Debug.Log("Cannot drop into disk before cooking.");
            }
        }
        else if (other.CompareTag("Trashcan"))
        {
            // Nếu Square vào Trashcan, bị hủy
            Destroy(gameObject);
            Debug.Log("Square thrown into trashcan and destroyed.");
        }
        else if (other.CompareTag("CustomerMouth"))
        {
            // Khi Square vào miệng khách hàng
            isGivenToCustomer = true;
            Debug.Log("Square given to customer.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Pan"))
        {
            isOverPan = false; // Đánh dấu Square đã ra khỏi Pan
        }
        else if (other.CompareTag("Disk"))
        {
            isOverDisk = false; // Đánh dấu Square đã ra khỏi Disk
        }
    }
}
