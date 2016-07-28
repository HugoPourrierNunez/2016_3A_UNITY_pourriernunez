using UnityEngine;
using System.Collections;

/*Classe qui gère les balles, leur collision et leur temps de vie*/
public class BalleScript : MonoBehaviour {

    [SerializeField]
    Rigidbody myRb;

    private float lifeTime = 0;

    private int nbCollision = 0;

    private bool actif = false;

    [SerializeField]
    CanonScript canon;


    public void setCanon(CanonScript c)
    {
        canon = c;
    }

    public Rigidbody getRigidBody()
    {
        return myRb;
    }

    void OnTriggerEnter(Collider col)
    {
        nbCollision++;
        if(nbCollision>1)
        {
            canon.removePV(col.gameObject);
            Desactive();
        }
            
    }

    void Update()
    {
        if (Time.time >= lifeTime && actif)
        {
            Desactive();
        }
    }

    private void Desactive()
    {
        actif=false;
        gameObject.SetActive(false);
        nbCollision = 0;
        transform.localPosition = Vector3.zero;
    }

    public void startLifeTime(float lt)
    {
        lifeTime = Time.time + lt;
        actif = true;
    }
}
