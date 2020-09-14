using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float health;
    public float maxHealth;
    public GameObject onDeathCreate;
    public bool friendly;

    void Start()
    {
        health = maxHealth;
    }

    public void changeHealth(float num)
    {
        health += num;
        checkDead();
    }

    public void checkDead()
    {
        if (health <= 0)
        {
            if (onDeathCreate != null)
            {
                Instantiate(onDeathCreate, transform.position, transform.rotation);
            }
            Destroy(this.gameObject);
            return;
        }
    }

    public bool getTeam()
    {
        return friendly;
    }

    public float getMax()
    {
        return maxHealth;
    }
    public float getHealth()
    {
        return health;
    }


}
