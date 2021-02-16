#version 330 core
out vec4 FragColor;

in vec2 TexCoord;
in vec3 Normal;

layout(binding = 0) uniform sampler2D diffuse;

void main()
{
    FragColor = texture(diffuse, TexCoord);
} 