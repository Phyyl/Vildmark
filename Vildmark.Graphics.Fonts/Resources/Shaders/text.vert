#version 400

uniform mat4 projection_matrix;
uniform mat4 view_matrix;
uniform mat4 model_matrix;

layout(location = 0) in vec3 vert_position;
layout(location = 1) in vec2 vert_tex_coord;
layout(location = 2) in int vert_texture_index;

out vec2 vert_frag_tex_coord;
flat out int vert_frag_texture_index;

void main()
{
	gl_Position = projection_matrix * view_matrix * model_matrix * vec4(vert_position, 1.0);
    vert_frag_tex_coord = vert_tex_coord;
    vert_frag_texture_index = vert_texture_index
}
