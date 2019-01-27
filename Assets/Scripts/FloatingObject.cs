using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
	private Transform m_transform;

	private void Awake() {
		m_transform = transform;
	}

    void LateUpdate()
    {
		var position = transform.position;
		var position2D = new Vector2(position.x, position.z);
		position.y = Water.GetHeightAt(position2D);

		var normal = Water.GetNormal(position2D);

		m_transform.rotation = Quaternion.FromToRotation(transform.up, normal) * m_transform.rotation;
		m_transform.position = position;
    }
}
