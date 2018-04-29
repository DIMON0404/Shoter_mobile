using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    // Управление игроком
    // На Player
    public Controller controller;
    public GameObject bulletPrefab;
    private bool _sightVision = true;
    public bool SightVision
    {
        get { return _sightVision; }
        set {
            _sightVision = value;
            if (!value)
            {
                line.transform.localScale = new Vector3(1, 1, 0);
            }
        }
    }
    private GameObject shouldWithGun;
    private GameObject gun;
    private GameObject startPoint;
    private GameObject endPoint;
    private GameObject line;

    private float lastShot;
    private float cooldown = 1f;

    private RaycastHit hit;

	// Use this for initialization
	void Start () {
        lastShot = Time.time - cooldown;
        shouldWithGun = transform.Find("RightShould").gameObject;
        gun = shouldWithGun.transform.GetChild(0).GetChild(0).gameObject;
        startPoint = gun.transform.Find("StartPoint").gameObject;
        endPoint = gun.transform.Find("EndPoint").gameObject;
        line = endPoint.transform.Find("Line").gameObject;
    }

    void Update()
    {
        if (_sightVision)
        {
            if (Physics.Raycast(endPoint.transform.position, endPoint.transform.position - startPoint.transform.position, out hit, 100))
            {
                line.transform.localScale = new Vector3(1, 1, hit.distance);
            }
        }
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
        if (Time.time - cooldown > lastShot)
        {
            StartCoroutine(RotateShould());
            GameObject bullet = Instantiate(bulletPrefab, controller.bulletsPool.transform);
            bullet.GetComponent<ShotController>().parentGun = gun;
            bullet.GetComponent<ShotController>().controller = controller;
            bullet.transform.position = endPoint.transform.position;
            bullet.transform.rotation = gun.transform.rotation;
            bullet.transform.Rotate(90, 0, 0);
            bullet.GetComponent<Rigidbody>().AddForce((endPoint.transform.position - startPoint.transform.position) * 5000);
            lastShot = Time.time;
            gun.GetComponent<AudioSource>().Play();
        }
    }

    private IEnumerator RotateShould()
    {
        shouldWithGun.transform.Rotate(new Vector3(-90, 0, 0));
        yield return new WaitForSeconds(0.5f);
        shouldWithGun.transform.Rotate(90, 0, 0);
    }
}
