using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCaravan : MonoBehaviour
{

    private Transform city;
    private Transform town;
    public float speed = 10f;

    public void Initialize(Transform city, Transform town)
    {
        this.city = city;
        this.town = town;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCaravan();
    }

    void MoveCaravan()
    {
        if (town == null)
        {
            Debug.LogError("Town reference is missing! Make sure the town is assigned properly.");
            return;
        }

        Debug.Log($"Caravan moving towards {town.position} from {transform.position}");
        transform.position = Vector3.MoveTowards(transform.position, town.position, (speed * Time.deltaTime));

        if (Vector3.Distance(transform.position, town.position) < 0.1f)
        {
            ArriveAtTown();
        }
    }

    void ArriveAtTown()
    {

       // Debug.Log("Caravan has arrived at the town!");

        Destroy(gameObject);
    }
}
