#version 330 core
out vec4 FragColor;

in vec2 TexCoord;
in vec3 Normal;
in vec3 FragPos;

layout(binding = 0) uniform sampler2D diffuse;

vec3 sun = vec3(0, 0, 0);

mat4 thresholdMatrix =
mat4(1.0 / 17.0,  9.0 / 17.0,  3.0 / 17.0, 11.0 / 17.0,
    13.0 / 17.0,  5.0 / 17.0, 15.0 / 17.0,  7.0 / 17.0,
     4.0 / 17.0, 12.0 / 17.0,  2.0 / 17.0, 10.0 / 17.0,
    16.0 / 17.0,  8.0 / 17.0, 14.0 / 17.0,  6.0 / 17.0);

int module(float x, int y)
{
    return int(x) - y * (int(x) / y);
}

void main()
{
    float ambientStrength = 0.1;

    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(sun - FragPos);

    float diff = max(dot(norm, lightDir), 0.0);
    
    vec4 color = texture(diffuse, TexCoord);
    if(color.w <= thresholdMatrix[module(gl_FragCoord.x, 4)][module(gl_FragCoord.y, 4)])
        discard;

    FragColor = vec4((diff + ambientStrength) * color.xyz, 1.0);
} 