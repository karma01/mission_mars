using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField][Range(0, 1)] float  movementFactor;
    [SerializeField] float period = 2f;
    private void Start()
    {
        startingPosition = transform.position;

    }
    private void Update()

    {
        if(period <=Mathf.Epsilon) { return; }//Mathf.Epsilon is the smallest posssible value.. this process is done to prevent nan error
        float cycles = Time.time / period; // to calculate the radian 
        const float tau = Mathf.PI * 2; // VALUE of tou ie 2 of pi
        float rawSineWave = Mathf.Sin(cycles * tau); /// to calculate value between[-1,1]
        movementFactor = (rawSineWave + 1F/ 2F); //TO PRODUCE VALUE BETWEEN 0 AND 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }

}
