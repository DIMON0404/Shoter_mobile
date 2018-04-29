using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {
    // Управление пулей
    // Присутствует на каждой выпущеной пуле
    public GameObject parentGun;
    public Controller controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            GameObject enemy = other.gameObject;
            while (!enemy.GetComponent<EnemyController>())
            {
                enemy = enemy.transform.parent.gameObject;
            }
            enemy.GetComponent<EnemyController>().GetDamage(30);
        }
        if (other.gameObject != parentGun)
        {
            controller.StartEffect(transform.position);
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
