#version 330 core
struct Material {
    vec3 color;
    float shininess;
};
struct Light {
    //For a directional light we dont need the lights position to calculate the direction.
    //Since the direction is the same no matter the position of the fragment we also dont need that.
    vec3 direction;
    vec3 diffuse;
    vec3 specular;
};

uniform Light light;
uniform Material material;
uniform vec3 viewPos;

out vec4 FragColor;

in vec3 Normal;
in vec2 TexCoords;

void main()
{
    // diffuse 
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(-light.direction);//We still normalize the light direction since we techically dont know,
                                                    //wether it was normalized for us or not.
    float nDotL = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * material.color * nDotL;

    // specular
    vec3 viewDir = normalize(viewPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * spec;

    vec3 result = diffuse + specular;
    FragColor = vec4(result + 0.2, 1.0);
}