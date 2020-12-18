using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (MeshRenderer))]
public class GraterHandler : MonoBehaviour
{
    private Material _mat;
    [SerializeField] Texture texture;

    private void Awake ()
    {
        _mat = GetComponent<MeshRenderer>().material;
        _mat.mainTexture = texture;
    }
}