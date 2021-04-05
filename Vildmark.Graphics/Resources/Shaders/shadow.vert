#version 400

uniform mat4 projection_matrix;
uniform mat4 view_matrix;
uniform mat4 model_matrix;
uniform vec3 offset;

layout(location = 0) in vec3 vert_position;

void main()
{
	gl_Position = projection_matrix * view_matrix * model_matrix * vec4(vert_position + offset, 1.0);
}
