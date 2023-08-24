using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;

    private bool VriusStop = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, moveSpeed * Time.deltaTime * -2.5f, 0);

        if (transform.position.y < -5.18)
        {
            Destroy(gameObject);
            transform.parent.GetComponent<VirusFolder>().SpwanVirus();
        }
    }
}
