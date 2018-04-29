using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    // Основной контроллер
    // На Controller
    public PlayerController playerController;
    public Joystick joystick;
    public TouchController touchController;
    public UI ui;
    [Space]
    public GameObject spawnPlace;
    public GameObject bulletsPool;
    [Header("Prefabs")]
    public GameObject enemyPrefab;
    public GameObject effect;
    [Space]
    private int _score;
    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            ui.SetScoreText(value);
        }
    }
    public static List<EnemyController> enemies;
    // Use this for initialization
    void Start()
    {
        _score = 0;
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
        yield return new WaitForSeconds(enemies.Count * 3);
        GameObject instantiatedGO = Instantiate(enemyPrefab, spawnPlace.transform);
        instantiatedGO.GetComponent<EnemyController>().controller = this;
        enemies.Add(instantiatedGO.GetComponent<EnemyController>());
        StartCoroutine(SpawnEnemy());
    }

    public void StartEffect(Vector3 point)
    {
        GameObject effectInstantiated = Instantiate(effect, bulletsPool.transform);
        effectInstantiated.transform.position = point;
        effectInstantiated.GetComponent<ParticleSystem>().Play();
        StartCoroutine(DestroyObject(effectInstantiated, 0.5f));
    }

    private IEnumerator DestroyObject(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    public void ChangeSightValue()
    {
        playerController.SightVision = ui.sightToogle.isOn;
    }
}
