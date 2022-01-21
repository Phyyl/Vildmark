#version 460

uniform mat4 projection_matrix;
uniform mat4 view_matrix;
uniform mat4 model_matrix;

layout(location = 0) in vec2 vert_position;

void main()
{
	gl_Position = projection_matrix * view_matrix * model_matrix * vec4(vert_position.x, vert_position.y, 0.0, 1.0);
}
