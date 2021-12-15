using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTexture : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private Texture yellow, red, green;
    private Renderer cubeRenderer;
    private bool isPrize = false;

    // Start is called before the first frame update
    void Start()
    {
        cubeRenderer = cube.GetComponent<Renderer>();
        gameObject.GetComponent<Button>().onClick.AddListener(ChangeCubeTexture);
    }

    // Update is called once per frame
    void ChangeCubeTexture()
    {
        if (isPrize) {            
            cubeRenderer.material.mainTexture = green;
        } else {            
            cubeRenderer.material.mainTexture = red;
        }
    }
}
