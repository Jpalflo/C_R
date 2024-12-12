using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public Animator animatorEnemy;
    public GameObject dinero;
    [SerializeField]private GameObject spawn;


    public GameObject Spawn { get => spawn; set => spawn = value; }

    public void Awake()
    {
        spawn = transform.GetChild(0).gameObject;

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(spawn.transform.position);
        if (health < 0)
        {
            Instantiate(dinero, spawn.transform.position, Quaternion.identity);

               this.gameObject.SetActive(false);
        }
    }

   
}
