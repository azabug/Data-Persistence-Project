using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float Speed = 2.0f;
    public float MaxMovement = 2.0f;
    public Vector3 paddleScale;
    public MainManager mainMan;
    // Start is called before the first frame update
    void Start()
    {
        float newSize = 0.0f;
        mainMan = GameObject.Find("MainManager").GetComponent<MainManager>();
        newSize = mainMan.paddleSize;
        paddleScale = new Vector3(newSize, 0.1f, 1.0f);
        transform.localScale = paddleScale;
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Horizontal");

        Vector3 pos = transform.position;
        pos.x += input * Speed * Time.deltaTime;

        if (pos.x > MaxMovement)
            pos.x = MaxMovement;
        else if (pos.x < -MaxMovement)
            pos.x = -MaxMovement;

        transform.position = pos;
    }
    
}
