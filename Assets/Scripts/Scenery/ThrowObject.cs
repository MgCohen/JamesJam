using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class ThrowObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler

{

    [Tooltip("Percent")]
    [Range(0f, 2f)]
    public float speedMultiplier;

    private Rigidbody2D rigdb2D;
    private Collider2D coll;
    private MoveObject moveObject;


    private void Start()
    {
        rigdb2D = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        moveObject = GetComponent<MoveObject>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        rigdb2D.velocity = Vector3.zero;
        if (moveObject) moveObject.enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rigdb2D.isKinematic = false;
        coll.enabled = false;
        //rigdb2D.velocity = (transform.position.normalized - (Vector3.one / 2)) * (speedMultiplier * 1000f);
        if (moveObject)
        {
            if (transform.position.normalized.y > 0.5f)
                rigdb2D.velocity = Vector3.up * (speedMultiplier * 100f);
            else
                rigdb2D.velocity = Vector3.down * (speedMultiplier * 100f);
        }
    }

}
