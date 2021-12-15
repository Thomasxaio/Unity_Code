using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate_Obj : MonoBehaviour
{

    public GameObject Instantiate_Position;

    public GameObject CKSMemorialHall_Final;

    void Start()
    {

        Instantiate(CKSMemorialHall_Final, Instantiate_Position.transform.position,

        Instantiate_Position.transform.rotation);

    }

}
