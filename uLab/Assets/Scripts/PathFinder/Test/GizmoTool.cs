
using UnityEngine;

using System.Collections.Generic;


public class GizmoTool
{
	/// <summary>
	/// When called from an OnDrawGizmos() function it will draw a curved path through the provided array of Vector3s.
	/// </summary>
	/// <param name=""path"">
	/// A <see cref=""Vector3s[]"/">
	/// 
	/// <param name=""color"">
	/// A <see cref=""Color"/">
	///  
	public static void DrawPath(Vector3[] path, Color color)
	{
		if (path.Length > 0)
		{
			DrawPathHelper(path, color);
		}
	}

	/// <summary>
	/// When called from an OnDrawGizmos() function it will draw a line through the provided array of Vector3s.
	/// </summary>
	/// <param name=""line"">
	/// A <see cref=""Vector3s[]"/">
	/// 
	/// <param name=""color"">
	/// A <see cref=""Color"/">
	///  
	public static void DrawLine(Vector3[] line, Color color)
	{
		if (line != null && line.Length > 0)
		{
			DrawLineHelper(line, color);
		}
	}

	public static void DrawRect(Rect rect, Color color)
	{
		Vector3[] line = new Vector3[5];
		line[0] = new Vector3(rect.x, rect.y, 0);
		line[1] = new Vector3(rect.x + rect.width, rect.y, 0);
		line[2] = new Vector3(rect.x + rect.width, rect.y + rect.height, 0);
		line[3] = new Vector3(rect.x, rect.y + rect.height, 0);
		line[4] = new Vector3(rect.x, rect.y, 0);
		if (line != null && line.Length > 0)
		{
			DrawLineHelper(line, color);
		}
	}

	private static void DrawLineHelper(Vector3[] line, Color color)
	{
		Gizmos.color = color;
		for (int i = 0; i < line.Length - 1; i++)
		{
			Gizmos.DrawLine(line[i], line[i + 1]); ;
		}
	}

	private static void DrawPathHelper(Vector3[] path, Color color)
	{
		Vector3[] vector3s = PathControlPointGenerator(path);

		Vector3 prevPt = Interp(vector3s, 0);
		Gizmos.color = color;
		int SmoothAmount = path.Length * 20;
		for (int i = 1; i <= SmoothAmount; i++)
		{
			float pm = (float)i / SmoothAmount;
			Vector3 currPt = Interp(vector3s, pm);
			Gizmos.DrawLine(currPt, prevPt);
			prevPt = currPt;
		}
	}

	/// <summary>
	/// 三点计算抛物线.
	/// </summary>
	/// <returns>组成抛物线的点.</returns>
	/// <param name=""path"">确定抛物线的三个点或者更多的点的数组.
	public static List<Vector3> DrawPathHelper(Vector3[] path)
	{
		List<Vector3> array = new List<Vector3>(177);
		Vector3[] vector3s = PathControlPointGenerator(path);
		//Line Draw:
		Vector3 prevPt = Interp(vector3s, 0);
		int SmoothAmount = path.Length * 20;
		for (int i = 1; i <= SmoothAmount; i++)
		{
			float pm = (float)i / SmoothAmount;
			Vector3 currPt = Interp(vector3s, pm);
			array.Add(currPt);
			prevPt = currPt;
		}
		return array;
	}

	private static Vector3[] PathControlPointGenerator(Vector3[] path)
	{
		Vector3[] suppliedPath;
		Vector3[] vector3s;
		//create and store path points:
		suppliedPath = path;
		//populate calculate path;
		int offset = 2;
		vector3s = new Vector3[suppliedPath.Length + offset];
		System.Array.Copy(suppliedPath, 0, vector3s, 1, suppliedPath.Length);
		//populate start and end control points:
		//vector3s[0] = vector3s[1] - vector3s[2];
		vector3s[0] = vector3s[1] + (vector3s[1] - vector3s[2]);
		vector3s[vector3s.Length - 1] = vector3s[vector3s.Length - 2] + (vector3s[vector3s.Length - 2] - vector3s[vector3s.Length - 3]);
		//is this a closed, continuous loop? yes? well then so let's make a continuous Catmull-Rom spline!
		if (vector3s[1] == vector3s[vector3s.Length - 2])
		{
			Vector3[] tmpLoopSpline = new Vector3[vector3s.Length];
			System.Array.Copy(vector3s, tmpLoopSpline, vector3s.Length);
			tmpLoopSpline[0] = tmpLoopSpline[tmpLoopSpline.Length - 3];
			tmpLoopSpline[tmpLoopSpline.Length - 1] = tmpLoopSpline[2];
			vector3s = new Vector3[tmpLoopSpline.Length];
			System.Array.Copy(tmpLoopSpline, vector3s, tmpLoopSpline.Length);
		}
		return (vector3s);
	}

