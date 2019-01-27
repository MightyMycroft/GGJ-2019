using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMover : MonoBehaviour
{
	private const float k_TwoPi = 6.3f;

	[SerializeField] Vector2 direction = Vector2.up;
	[SerializeField] float speed = 1f;
	[SerializeField] float amplitude;
	[SerializeField] float frequency;
	[SerializeField] float offset;
	[SerializeField] float heightOffset;

	private Transform m_transform;

	private void Awake() {
		m_transform = transform;
	}

    void LateUpdate()
    {
		var position = transform.position;
		var position2D = new Vector2(position.x, position.z);
		var fishHeight = GetHeight(amplitude, frequency, offset);

		position += Time.deltaTime * speed * DirectionToWorld(direction);
		position.y = Water.GetHeightAt(position2D) +  heightOffset;// + fishHeight;

		var normal = Water.GetNormal(position2D);
		var tangent = Water.GetTangent(position2D, direction);

		m_transform.rotation = Quaternion.LookRotation(tangent, normal);
		m_transform.position = position;
    }

	float GetHeight(float amplitude, float frequency, float offset){
		return amplitude * Mathf.Sin(k_TwoPi * frequency * Time.time) - offset;
	}

	Vector3 DirectionToWorld(Vector2 direction){
		return new Vector3(direction.x, 0, direction.y);
	}
}
