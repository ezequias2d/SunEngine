#version 420 core
out vec4 FragColor;

in vec2 TexCoord;
in vec3 Normal;
in vec3 FragPos;

layout(binding = 0) uniform sampler2D diffuse;
layout(binding = 1) uniform sampler2D diffuseDark;

vec3 sun = vec3(0, 0, 0);

void main()
{
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(sun - FragPos);

    float diff = max(dot(norm, lightDir), 0.0);
    FragColor =  diff * texture(diffuse, TexCoord) + (1.0 - diff) * texture(diffuseDark, TexCoord);
} 