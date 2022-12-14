using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndShoot : MonoBehaviour
{

    GameControler gameControler;
    [SerializeField] GameObject GameCon;
    public GameObject crosshairs;
    public GameObject player;
    public GameObject bulletPrefab;
    public GameObject bulletStart;

   


    public float bulletSpeed = 60.0f;
    public int bulletsAvalible = 50;
    public int bulletsShot = 0;
    public bool pause = false;
    private Vector3 target;


    void Awake()
    {
        gameControler = GameCon.GetComponent<GameControler>();
    }
    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (pause == false)
            {
                pause = true;
            }
            else
            {
                pause = false;
            }

        }
        if (Input.GetKeyDown(KeyCode.R))
        {

            if(gameControler.score >= 100)
            {
                bulletsAvalible = bulletsAvalible + 10;
            }
            
            
        }



        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshairs.transform.position = new Vector2(target.x, target.y);

        Vector3 difference = target - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);


    if (pause == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                direction.Normalize();
                fireBullet(direction, rotationZ);
            }
        }
        else
        {
        }
    }
    
    
    void fireBullet(Vector2 direction, float rotationZ)
    {
        if (bulletsShot < bulletsAvalible)
        {
            GameObject b = Instantiate(bulletPrefab) as GameObject;
            b.transform.position = bulletStart.transform.position;
            b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
            b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            bulletsShot++;
            FindObjectOfType<GameControler>().AmmoUse();

        }
       
    }
    
}
