using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*Classe qui contient d'autre conteneur de gameobject et qui permet d'y accéder*/ 
public class AllContainerScript : MonoBehaviour {

    [SerializeField]
    List<SpawnableObjectContainerScript> containers;

    /* Retourne un conteneur à un rang*/
    public SpawnableObjectContainerScript getContainer(int nb)
    {
        if (nb < 0 || nb > containers.Count)
            return null;
        else return containers[nb];
    }

    /*Desactive tout les conteneur et leur contenu*/
    public void DesactiveAll()
    {
        for(int i=0;i<containers.Count;i++)
        {
            containers[i].DesactiveAll();
        }
    }

}
