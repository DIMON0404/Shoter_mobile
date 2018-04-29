using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    private NavMeshAgent navMeshAgent;
    private GameObject target;
	// Use this for initialization
	void Start () {
        transform.Find("Head").GetComponent<MeshRenderer>().material.color = new Color(1, Random.Range(0.5f, 1f), 0);
        transform.Find("Body").GetComponent<MeshRenderer>().material.color = new Color(1, Random.Range(0f, 0.5f), 0);
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        navMeshAgent.SetDestination(target.transform.position);

    }

    public void GetDamage(int damage)
    {
        Controller.enemies.Remove(this);
        Destroy(gameObject);
    }
}
