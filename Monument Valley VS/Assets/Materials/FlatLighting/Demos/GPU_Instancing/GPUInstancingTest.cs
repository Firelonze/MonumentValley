using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUInstancingTest : MonoBehaviour {

    public Transform prefab;

    public int instances = 5000;

    public float radius = 50f;

    void Start()
    {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        MeshRenderer renderer;

        for (int i = 0; i < instances; i++)
        {
            generateRandomProperties(props);

            Transform t = Instantiate(prefab);
            t.SetParent(transform);
            t.localPosition = Random.insideUnitSphere * radius;
            t.localRotation = Quaternion.AngleAxis(Random.Range(0f, 180f), Random.insideUnitSphere);

            renderer = t.GetComponent<MeshRenderer>();
            renderer.SetPropertyBlock(props);
        }
    }

    private void generateRandomProperties(MaterialPropertyBlock props)
    {
        props.SetColor("_LightPositiveX", GenerateRandomColor());
        props.SetFloat("_GradientOriginOffsetPositiveX", generateRandomOffset());
        props.SetFloat("_GradientWidthPositiveX", generateRandomWidth());
        props.SetColor("_LightNegativeX2", GenerateRandomColor());
        props.SetFloat("_GradientOriginOffsetNegativeX", generateRandomOffset());
        props.SetFloat("_GradientWidthNegativeX", generateRandomWidth());

        props.SetColor("_LightPositiveZ", GenerateRandomColor());
        props.SetFloat("_GradientOriginOffsetPositiveZ", generateRandomOffset());
        props.SetFloat("_GradientWidthPositiveZ", generateRandomWidth());
        props.SetColor("_LightNegativeZ2", GenerateRandomColor());
        props.SetFloat("_GradientOriginOffsetNegativeZ", generateRandomOffset());
        props.SetFloat("_GradientWidthNegativeZ", generateRandomWidth());

        props.SetColor("_LightPositiveY", GenerateRandomColor());
        props.SetFloat("_GradientOriginOffsetPositiveY", generateRandomOffset());
        props.SetFloat("_GradientWidthPositiveY", generateRandomWidth());
        props.SetColor("_LightNegativeY2", GenerateRandomColor());
        props.SetFloat("_GradientOriginOffsetNegativeY", generateRandomOffset());
        props.SetFloat("_GradientWidthNegativeY", generateRandomWidth());
    }

    private float generateRandomOffset()
    {
        return Random.Range(0, 1);
    }

    private float generateRandomWidth()
    {
        return Random.Range(0.5f, 2f);
    }

    private Color GenerateRandomColor()
    {
        float r = Random.Range(0.0f, 1.0f);
        float g = Random.Range(0.0f, 1.0f);
        float b = Random.Range(0.0f, 1.0f);

        return new Color(r, g, b);
    }
}
