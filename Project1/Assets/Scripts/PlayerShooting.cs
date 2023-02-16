using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public Camera cam;
    Vector2 mousePos;
    private Rigidbody2D rb;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator animator;
    public float bulletForce = 20f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Fire2")){
            animator.SetBool("isShooting", true);
            Shoot();
        }
        else {
        animator.SetBool("isShooting", false);
        }
        Vector2 positionOnScreen = cam.WorldToViewportPoint(rb.position);
        Vector2 mouseOnScreen = (Vector2)cam.ScreenToViewportPoint(Input.mousePosition);
        float angle = AngleBetweenTwoPoints(positionOnScreen,mouseOnScreen);
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        firePoint.rotation = Quaternion.Euler (new Vector3(0f,0f,angle));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b){
        return Mathf.Atan2(a.y-b.y, a.x - b.x) * Mathf.Rad2Deg + 90f;
    }

    void Shoot(){
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb1 = bullet.GetComponent<Rigidbody2D>();
        rb1.rotation +=90f;
        rb1.AddForce(firePoint.up*bulletForce, ForceMode2D.Impulse);

    }

    void FixedUpdate(){
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //firePoint.rotation = angle;
    }
}
