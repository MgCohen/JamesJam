using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public Transform constantlyMoveTo;
    public float dragTime;

    [HideInInspector]
    public bool hitObstacle;

    private Vector3 mousePos;

    private bool canDrag = true;

    private void Update()
    {
        if (constantlyMoveTo != null)
            transform.position = Vector3.MoveTowards(transform.position, constantlyMoveTo.position, Time.deltaTime);

        if (Time.timeScale == 0)
            canDrag = false;
    }

    private void OnMouseDown()
    {
        if (dragTime > 0)
            StartCoroutine(DragTimer());
    }

    private void OnMouseDrag()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (canDrag)
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
    }

    private IEnumerator DragTimer()
    {
        canDrag = true;
        yield return new WaitForSeconds(dragTime);
        canDrag = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
            hitObstacle = true;
    }
}
