﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Math
{
	public float getAngle(Vector2 vec1, Vector2 vec2)
	{
		Vector2 diference = vec2 - vec1;
		float sign = (vec2.y < vec1.y)? -1.0f : 1.0f;
		return Vector2.Angle(Vector2.right, diference) * sign;
	}
}
