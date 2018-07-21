using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    // Carac
    private uint m_life = 1;
    private uint m_maxLife = 3;
    private float m_speed = 1;
    private uint m_resistance = 0;
    private uint m_damage = 1;
    private float m_rateOfFire = 0.5f;
    private float m_timeElapsedSinceLastshoot;

    // Unity objects
    private Animator m_animator;
    protected Rigidbody2D m_body;
    private Collider2D m_collider;
    private SpriteRenderer[] m_renderers;

    static protected Pool s_bulletPool = null;

    [SerializeField]
    GameObject m_bulletPrefab;

    protected StatsManager m_manager;

    // Use this for initialization
    protected virtual void Start ()
    {
        m_animator = GetComponentInChildren<Animator>();
        SetLifePoint(m_life);

        m_body = GetComponent<Rigidbody2D>();

        if(!s_bulletPool)
        {
            GameObject pool = GameObject.FindGameObjectWithTag("Pools");
            GameObject bulletPool = new GameObject("Bullet Pool");
            bulletPool.transform.parent = pool.transform;
            s_bulletPool = bulletPool.AddComponent<Pool>();
            s_bulletPool.FillPoolWith(m_bulletPrefab, 200);
        }

        m_timeElapsedSinceLastshoot = m_rateOfFire;

        m_manager = GameObject.FindGameObjectWithTag("StatsManager").GetComponent<StatsManager>();

        m_renderers = GetComponentsInChildren<SpriteRenderer>();

        m_collider = GetComponent<Collider2D>();

    }
	
	// Update is called once per frame
	protected virtual void FixedUpdate ()
    {
        m_timeElapsedSinceLastshoot += Time.fixedDeltaTime;
    }

    public uint GetLifePoint()
    {
        return m_life;
    }

    public uint GetMaxLifePoint()
    {
        return m_maxLife;
    }

    public bool IsFullLife()
    {
        return GetLifePoint() == GetMaxLifePoint();
    }

    public bool IsDead()
    {
        return GetLifePoint() == 0;
    }

    public float GetSpeed()
    {
        return m_speed;
    }

    public void SetLifePoint(uint life)
    {        
        m_life = life;
        if(!CompareTag("Bullet"))
            m_animator.SetInteger("hitpoints", (int)m_life);

        if (m_life <= 0)
            Die();
    }

    public void SetMaxLifePoint(uint maxLife)
    {
        m_maxLife = maxLife;
    }

    public void SetSpeed(float speed)
    {
        m_speed = speed;
    }

    public void SetDamage(uint damage)
    {
        m_damage = damage;
    }

    public uint GetDamage()
    {
        return m_damage;
    }

    public uint GetResistance()
    {
        return m_resistance;
    }

    public virtual uint InflictDamage(uint damage)
    {
        int damageTotal = (int)damage - (int)GetResistance();

        if (damageTotal > 0)
        {
            int lifePoint = (int)GetLifePoint() - damageTotal;
            if (lifePoint < 0)
                lifePoint = 0;
            SetLifePoint((uint)lifePoint);

            return (uint)damageTotal;
        }
        else
        {
            return 0;
        }
    }

    virtual public void Die()
    {
        foreach (SpriteRenderer renderer in m_renderers)
        {
            renderer.enabled = false;
        }

        m_collider.enabled = false;

        if (!CompareTag("Bullet"))
            m_animator.SetBool("dead", true);
    }

    public bool Shoot()
    {
        if (m_timeElapsedSinceLastshoot < m_rateOfFire)
            return false;

        GameObject obj = s_bulletPool.PickObject();
        Bullet bullet = obj.GetComponent<Bullet>();
        bullet.SetDirection(Vector2.up);
        bullet.SetDamage(GetDamage());
        bullet.transform.position = transform.position;
        m_timeElapsedSinceLastshoot = 0;
        obj.layer = gameObject.layer;

        return true;
    }

    public virtual void SetAlive()
    {
        m_collider.enabled = true;
        foreach (SpriteRenderer renderer in m_renderers)
        {
            renderer.enabled = true;
        }
        if (!CompareTag("Bullet"))
            m_animator.SetBool("dead", false);
    }
}
