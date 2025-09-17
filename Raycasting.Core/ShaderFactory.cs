namespace Raycasting.Core;

public class ShaderFactory : IDisposable
{
    private static ShaderFactory? _instance;
    private readonly Dictionary<(string, string), Shader> _shaders = [];

    public static ShaderFactory Instance
    {
        get
        {
            return _instance ??= new ShaderFactory();
        }
    }

    private ShaderFactory() { }

    public Shader GetShader(string vertexPath, string fragmentPath)
    {
        var key = (vertexPath, fragmentPath);
        if (!_shaders.TryGetValue(key, out var shader))
        {
            shader = new Shader(vertexPath, fragmentPath);
            _shaders[key] = shader;
        }
        return shader;
    }

    public void Dispose()
    {
        foreach (var shader in _shaders.Values)
        {
            shader.Dispose();
        }
        _shaders.Clear();
        _instance = null;
    }
}
