#version 330

uniform sampler2D tex0;
uniform vec4 tint;

in vec2 vert_frag_tex_coord;
in vec4 vert_frag_color;

layout(location = 0) out vec4 frag_color;

void main()
{
	vec4 rgb = texture(tex0, vert_frag_tex_coord);

	frag_color = vec4(1.0, 1.0, 1.0, rgb.r) * vert_frag_color * tint;
}