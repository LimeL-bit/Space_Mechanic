using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] int bulletAmmount;
    [SerializeField] int bulletSpeed;
    [SerializeField] int bulletSpred;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet);
        }
    }
}
