#version 330 core
out vec4 FragColor;

in vec2 TexCoord;
in vec3 Normal;
in vec3 FragPos;

layout(binding = 0) uniform sampler2D diffuse;

vec3 sun = vec3(0, 0, 0);

void main()
{
    float ambientStrength = 0.1;

    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(sun - FragPos);

    float diff = max(dot(norm, lightDir), 0.0);

    FragColor = (diff + ambientStrength) * texture(diffuse, TexCoord);
    //FragColor = vec4(normalize(FragPos), 1);
} 