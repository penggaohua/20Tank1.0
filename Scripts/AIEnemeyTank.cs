using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIEnemeyTank : MonoBehaviour
{
    public GameObject target;
    public float interval=20f;
	private NavMeshAgent agent;
   // bool isArrive = true;
    void Start()
    {
        float interval_f = Random.Range(interval, interval + 10f);
        agent = this.GetComponent<NavMeshAgent>();
        InvokeRepeating("TankCannonAI", 2, interval_f);
    }

    //void TankCannonAI()
    //{
    //    GoPosition(50);           
    //}

    void TankCannonAI()
    {
        GoPosition(60);
    }
    //Vector3 newPosition;
   // void GoPosition(out Vector3 newPosition)
    void GoPosition(int randomRange)
    {
        float px = target.transform.position.x + Random.Range(-randomRange, randomRange);
        float pz = target.transform.position.z + Random.Range(-randomRange, randomRange);
        float py = target.transform.position.y; 
        Vector3 newPosition = new Vector3(px, py, pz);
   //    Debug.Log("newPosition" + newPosition);
       if(agent)
        agent.SetDestination(newPosition);
       Debug.Log("前往指定地点");
    }

    void GoPositionCircle(int r)
    {
        //随机到一个距离目标半径为r的圆上
        float px = target.transform.position.x;
        float pz = target.transform.position.z;
        
        px += Random.Range(-r, r);
        pz = Mathf.Sqrt(r * r - px * px);
        int temp = Random.Range(-1, 1);
        if (temp < 0)
            pz = -pz;
        Vector3 newPosition = new Vector3(px, 0, pz);

        if (agent)
            agent.SetDestination(newPosition);
        Debug.Log("前往指定地点circle"+newPosition);
    }

    //等待
    void Wait(int seconds)
    {

    }

}
