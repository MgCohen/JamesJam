using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Transform constantlyMoveTo;
    public float speed = 1;
    public float distanceLimit;


    private void Update()
    {
        if (constantlyMoveTo != null)
        {
            SetRotation();
            transform.position = Vector3.MoveTowards(transform.position, constantlyMoveTo.position, speed * Time.deltaTime);

            if (Vector2.Distance(constantlyMoveTo.position, transform.position) <= distanceLimit)
                GameController.Instance.LooseMinigame();
        }
    }


    private void SetRotation()
    {
        Vector2 toLook = constantlyMoveTo.position - transform.position;
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(toLook.y, toLook.x) * Mathf.Rad2Deg - 270f);
        transform.rotation = rotation;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
            GameController.Instance.LooseMinigame();
    }
}
