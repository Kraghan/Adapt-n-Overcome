using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType
{
    PATH,
    PLAYER
}

public class Enemy : Entity
{
    ParticleSystem m_particles;

    Pool m_originPool = null;

    MovementType m_movementType;
    List<Positions> m_pathPoints = new List<Positions>();
    Transform m_player;
    Vector3 m_previousDirection = new Vector3(0, 0, float.MaxValue);

    protected override void Start()
    {
        base.Start();
        m_particles = GetComponentInChildren<ParticleSystem>();
        if (m_particles)
        {
            m_particles.gameObject.SetActive(false);
            m_particles.Stop();
        }

        m_player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if(m_movementType == MovementType.PATH)
        {
            if(m_pathPoints.Count == 0)
            {
                Disapear();
                return;
            }

            if (Vector3.Distance(m_pathPoints[0].GetPosition(), transform.position) <= 0.01f)
            {
                m_pathPoints[0].UpdateTimer();
            }
            else
            {
                Vector3 direction = m_pathPoints[0].GetPosition() - transform.position;
                transform.position += direction.normalized * GetSpeed() * Time.fixedDeltaTime;
            }

        }
        else if(m_movementType == MovementType.PLAYER)
        {
            if(transform.position.y < -5)
            {
                Disapear();
            }

            if(m_previousDirection.z == float.MaxValue || transform.position.y > m_player.position.y)
            {
                Vector3 direction = m_player.position - transform.position;
                transform.position += direction.normalized * GetSpeed() * Time.fixedDeltaTime;
                m_previousDirection = direction;
            }
            else
            {
                transform.position += m_previousDirection.normalized * GetSpeed() * Time.fixedDeltaTime;
            }
        }
    }

    public override void Die()
    {
        base.Die();
        m_manager.AddKill();

        if (m_particles)
        {
            m_particles.gameObject.SetActive(true);
            m_particles.Play();
        }

        StartCoroutine(OnParticlesComplete());
    }

    IEnumerator OnParticlesComplete()
    {
        while(m_particles.IsAlive())
        {
            yield return new WaitForSeconds(0.5f);
        }

        Disapear();
    }

    public void Disapear()
    {
        if (m_originPool)
            m_originPool.ReleaseObject(gameObject);
        else
            Destroy(gameObject);
    }

    public void SetPool(Pool pool)
    {
        m_originPool = pool;
    }

    public void SetMovementTypeToPlayerTarget()
    {
        m_movementType = MovementType.PLAYER;
    }

    public void SetMovementTypeToPath(List<Positions> points)
    {
        m_movementType = MovementType.PATH;
        m_pathPoints.Clear();

        m_pathPoints.AddRange(points);
    }

    public override void SetAlive()
    {
        base.SetAlive();

        m_particles.gameObject.SetActive(false);
    }
}
