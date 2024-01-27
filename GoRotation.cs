using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoRotation : MonoBehaviour
{

    public float degreesPerSec = 20f;

    private void Update()
    {
        transform.Rotate(new Vector3(0, degreesPerSec, 0) * Time.deltaTime);

    }

}
