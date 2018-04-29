using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    // Управлене действиями врага
    // На каждом враге
    public Controller controller;
    private NavMeshAgent navMeshAgent;
    private GameObject target;
    private int health = 100;

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
        health -= damage;
        controller.ui.ChangeHealthBar((float)health / 100);
        if (health <= 0)
        {
            Controller.enemies.Remove(this);
            controller.Score += 30;
            Destroy(gameObject);
        }
    }
}
