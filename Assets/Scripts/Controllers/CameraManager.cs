using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class CameraManager : MonoBehaviour
{
    public float sizeMultiply = 1f;
    Camera cam_cache;
    Camera Cam
    {
        get
        {
            if (cam_cache == null)
                cam_cache = this.GetComponent<Camera>();
            return cam_cache;
        }
    }


    public void SetAspectSize()
    {
        Rect rect = Screen.safeArea;
        float ratio = Mathf.Clamp(rect.width / rect.height, 0f, 2f);

        float size = 0;
        if (ratio <= 2)
            size = 10f / ratio;
        else size = ratio * 2f;

        Cam.orthographicSize = size * sizeMultiply;
    }


    private void Awake()
    { SetAspectSize(); }
#if UNITY_EDITOR
    private void Update()
    { SetAspectSize(); }
#endif

}
