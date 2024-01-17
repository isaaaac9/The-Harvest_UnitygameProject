using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Vector2 InputVecter { get; private set; }
    public Vector3 MousePosition { get; private set; }

    private void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        InputVecter = new Vector2(h, v);

        MousePosition = Input.mousePosition;
    }
}
