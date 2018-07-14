using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetMode
{
    DIRECTION,
    ENTITY
}

public class Bullet : Entity
{
    
    TargetMode m_mode;

    // Entity mode
    Entity m_target;
    List<Vector2> m_previousPosition = new List<Vector2>();
    uint m_precision;

    // Direction mode
    Vector2 m_direction;

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void FixedUpdate ()
    {
        base.FixedUpdate();
        Vector2 movement = new Vector2();

        if(m_mode == TargetMode.DIRECTION)
        {
            movement = m_direction * GetSpeed();
        }
        else if(m_mode == TargetMode.ENTITY)
        {
            m_previousPosition.Add(m_target.transform.position);
            if(m_previousPosition.Count > m_precision)
                m_previousPosition.RemoveAt(0);

            Vector2 direction = m_previousPosition[0] - new Vector2(transform.position.x, transform.position.y);
            movement = direction * GetSpeed();
        }

        transform.position += new Vector3(movement.x, movement.y, 0) * Time.fixedDeltaTime;
    }

    public void SetDirection(Vector2 direction)
    {
        m_direction = direction;
        m_mode = TargetMode.DIRECTION;
    }

    public void SetTarget(Entity entity, uint precision)
    {
        m_target = entity;
        m_mode = TargetMode.ENTITY;
        m_precision = precision;
        m_previousPosition.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Entity"))
        {
            Entity entity = collision.GetComponent<Entity>();
            entity.InflictDamage(GetDamage());
            m_manager.AddTouched();
        }

        if(!collision.CompareTag("Analytics"))
            Die();
    }

    public override void Die()
    {
        s_bulletPool.ReleaseObject(gameObject);
    }
}
