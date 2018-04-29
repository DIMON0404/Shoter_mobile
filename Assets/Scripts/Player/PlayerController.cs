using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Controller controller;
    public GameObject bulletPrefab;
    public GameObject bulletsPool;
    public GameObject gun;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void MoveAndRotatePlayer(Vector2 value)
    {
        transform.position = Vector3.MoveTowards(transform.position, 
            new Vector3(transform.position.x + transform.forward.x * value.y / 10, transform.position.y, transform.position.z + transform.forward.z * value.y / 10),
            1);
        transform.Rotate(0, value.x * 2, 0);
    }

    public void Jump()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0, 0.01f, 0), new Vector3(0, -1, 0), out hit, 0.1f))
        {
            GetComponent<Rigidbody>().AddForce(0, 250, 0);
        }
    }

    public void Shot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletsPool.transform);
        bullet.GetComponent<ShotController>().parentGun = gun;
        bullet.transform.position = gun.transform.Find("EndPoint").position;
        bullet.GetComponent<Rigidbody>().AddForce((gun.transform.Find("EndPoint").position - gun.transform.Find("StartPoint").position) * 7000);
    }
}
