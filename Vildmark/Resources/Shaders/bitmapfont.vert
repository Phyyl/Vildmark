#version 420

uniform mat4 projection_matrix;
uniform mat4 view_matrix;
uniform mat4 model_matrix;

layout(location = 0) in vec2 vert_position;
layout(location = 1) in vec2 vert_texcoord;
layout(location = 2) in int vert_page_index;

out vec2 vert_frag_texcoord;
flat out int vert_frag_page_index;

void main()
{
	gl_Position = projection_matrix * view_matrix * model_matrix * vec4(vert_position.x, vert_position.y, 0.0, 1.0);
    vert_frag_texcoord = vert_texcoord;
    vert_frag_page_index = vert_page_index;
}