	//andeeee from the Unity forum's steller Catmull-Rom class ( http://forum.unity3d.com/viewtopic.php?p=218400#218400 ):
	private static Vector3 Interp(Vector3[] pts, float t)
	{
		int numSections = pts.Length - 3;
		int currPt = Mathf.Min(Mathf.FloorToInt(t * (float)numSections), numSections - 1);
		float u = t * (float)numSections - (float)currPt;
		Vector3 a = pts[currPt];
		Vector3 b = pts[currPt + 1];
		Vector3 c = pts[currPt + 2];
		Vector3 d = pts[currPt + 3];
		return .5f * (
			(-a + 3f * b - 3f * c + d) * (u * u * u)
			+ (2f * a - 5f * b + 4f * c - d) * (u * u)
			+ (-a + c) * u
			+ 2f * b
			);
	}

}



public class GUIHelper
{
    protected static bool clippingEnabled;
    protected static Rect clippingBounds;
    protected static Material lineMaterial;
 
    /* @ Credit: "http://cs-people.bu.edu/jalon/cs480/Oct11Lab/clip.c" */
    protected static bool clip_test(float p, float q, ref float u1, ref float u2)
    {
        float r;
        bool retval = true;
        if (p < 0.0)
        {
            r = q / p;
            if (r > u2)
                retval = false;
            else if (r > u1)
                u1 = r;
        }
        else if (p > 0.0)
        {
            r = q / p;
            if (r < u1)
                retval = false;
            else if (r < u2)
                u2 = r;
        }
        else
            if (q < 0.0)
                retval = false;
 
        return retval;
    }
 
    protected static bool segment_rect_intersection(Rect bounds, ref Vector2 p1, ref Vector2 p2)
    {
        float u1 = 0.0f, u2 = 1.0f, dx = p2.x - p1.x, dy;
        if (clip_test(-dx, p1.x - bounds.xMin, ref u1, ref u2))
            if (clip_test(dx, bounds.xMax - p1.x, ref u1, ref u2))
            {
                dy = p2.y - p1.y;
                if (clip_test(-dy, p1.y - bounds.yMin, ref u1, ref u2))
                    if (clip_test(dy, bounds.yMax - p1.y, ref u1, ref u2))
                    {
                        if (u2 < 1.0)
                        {
                            p2.x = p1.x + u2 * dx;
                            p2.y = p1.y + u2 * dy;
                        }
                        if (u1 > 0.0)
                        {
                            p1.x += u1 * dx;
                            p1.y += u1 * dy;
                        }
                        return true;
                    }
            }
        return false;
    }
 
    public static void BeginGroup(Rect position)
    {
        clippingEnabled = true;
        clippingBounds = new Rect(0, 0, position.width, position.height);
        GUI.BeginGroup(position);
    }
 
    public static void EndGroup()
    {
        GUI.EndGroup();
        clippingBounds = new Rect(0, 0, Screen.width, Screen.height);
        clippingEnabled = false;
    }
 
    public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color)
    {
        if (clippingEnabled)
            if (!segment_rect_intersection(clippingBounds, ref pointA, ref pointB))
                return;
 
        if (!lineMaterial)
        {
            /* Credit:  */
            lineMaterial = new Material("Shader \"Lines/Colored Blended\" {" +
           "SubShader { Pass {" +
           "   BindChannels { Bind \"Color\",color }" +
           "   Blend SrcAlpha OneMinusSrcAlpha" +
           "   ZWrite Off Cull Off Fog { Mode Off }" +
           "} } }");
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
        }
 
        lineMaterial.SetPass(0);
        GL.Begin(GL.LINES);
        GL.Color(color);
        GL.Vertex3(pointA.x, pointA.y, 0);
        GL.Vertex3(pointB.x, pointB.y, 0);
        GL.End();
    }
};

