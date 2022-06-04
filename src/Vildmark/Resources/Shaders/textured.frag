#version 420

uniform sampler2D tex;
uniform vec4 tint;

in vec2 vert_frag_texcoord;
in vec4 vert_frag_color;

layout(location = 0) out vec4 frag_color;

void main()
{
	frag_color = texture(tex, vert_frag_texcoord) * vert_frag_color * tint;
}
