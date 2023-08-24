using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    private bool FallStop = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Enemys3>().isDie == true)
        {
            Destroy(gameObject);
            transform.parent.GetComponent<VirusFolder>().SpwanVirus();
        }
    }
}
