﻿#version 330

uniform mat4 projection_matrix;
uniform mat4 view_matrix;
uniform mat4 model_matrix;
uniform vec4 source_rect;
uniform vec3 offset;

layout(location = 0) in vec3 vert_position;
layout(location = 1) in vec2 vert_tex_coord;
layout(location = 2) in vec4 vert_color;
layout(location = 3) in vec3 vert_normal;

out vec2 vert_frag_tex_coord;
out vec4 vert_frag_color;

void main()
{
	gl_Position = projection_matrix * view_matrix * model_matrix * vec4(vert_position + offset, 1.0);
	vert_frag_color = vert_color;
	vert_frag_tex_coord = vec2((vert_tex_coord.x * source_rect.z) + source_rect.x, (vert_tex_coord.y * source_rect.w) + source_rect.y);
}