#version 330 core

in vec2 aPos;
in vec4 aColor;

out vec4 vColor;

uniform mat4 model;
uniform mat4 viewProj;

void main()
{
    gl_Position = vec4(aPos, 0.0, 1.0) * model * viewProj;
    vColor = aColor;
}
