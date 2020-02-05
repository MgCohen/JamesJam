using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundRepeat : MonoBehaviour {

    [Header("Settings")]
    [SerializeField] private float speed;

	Renderer render;

	Vector2 offset;

	void Start()
	{
		render = GetComponent<Renderer> ();
	}

	void Update()
	{
        offset = new Vector2(Time.timeSinceLevelLoad * speed, 0);

        render.material.mainTextureOffset = offset;
	}

}
