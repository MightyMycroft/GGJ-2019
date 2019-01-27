using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Water {
	public class GrestnerParamters {
		public float steepness;
		public float amplitude;
		public float phase;
		public float crestSharpness;
		public Vector2 direction;

		public GrestnerParamters(float amplitude, float steepness, float phase, float crestSharpness, Vector2 direction){
			this.amplitude = amplitude;
			this.steepness = steepness;
			this.phase = phase;
			this.crestSharpness = crestSharpness;
			this.direction = direction;
		}
	}

	static GrestnerParamters[] layers = new GrestnerParamters[]
	{
		new GrestnerParamters(1f, 1f, 1f, .5f, new Vector2(0, 1f)){},
		new GrestnerParamters(.5f, 1f, .5f, .5f, new Vector2(0, .8f)){},
		new GrestnerParamters(.25f, 1f, 1f, 1f, new Vector2(0, 1.2f)){},
		new GrestnerParamters(.12f, 1f, 2f, 1f, new Vector2(0, .5f)){}
	};

	static Vector2 islandPosition = Vector3.zero;
	static float islandThreshold = 10f;
	static float distanceFalloff = 10f;
	static float minWaveHeight = .3f;
	static float maxWaveHeight = 1.0f;

	public static Vector3 GetNormal(Vector2 position){
		var tangent = GetTangent(position, Vector2.up);
		var biTangent = GetTangent(position, Vector2.right);
		return Vector3.Cross(tangent, biTangent).normalized;
	}

	public static Vector3 GetTangent(Vector2 position, Vector2 direction){
		var dirPos = position + .1f * direction.normalized;
		var pos1 = new Vector3(position.x, GetHeightAt(position.x, position.y), position.y);
		var pos2 = new Vector3(dirPos.x, GetHeightAt(dirPos.x, dirPos.y), dirPos.y);
		
		return (pos2 - pos1).normalized;
	}

	public static float GetHeightAt(Vector2 pos)
	{
		var yPos = 0f;

		for(int i = 0; i < layers.Length; i++) {
			var layer = layers[i];
			yPos += (layer.steepness * layer.amplitude) * layer.direction.y * Mathf.Cos(layer.crestSharpness * Vector2.Dot(layer.direction, pos) + layer.phase * Time.time);
		}

		var multiplier = Vector2.Distance(islandPosition, pos);
		multiplier -= islandThreshold;
		multiplier /= distanceFalloff;
		multiplier = Mathf.Clamp01(multiplier);
		multiplier = Mathf.Lerp(minWaveHeight, maxWaveHeight, multiplier);

		return multiplier * yPos;
	}

	public static float GetHeightAt(float x, float y){
		return GetHeightAt(new Vector2(x, y));
	}
}
