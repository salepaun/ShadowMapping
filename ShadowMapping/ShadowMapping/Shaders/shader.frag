#version 330 core
out vec4 FragColor;

uniform vec4 color;

void main()
{
    FragColor = color; // set all 4 vector values to 1.0
}