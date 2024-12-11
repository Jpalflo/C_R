using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Attack : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private int damage = 10;
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
       
        _enemy.animatorEnemy.SetTrigger("TakeDamage");


    }
    private void SpawnObject()
    {

        Instantiate(_enemy.dinero, _enemy.Spawn.transform.position + new Vector3 (0, 1,0), Quaternion.identity); 
        
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            _enemy = other.GetComponent<Enemy>();
            _enemy.animatorEnemy = other.GetComponent<Animator>();
            MakeDamage();

        }
    }
}
