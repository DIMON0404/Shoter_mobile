using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public PlayerController PlayerController;
    public Joystick joystick;
    public TouchController touchController;
    [Space]
    public GameObject spawnPlace;
    [Header("Prefabs")]
    public GameObject enemyPrefab;
    public static List<EnemyController> enemies;
    // Use this for initialization
    void Start()
    {
        enemies = new List<EnemyController>();
        CheckSpawn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckSpawn()
    {
        StartCoroutine(SpawnEnemy());
    }
    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(enemies.Count);
        GameObject instantiatedGO = Instantiate(enemyPrefab, spawnPlace.transform);
        enemies.Add(instantiatedGO.GetComponent<EnemyController>());
        StartCoroutine(SpawnEnemy());
    }
}
