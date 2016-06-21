using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class GamerInstanceManager : NetworkBehaviour
{

    public static GamerInstanceManager Instance { get; private set;}

    public virtual void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            return;
        }
        Destroy(this.gameObject);
    }

    [SerializeField]
    AbstractPlayerController[] gamerInstance;

    private Queue<AbstractPlayerController> availableControllers = null;
    private int numberOfPlayerWaiting = 0;

    private Queue<AbstractPlayerController> AvailableControllers
    {
        get
        {
            if (null == availableControllers)
            {
                availableControllers = new Queue<AbstractPlayerController>(gamerInstance);
            }

            return availableControllers;
        }
    }

    public AbstractPlayerController GetNextAvailableController()
    {
        if (AvailableControllers.Count <= 0)
        {
            return null;
        }

        return AvailableControllers.Dequeue();
    }

    public void ReleaseController(AbstractPlayerController controller)
    {
        if (null == controller)
        {
            return;
        }
        AvailableControllers.Enqueue(controller);
    }

    public int getNumberOfPlayerWaiting()
    {
        return this.numberOfPlayerWaiting;
    }

    public void incNumberOfPlayerWaiting()
    {
        this.numberOfPlayerWaiting++;
        print(numberOfPlayerWaiting);
    }
}
