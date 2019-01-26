using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGizmos : MonoBehaviour
{
	[SerializeField] bool drawGizmos = false;
	void OnDrawGizmos() {
		if(!drawGizmos)
			return;

		var worldPos = transform.position;
		worldPos.x -= 5;
		worldPos.z -= 5;
		for(int x = 0; x < 10; x++) {
			for(int y = 0; y < 10; y++) {
				var localPos = new Vector3(worldPos.x, worldPos.y, worldPos.z);
				localPos.y = Water.GetHeightAt(new Vector2(worldPos.x + x, worldPos.z + y));
				localPos.x += x;
				localPos.z += y;
				Debug.DrawLine(localPos, localPos + .1f * Vector3.up, Color.white);
				//var tangent = GetTangent(new Vector2(localPos.x, localPos.z), new Vector2(1, 1));
				//Debug.DrawRay(localPos, tangent);
				//var normal = Water.GetNormal(new Vector2(localPos.x, localPos.z));
				//Debug.DrawRay(localPos, normal);
			}
		}
	}
}
