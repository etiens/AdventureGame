using UnityEngine;
using System.Collections;

public class Utils : Singleton<Utils> {

	// Singleton
	protected Utils() {}

	// This function outputs the angle in deg between the vector
	// passed as a parameter and the positive y-axis.
	static public float Vec2Ang (Vector2 vector)
	{
		float returnAngle;
		returnAngle = Vector2.Angle(vector, new Vector2(0.0F, 1.0F));
		if (vector.x < 0) {
			returnAngle *= -1;		
		}
		return returnAngle;

	}
	static public Vector2 Get8Dir(Vector2 vec)
	{
		float sinmax = 0.9239f;
		float sinmin = 0.3826f;
		Vector2 dir = vec.normalized;
		if(dir.x > sinmax){
			dir = new Vector2(1,0);
		}
		else if (dir.x > sinmin){
			dir = new Vector2(1,1).normalized;
			
		}
		else if (dir.x > -sinmin){
			dir = new Vector2(0,1);
		}
		else if (dir.x > -sinmax){
			dir = new Vector2(-1,1).normalized;
		}
		else{
			dir = new Vector2(-1,0);
		}
		if(vec.y < 0)
			dir.y = -dir.y;
		return dir;
	}

	public const float eps = 0.0000001F;

}
