using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public float raiseHeight = 5f;
    public float speed = 2f;

    private Vector3 loweredPosition;
    private Vector3 raisedPosition;
    private bool isRaised = false;


    // Start is called before the first frame update
    void Start()
    {
        loweredPosition = transform.position;
        raisedPosition = loweredPosition +new Vector3(0, raiseHeight, 0);

    }

    public void Raisegate()
    {
        if(!isRaised)
        {
            StartCoroutine(MoveGate(raisedPosition));
            isRaised = true;
        }
    }

    public void LowerGate()
    {
        if(isRaised)
        {
            StartCoroutine(MoveGate(loweredPosition));
            isRaised = false;
        }
    }

    private IEnumerator MoveGate(Vector3 targetPosition)
    {
        while(Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
    }

}
