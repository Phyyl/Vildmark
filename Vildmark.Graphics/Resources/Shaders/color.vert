#version 400

uniform mat4 projection_matrix;
uniform mat4 view_matrix;
uniform mat4 model_matrix;

layout(location = 0) in vec3 vert_position;
layout(location = 1) in vec4 vert_color;

out vec4 vert_frag_color;

void main()
{
	gl_Position = projection_matrix * view_matrix * model_matrix * vec4(vert_position, 1.0);
	vert_frag_color = vert_color;
}
