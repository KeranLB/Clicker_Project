using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    public float HP;
    public float multiplicateur;
    // Start is called before the first frame update
    void Start()
    {
        HP = 10;
        multiplicateur = 1.1f;
    }

    // Update is called once per frame
    void Update()
    {
        var tmp = HP * multiplicateur;
        HP += tmp;
        print(HP);
    }
}
