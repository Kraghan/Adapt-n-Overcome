using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{

    [SerializeField]
    Slider m_lifeSlider;
    [SerializeField]
    Slider m_bombSlider;
    [SerializeField]
    Slider m_superSlider;

    Timer m_bombTimer = new Timer(5);
    Timer m_lifeTimer = new Timer(10);
    Timer m_superTimer = new Timer(3);

    uint m_killsNeededForSuper = 30;
    uint m_killsDoneForSuper = 0;

    uint m_maxBombStorage = 1;
    uint m_bombStored = 0;

    bool m_isUsingSuper = false;

    public override void Die()
    {
        
    }

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();

        m_killsDoneForSuper = m_manager.GetKills();
    }
	
	protected override void FixedUpdate ()
    {
        base.FixedUpdate();
        
        ManageBomb();
        ManageLife();
        ManageSuper();

        ManageMovement();

        ManageInputs();
    }

    public bool UltraAvailable()
    {
        uint killsDone = m_manager.GetKills() - m_killsDoneForSuper;
        return killsDone >= m_killsNeededForSuper;
    }

    public bool BombAvailable()
    {
        return m_bombStored != 0;
    }

    private void ManageBomb()
    {
        if (m_bombTimer.IsTimedOut())
        {
            if (m_bombStored != m_maxBombStorage)
                m_bombStored++;
            if (m_bombStored != m_maxBombStorage)
                m_bombTimer.Restart();
        }
        else
        {
            m_bombTimer.FixedUpdateTimer();
        }

        m_bombSlider.value = m_bombTimer.GetRatio();
    }

    private void ManageLife()
    {
        if (m_lifeTimer.IsTimedOut() && !IsFullLife())
        {
            SetLifePoint(GetLifePoint() + 1);
            if(!IsFullLife())
            {
                m_lifeTimer.Restart();
            }
        }
        else
        {
            m_lifeTimer.FixedUpdateTimer();
        }

        m_lifeSlider.value = m_lifeTimer.GetRatio();
    }

    private void ManageSuper()
    {
        if(!m_isUsingSuper)
        {
            uint killsDone = m_manager.GetKills() - m_killsDoneForSuper;
            m_superSlider.value = Mathf.Clamp01((float)killsDone / (float)m_killsNeededForSuper);
        }
        else
        {
            m_superTimer.FixedUpdateTimer();
            if (m_superTimer.IsTimedOut())
                m_isUsingSuper = false;
            m_superSlider.value = 1 - m_superTimer.GetRatio();
        }
    }

    private void ManageMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(horizontal, vertical).normalized;

        Vector2 movement = direction * GetSpeed() * Time.fixedDeltaTime;

        transform.position += new Vector3(movement.x, movement.y, 0);
    }

    private void ManageInputs()
    {
        if (Input.GetButton("Fire"))
        {
            if (Shoot())
                m_manager.AddShoot();
        }

        if (Input.GetButton("Bomb") && BombAvailable())
        {
            Bomb();
        }

        if(Input.GetButton("Super") && UltraAvailable())
        {
            ActiveSuper();
        }
        else
        {
            DesactiveSuper();
        }
    }

    public override uint InflictDamage(uint damage)
    {
        bool wasFullLife = IsFullLife();
        uint damageTake = base.InflictDamage(damage);

        if(wasFullLife && damageTake > 0)
        {
            m_lifeTimer.Restart();
        }

        return damageTake;
    }

    public void Bomb()
    {
        if (m_maxBombStorage == m_bombStored)
            m_bombTimer.Restart();
    }

    public void ActiveSuper()
    {
        if(!m_isUsingSuper)
        {
            m_killsDoneForSuper = m_manager.GetKills();
            m_isUsingSuper = true;
        }
        else
        {
            
        }
    }

    public void DesactiveSuper()
    {

    }
}
