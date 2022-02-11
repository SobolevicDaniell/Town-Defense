using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg_moving : MonoBehaviour
{
    public GameObject bg;

    void Update()
    {
        bg.transform.position += new Vector3(-50 * Time.deltaTime, 0, 0);

        if (bg.transform.position.x <= -1760) { bg.transform.position = new Vector2(1160, 302.5f); }
    }
}
