﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AllContainerScript : MonoBehaviour {

    [SerializeField]
    List<SpawnableObjectContainerScript> containers;


    public SpawnableObjectContainerScript getContainer(int nb)
    {
        if (nb < 0 || nb > containers.Count)
            return null;
        else return containers[nb];
    }

}