#version 400

uniform vec4 tint;

in vec4 vert_frag_color;

layout(location = 0) out vec4 frag_color;

void main()
{
	frag_color = vert_frag_color * tint;
}
