using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Attack : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private int damage = 10;
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void MakeDamage()
    {
        _enemy.health -= damage;
        if (_enemy.health <= 0)
        {
            SpawnObject();

            Destroy(_enemy.gameObject);
        }

    }
    private void SpawnObject()
    {
        
            //Instantiate(obj, this.transform.position, Quaternion.identity);
            Instantiate(obj, _enemy.transform.position, Quaternion.identity);
        
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("entraaqui");
            _enemy = other.GetComponent<Enemy>();

            MakeDamage();
        }
    }
}
