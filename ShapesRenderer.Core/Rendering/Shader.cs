using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace ShapesRenderer.Core.Rendering;

internal class Shader : IDisposable
{
    private bool _disposedValue = false;
    private int _handle;

    public Shader(string vertexPath, string fragmentPath)
    {
        string vertexShaderSource = File.ReadAllText(vertexPath);
        string fragmentShaderSource = File.ReadAllText(fragmentPath);

        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexShaderSource);
        GL.CompileShader(vertexShader);
        CheckCompileErrors(vertexShader, "VERTEX");

        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentShaderSource);
        GL.CompileShader(fragmentShader);
        CheckCompileErrors(fragmentShader, "FRAGMENT");

        _handle = GL.CreateProgram();
        GL.AttachShader(_handle, vertexShader);
        GL.AttachShader(_handle, fragmentShader);
        GL.LinkProgram(_handle);
        CheckCompileErrors(_handle, "PROGRAM");

        GL.DetachShader(_handle, vertexShader);
        GL.DetachShader(_handle, fragmentShader);
        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }

    public void Use()
    {
        GL.UseProgram(_handle);
    }

    public bool SetColor4(string name, Color4 color)
    {
        int location = GetUniformLocation(name);
        if (location != -1)
        {
            GL.Uniform4(location, color);
            return true;
        }
        return false;
    }

    public bool SetMatrix4(string name, Matrix4 matrix)
    {
        int location = GetUniformLocation(name);
        if (location != -1)
        {
            GL.UniformMatrix4(location, true, ref matrix);
            return true;
        }
        return false;
    }

    public int GetAttribLocation(string attribName)
    {
        return GL.GetAttribLocation(_handle, attribName);
    }

    public int GetUniformLocation(string uniformName)
    {
        return GL.GetUniformLocation(_handle, uniformName);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            GL.DeleteProgram(_handle);

            _disposedValue = true;
        }
    }

    private static void CheckCompileErrors(int shader, string type)
    {
        int success;
        if (type != "PROGRAM")
        {
            GL.GetShader(shader, ShaderParameter.CompileStatus, out success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(shader);
                Console.WriteLine($"ERROR::SHADER_COMPILATION_ERROR of type: {type}\n{infoLog}\n -- --------------------------------------------------- -- ");
            }
        }
        else
        {
            GL.GetProgram(shader, GetProgramParameterName.LinkStatus, out success);
            if (success == 0)
            {
                string infoLog = GL.GetProgramInfoLog(shader);
                Console.WriteLine($"ERROR::PROGRAM_LINKING_ERROR of type: {type}\n{infoLog}\n -- --------------------------------------------------- -- ");
            }
        }
    }

    ~Shader()
    {
        if (!_disposedValue)
        {
            Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
        }
    }
}
