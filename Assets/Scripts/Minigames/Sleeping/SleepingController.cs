using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingController : Minigame
{

    public GameObject spaceship;
    public List<ObjectSpawner> meteorSpawners;


    private void Start()
    {
        success = true;
    }


    public override void UpdateBehaviour(float timer)
    {
        foreach (var spawner in meteorSpawners)
        {
            foreach (var obj in spawner.objsSpawned)
            {
                MoveObject moveObject = obj.GetComponent<MoveObject>();
                if (moveObject != null)
                {
                    if (moveObject.constantlyMoveTo == null)
                        moveObject.constantlyMoveTo = spaceship.transform;
                }
            }
        }
        base.UpdateBehaviour(timer);
    }

}
