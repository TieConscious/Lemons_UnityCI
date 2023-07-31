using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFader : MonoBehaviour
{
    

    void Update() {
        if (Input.GetKeyDown("space")) {
            StartCoroutine(nameof(Fade));
        }
    }

    IEnumerator Fade()
    {
        Renderer renderer = GetComponent<Renderer>();
        Color color = renderer.material.color;
        Debug.Log("toink");

        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            color.a = f;
            renderer.material.color = color;
            yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds
        }
    }

}
