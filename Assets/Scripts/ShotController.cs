using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {

    public GameObject parentGun;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            GameObject enemy = other.gameObject;
            while (!enemy.GetComponent<EnemyController>())
            {
                enemy = enemy.transform.parent.gameObject;
            }
            print("Shot");
            enemy.GetComponent<EnemyController>().GetDamage(30);
        }
        if (other.gameObject != parentGun)
        {
            Destroy(gameObject);
        }
    }
}
