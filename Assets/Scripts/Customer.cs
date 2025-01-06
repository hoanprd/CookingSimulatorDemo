using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public float timeWait;
    public SpriteRenderer fillBar; // Thanh Fill Bar
    [Range(0, 1)] public float value = 0.5f; // Giá trị từ 0 đến 1

    public static int wantedIndex, eatSuccess;
    public GameObject[] wantedShow;

    // Start is called before the first frame update
    void Start()
    {
        wantedIndex = Random.Range(0, 2);

        for (int i = 0; i < wantedShow.Length; i++)
        { 
            if (wantedIndex  == i)
            {
                wantedShow[i].SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeWait > 0)
        {
            timeWait -= Time.deltaTime;
            SetValue(timeWait / 5);
        }
        else
        {
            Destroy(gameObject);
        }

        // Cập nhật kích thước của Fill Bar theo giá trị
        if (fillBar != null)
        {
            fillBar.transform.localScale = new Vector3(value, 0.2f, 0.2f);
        }

        if (eatSuccess == 1)
        {
            eatSuccess = 0;
            Destroy(gameObject);
        }
    }

    // Hàm để cập nhật giá trị Fill Bar từ bên ngoài
    public void SetValue(float newValue)
    {
        value = Mathf.Clamp01(newValue); // Đảm bảo giá trị nằm trong khoảng 0 đến 1
    }
}
