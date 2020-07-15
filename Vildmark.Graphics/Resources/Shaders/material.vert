#version 330

uniform mat4 projection_matrix;
uniform sampler2D tex0;

layout(location = 0) in vec3 vert_position;
layout(location = 1) in vec2 vert_tex_coord;
layout(location = 2) in vec4 vert_color;
layout(location = 3) in vec3 vert_normal;

out vec2 vert_frag_tex_coord;
out vec4 vert_frag_color;

void main()
{
    vec2 tex0HalfTexelSize = 1.0 / vec2(textureSize(tex0, 0)) / 2.0;
	gl_Position = projection_matrix * vec4(vert_position, 1.0);
	vert_frag_color = vert_color + vec4(1.0, 1.0, 1.0, 1.0) * (abs(vert_normal.x) - abs(vert_normal.z)) * 0.05;
	vert_frag_tex_coord = vert_tex_coord - tex0HalfTexelSize;
}