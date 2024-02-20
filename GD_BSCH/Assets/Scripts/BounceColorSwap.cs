using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceColorSwap : MonoBehaviour
{
    public List<Color> colors = new List<Color>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter()
    {
        gameObject.GetComponent<Renderer>().material.color = colors[Random.Range(0, colors.Count)];
    }
}
