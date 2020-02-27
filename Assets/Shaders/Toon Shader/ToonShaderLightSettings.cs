using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ToonShaderLightSettings : MonoBehaviour
{
    new private Light light;

    void OnEnable()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalVector("_ToonLightDirection", -transform.forward);
        Shader.SetGlobalColor("_ToonLightColor", light.color);
    }
}
