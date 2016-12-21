using UnityEngine;

public class GLTest : MonoBehaviour
{

    public Material mat;

    void Awake()
    {
		mat = new Material(Shader.Find("Diffuse"));
    }

	void OnPostRender()
    {
        // �������������Բ�Ҫ�˲��������������еĶ���ſ�����ʾ��
        GL.Clear(true, true, Color.black);

        // ����ǰ����任����push������������ֹ�Լ��Ĳ���Ӱ�쵽������Ⱦ����
        GL.PushMatrix();

        // ���û���ģʽΪ2D���ƣ��������ģʽ֮����Ļ���½Ǳ�Ϊ(0,0)����Ļ���ϽǱ�Ϊ(1,1)��ע��֮���Ϊ3D��ʵ����
        GL.LoadOrtho();

        // ���ƹ���
        for (var i = 0; i < mat.passCount; ++i)
        {
            // ����shader���ù�OpenGL ES2.0��ͬ־Ӧ��֪�������������combine glsl�Ĺ��̣�����ô���Լ�д���õ�unity�Դ���sprite��shader
            // ����shader���ܴ��ڶ��passͨ�������Բ��ñ����ķ�ʽ��ÿ��ͨ��������һ�飬��Ȼ��Щshaderֻ��һ��ͨ������������Դ���sprite��shader
            // Ҳ�������ó�SetPass(0)������ʹ��Ĭ�ϵĵ�һ��ͨ��������Ⱦ
            mat.SetPass(i);
            // ���û���ģʽΪ����ģʽ�����ģʽÿ��������Ϊһ�飩
            GL.Begin(GL.LINES);

            // ���ö�����ɫ��������һ���������ɫ���������û�и��ģ��������������ɫ����ֱ�������ģ�
            GL.Color(Color.red);
            // ��GL������һ���������
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0.5F, 0.5F, 0);

            GL.Color(Color.white);
            GL.Vertex3(0.5F, 0.5F, 0);
            GL.Color(Color.blue);
            GL.Vertex3(1F, 0F, 0);

            // ֪ͨGL�رյ�ǰ����ģʽ
            GL.End();
        }

        // ���������ԭ����֮ǰ��push�������Ӧ
        GL.PopMatrix();

		Debug.Log("??");
    }

}