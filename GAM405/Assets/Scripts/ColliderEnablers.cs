using System.Collections;
using UnityEngine;

public class ColliderEnablers : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        StartCoroutine(EnableAfterSecond());

    }

    private IEnumerator EnableAfterSecond()
    {
        yield return new  WaitForSeconds(0.1f);
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = true;
    }
}
