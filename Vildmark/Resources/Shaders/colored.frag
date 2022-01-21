#version 460

uniform vec4 color;

layout(location = 0) out vec4 frag_color;

void main()
{
	frag_color = color;
}
