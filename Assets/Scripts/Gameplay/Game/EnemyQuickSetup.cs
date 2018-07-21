using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyQuickSetup : MonoBehaviour
{
    [SerializeField]
    MovementType m_type;
    [SerializeField]
    Positions[] m_path;

	void Start ()
    {
        Enemy enemy = GetComponent<Enemy>();

		if(m_type == MovementType.PATH)
        {
            List<Positions> path = new List<Positions>();
            foreach(Positions position in m_path)
            {
                path.Add(position);
            }

            enemy.SetMovementTypeToPath(path);
        }
        else
        {
            enemy.SetMovementTypeToPlayerTarget();
        }
	}
}
