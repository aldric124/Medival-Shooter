using UnityEngine;

public class TargetClickDestroy : MonoBehaviour
{
    void OnMouseDown()
    {
        Destroy(gameObject);
    }
}