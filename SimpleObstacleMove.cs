using UnityEngine;

public class SimpleObstacleMove : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.back * GameManager.Instance.gameSpeed * Time.deltaTime, Space.World);

        if (transform.position.z < Camera.main.transform.position.z - 10f)
            Destroy(gameObject);
    }
}