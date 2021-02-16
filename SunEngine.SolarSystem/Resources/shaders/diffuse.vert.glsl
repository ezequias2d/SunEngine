#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoord;
layout (location = 2) in vec3 aNormal;

out vec2 TexCoord;
out vec3 Normal;
out vec3 FragPos;

layout (std140) uniform Matrices
{
    mat4 projection;
    mat4 view;
};

layout (std140) uniform Model
{
    mat4 model;
};

void main()
{
    gl_Position = projection * view * model * vec4(aPos.x, aPos.y, aPos.z, 1.0);
    TexCoord = aTexCoord;
    Normal = mat3(transpose(inverse(model))) * aNormal;
    FragPos = vec3(model * vec4(aPos, 1.0));
    //gl_Position = vec4(aPos.x / aPos.z, aPos.y / aPos.z, 0, 1.0);
}